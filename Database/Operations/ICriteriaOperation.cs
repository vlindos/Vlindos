using Database.Entity;

namespace Database.Operations
{
    public interface ICriteriaOperation<T, out TOperation> :
        IWhereOperation<T, TOperation>,
        ITopOperation<TOperation>,
        IOffsetOperation<TOperation>, 
        IOrderByOperation<T, TOperation> 
        where T : IEntity
    {
    }
}