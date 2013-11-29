using System;

namespace Database.Entity
{
    public class EntityHolder<T> 
        where T : IEntity
    {
        public Guid Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Changed { get; set; }

        public T Entity { get; set; }
    }
}