﻿using System;
using System.Linq;
using Database.Entity;
using Database.Operations;
using Database.Operations.Results;

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

            using (var retrieveResult = q.Retrieve(10))
            {
                while (retrieveResult.Success && retrieveResult.Entities.Any())
                {
                    foreach (var entity in retrieveResult.Entities)
                    {
                        Console.WriteLine("{0}", entity.Id);
                    }

                    retrieveResult.RetrieveNextBatch();
                }
                if (!retrieveResult.Success)
                {
                    Console.WriteLine(string.Join("\r\n", retrieveResult.Errors));
                }
            }

            using (database.BeginTranscation())
            {
                var item = database
                    .Select()
                    .Where(x => x.Changed > DateTimeOffset.Now)
                    .Top(1)
                    .Offset(10)
                    .OrderBy(x => x.Created, OrderType.Ascending)
                    .Retrieve(1)
                    .Entities
                    .FirstOrDefault();
                IOperationResult<EntityExample> operationResult;
                if (item == null)
                {
                    operationResult = database.Add(new EntityExample { Name = "Exampe #1" }).Perform();
                }
                else
                {
                    operationResult = database.Update(x =>
                    {
                        x.Entity.Name = "Example #2";
                        return x;
                    }).Where(x => x.Id == item.Id).Perform();
                }
                if (!operationResult.Success) // check if queries had compiled well
                {
                    Console.WriteLine(string.Join("\r\n", operationResult.Errors));
                }
                operationResult = database.Commit(); // check if actuall persistense went well
                if (!operationResult.Success)
                {
                    Console.WriteLine(string.Join("\r\n", operationResult.Errors));
                }
            } // Dispose will make sure the transcation is released (or rollbacked if not committed)
        }
    }
}
