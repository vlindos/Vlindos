namespace Database
{
    public interface ISelectOperation<T> : ICriteriaOperation<T, ISelectOperation<T>>
        where T : IEntity
    {
        IRetrieveByBatchResult<T> RetriveByBatchOf(int maximum);
        IRetrieveAllResult<T> RetrieveAll(int maximum);
    }
}