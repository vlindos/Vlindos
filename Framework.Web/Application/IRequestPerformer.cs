namespace Framework.Web.Application
{
    public interface IRequestPerformer<out TResponse>
    {
        TResponse Perform();
    }
}