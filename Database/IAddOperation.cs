namespace Database
{
    public interface IAddOperation<T>
        where T : IEntity
    {
        IResult<T> Perform();
    }
}