using Database.Entity;

namespace Database.Operations
{
    public interface ISelectOneCriteriaOperation<T, out TOperation> :
        IWhereOperation<T, TOperation>,
        IOffsetOperation<TOperation>
        where T : IEntity
    {
    }
}