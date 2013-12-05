using System;
using Database.Operations;
using Database.Operations.Results;
using Vlindos.Common.Extensions.IEnumerable;

namespace Database.DemoApplication
{   
    class Program
    {
        static void Main()
        {
            const string databaseDirectory = @"";
            IDatabaseOpener databaseOpener = null;
            var database = databaseOpener.OpenDatabase<EntityExample>(databaseDirectory);
            var newEntity = new EntityExample();

            var r = database.Add(newEntity)
                            .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Messages));
                return;
            }

            r = database.Add(newEntity)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Messages));
                return;
            }

            r = database.Delete()
                        .Where(x => x.Entity.Name == "")
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Messages));
                return;
            }

            r = database.Update(newEntity)
                        .Where(x => x.Id == Guid.Empty)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Messages));
                return;
            }

            r = database.Update(x => x.Entity)
                        .Where(x => x.Id == Guid.Empty)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Messages));
                return;
            }

            var q = database.Select()
                            .Where(x => x.Changed > DateTimeOffset.Now)
                            .Top(10)
                            .Offset(10)
                            .OrderBy(x => x.Created, OrderType.Ascending)
                            .Retrieve(entities => entities.ForEach(x => Console.WriteLine("{0}", x.Id)), 10);
            if (q.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, q.Messages));
            }

            var transactionOperation = database.ExecuteInTranscation(tx =>
            {
                var selectRequest = tx.SelectOne()
                    .Where(x => x.Changed > DateTimeOffset.Now)
                    .Offset(10)
                    .RetrieveOne();
                if (!selectRequest.Success) // check if queries had compiled well
                {
                    Console.WriteLine(string.Join(Environment.NewLine, selectRequest.Messages));
                    return Transaction.Rollback;
                }
                var item = selectRequest.Result.Entity;
                IOperationResult<EntityExample> operationResult;
                if (item == null)
                {
                    operationResult = tx.Add(new EntityExample { Name = "new Example" })
                        .Perform();
                }
                else
                {
                    operationResult = tx.Update(new EntityExample { Name = "Example Update By Update" })
                        .Perform();
                }
                if (!operationResult.Success) // check if queries had compiled well
                {
                    Console.WriteLine(string.Join(Environment.NewLine, operationResult.Messages));
                    return Transaction.Rollback;
                }
                return Transaction.Commit;
            }, new TimeSpan(0, 0, 0, 15));

            if (transactionOperation.Success == false)
            {
                Console.WriteLine(transactionOperation.TransactionResult.ToString());
            }

            Console.WriteLine("All OK.");
        }
    }
}
