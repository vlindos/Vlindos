using System;
using System.Collections.Generic;
using System.Linq;
using Database.Entity;
using Database.Operations;
using Database.Operations.Results;

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
                Console.WriteLine(string.Join(Environment.NewLine, r.Errors));
                return;
            }

            r = database.Add(newEntity)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Errors));
                return;
            }

            r = database.Delete()
                        .Where(x => x.Entity.Name == "")
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Errors));
                return;
            }

            r = database.Update(newEntity)
                        .Where(x => x.Id == Guid.Empty)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Errors));
                return;
            }

            r = database.Update(x => x.Entity)
                        .Where(x => x.Id == Guid.Empty)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, r.Errors));
                return;
            }

            var q = database.Select()
                            .Where(x => x.Changed > DateTimeOffset.Now)
                            .Top(10)
                            .Offset(10)
                            .OrderBy(x => x.Created, OrderType.Ascending)
                            .Retrieve(10, entities =>
                            {
                                foreach (var entity in entities)
                                {
                                    Console.WriteLine("{0}", entity.Id);
                                }
                            });
            if (q.Success == false)
            {
                Console.WriteLine(string.Join(Environment.NewLine, q.Errors));
            }


            var transactionOperation = database.ExecuteInTranscation(new TimeSpan(0, 0, 0, 15), tx =>
            {
                var selectRequest = tx.SelectOne()
                                      .Where(x => x.Changed > DateTimeOffset.Now)
                                      .Offset(10)
                                      .RetrieveOne();
                if (!selectRequest.Success) // check if queries had compiled well
                {
                    Console.WriteLine(string.Join(Environment.NewLine, selectRequest.Errors));
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
                    Console.WriteLine(string.Join(Environment.NewLine, operationResult.Errors));
                    return Transaction.Rollback;
                }
                return Transaction.Commit;
            });

            if (transactionOperation.Success == false)
            {
                Console.WriteLine(transactionOperation.TransactionResult.ToString());
            }

            Console.WriteLine("All OK.");
        }
    }
}
