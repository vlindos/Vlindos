namespace Framework.Web.HttpMethods
{
    public interface IPutHttpMethod : IHttpMethod
    {
    }

    public class PutHttpMethod : IPutHttpMethod
    {
        public string MethodName { get { return "PUT"; } }
    }
}