using System;
using System.Linq;

namespace Database.DemoApplication
{

    public class EntityExample : IEntity
    {
        public string Name { get; set; }
    }

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
                Console.WriteLine(string.Join("\r\n", r.Errors));
                return;
            }

            r = database.Delete()
                        .Where(x => x.Entity.Name == "")
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join("\r\n", r.Errors));
                return;
            }

            r = database.Update(newEntity)
                        .Where(x => x.Id == Guid.Empty)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join("\r\n", r.Errors));
                return;
            }

            r = database.Update(x => x)
                        .Where(x => x.Id == Guid.Empty)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join("\r\n", r.Errors));
                return;
            }

            var q = database.Select()
                            .Where(x => x.Changed > DateTimeOffset.Now)
                            .Top(10)
                            .Offset(10)
                            .OrderBy(x => x.Created, OrderType.Ascending);

            var retrieveAllResult = q.RetrieveAll();
            if (retrieveAllResult.Success == false)
            {
                Console.WriteLine(string.Join("\r\n", retrieveAllResult.Errors));
                return;
            }
            foreach (var entity in retrieveAllResult.Entities)
            {
                Console.WriteLine("{0}", entity.Id);
            }

            using (var retrieveResult = q.RetriveByBatchOf(10))
            {
                while (retrieveAllResult.Success && retrieveResult.Entities.Any())
                {
                    foreach (var entity in retrieveResult.Entities)
                    {
                        Console.WriteLine("{0}", entity.Id);
                    }

                    retrieveResult.RetrieveNextBatch();
                }
                if (!retrieveAllResult.Success)
                {
                    Console.WriteLine(string.Join("\r\n", retrieveAllResult.Errors));
                }
            }
        }
    }
}
