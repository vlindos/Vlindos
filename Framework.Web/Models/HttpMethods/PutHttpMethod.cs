namespace Framework.Web.Models.HttpMethods
{
    public interface IPutHttpMethod : IHttpMethod
    {
    }

    public class PutHttpMethod : IPutHttpMethod
    {
        public string MethodName { get { return "PUT"; } }
    }
}