using System;
using System.Collections.Generic;

namespace Database
{
    public enum OrderType
    {
        Default,
        Ascending,
        Descending
    }

    public enum OperationType
    {
        Add,
        Update,
        Select,
        Delete
    }

    public class EntityHolder<T> where T : IEntity
    {
        public Guid Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Changed { get; set; }

        public T Entity { get; set; }
    }

    public interface IEntity
    {

    }

    public interface IDatabase<T> : IDisposable where T : IEntity
    {
        IOperation<T> Add(params IEntity[] entity);
        IDeleteOperation<T> Delete();
        IUpdateOperation<T> Update(IEntity entity);
        IUpdateOperation<T> Update(Func<EntityHolder<T>> entityUpdater);
        ISelectionOperation<T> Select();
    }

    public interface IDatabaseOpener
    {
        IDatabase<T> OpenDatabase<T>(string directoryPath) where T : IEntity;
    }

    public interface IOperation<T> where T : IEntity
    {
        OperationType OperationType { get; set; }
    
        IOperationResult<T> Perform();
    }

    public interface ICriteriaOperation<T> : IOperation<T> where T : IEntity
    {
        ICriteriaOperation<T> Where(Predicate<EntityHolder<T>> whereCriteria);

        ICriteriaOperation<T> Top(long top);

        ICriteriaOperation<T> Offset(long offset);

        ICriteriaOperation<T> OrderBy(Func<EntityHolder<T>, object> property, OrderType orderType);
    }

    public interface IAddOperation<T> : IOperation<T> where T : IEntity
    {
        IEnumerable<T> Entities { get; set; }
    }

    public interface ISelectionOperation<T> : ICriteriaOperation<T> where T : IEntity
    {
        
    }

    public interface IDeleteOperation<T> : ICriteriaOperation<T> where T : IEntity
    {
        
    }

    public interface IUpdateOperation<T> : ICriteriaOperation<T> where T : IEntity
    {
        
    }

    public interface IOperationResult<T> where T : IEntity
    {
        bool Success { get; set; }
        IEnumerable<string> Errors { get; set; }
        IRetrieveResult<T> Retrive(int number);
        IRetrieveResult<T> RetrieveAll();
    }

    public interface IRetrieveResult<T> where T : IEntity
    {
        bool HasResult { get; set; }
        IEnumerable<EntityHolder<T>> Entities { get; set; }
        bool Success { get; set; }
        IEnumerable<string> Errors { get; set; }
    }
}
