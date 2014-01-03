namespace Framework.Web.Application.HttpEndpoint
{
    public interface IPerformer<out TResponse>
    {
        TResponse Perform();   
    }

    public interface IPerformer<in TRequest, out TResponse>
    {
        TResponse Perform(TRequest request);
    }
}