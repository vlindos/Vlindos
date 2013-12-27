namespace Framework.Web.Service.Rest
{
    public interface IRequestPerformer<out TResponse>
    {
        TResponse Perform();
    }
}