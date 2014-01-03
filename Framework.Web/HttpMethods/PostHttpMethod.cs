namespace Framework.Web.HttpMethods
{
    public interface IPostHttpMethod : IHttpMethod
    {
    }

    public class PostHttpMethod : IPostHttpMethod
    {
        public string MethodName { get { return "POST"; } }
    }
}