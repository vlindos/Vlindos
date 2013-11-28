using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Database.DemoApplication
{

    public class Entity : object          
    {
        public Guid Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof (Entity)) return false;
            return ((Entity)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class EntityExample : Entity
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseManager databaseManager = null;
            var database = databaseManager.OpenDatabase("");
            var entity = new EntityExample();      
            database.SubscribeForAdd<EntityExample>(entittyInserted =>
            {
                if (entityInserted.Id != entity.Id)
                {
                    // subscribtion event raised before insert confirmation
                }    
            });

            database.Add<EntityExample>(entity);

            database.Delete<EntityExample>().ByCriteria(x => x.Name == "").Or(x => x.Id != Guid.Empty);
            database.Update<EntityExample>(entity);
            database.Update<EntityExample>(entity).ByCriteria(x => x.Name == "");
        }
    }
}
