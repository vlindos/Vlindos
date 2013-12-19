namespace Framework.Web.Models.HttpMethods
{
    public interface IConnectHttpMethod : IHttpMethod
    {
    }

    public class ConnectHttpMethod : IConnectHttpMethod
    {
        public string MethodName { get { return "CONNECT"; } }
    }
}