using Database.Entity;

namespace Database.DemoApplication
{
    public class EntityExample : IEntity
    {
        public string Name { get; set; }

        public Entity2Example Entity2Example { get; set; }
    }

    public class Entity2Example : IEntity
    {
        public int Num { get; set; }
    }
}