namespace Framework.Web.HttpMethods
{
    public interface IConnectHttpMethod : IHttpMethod
    {
    }

    public class ConnectHttpMethod : IConnectHttpMethod
    {
        public string MethodName { get { return "CONNECT"; } }
    }
}