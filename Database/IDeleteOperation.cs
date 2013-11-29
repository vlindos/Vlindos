namespace Database
{
    public interface IDeleteOperation<T> : ICriteriaOperation<T, IDeleteOperation<T>> 
        where T : IEntity
    {
        IResult<T> Perform();
    }
}