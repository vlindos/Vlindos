using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Database.DemoApplication
{

    public class EntityExample : IEntity
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
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

            r = database.Select()
                        .Where(x => x.Changed > DateTimeOffset.Now)
                        .Top(10)
                        .Offset(10)
                        .OrderBy(x => x.Created, OrderType.Ascending)
                        .Perform();
            if (r.Success == false)
            {
                Console.WriteLine(string.Join("\r\n", r.Errors));
                return;
            }
            IRetrieveResult<EntityExample> retrieveResult;
            while ((retrieveResult = r.Retrive(1)).Entities.Any())
            {
                foreach (var entity in retrieveResult.Entities)
                {
                    Console.WriteLine("{0}", entity.Id);
                }
            }
            if (retrieveResult.Success == false)
            {
                Console.WriteLine(string.Join("\r\n", retrieveResult.Errors));
                return;
            }
            var retrieveAllResult = r.RetrieveAll();
            if (retrieveAllResult.Success == false)
            {
                Console.WriteLine(string.Join("\r\n", retrieveResult.Errors));
                return;
            }
            foreach (var entity in retrieveResult.Entities)
            {
                Console.WriteLine("{0}", entity.Id);
            }
        }
    }
}
