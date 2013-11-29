namespace Database
{
    public interface IRetrieveAllResult<T> : IRetrieveResult<T>
        where T : IEntity
    {
    }
}