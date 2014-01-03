namespace Framework.Web.HttpMethods
{
    public interface IHeadHttpMethod : IHttpMethod
    {
    }

    public class HeadHttpMethod : IHeadHttpMethod
    {
        public string MethodName { get { return "HEAD"; } }
    }
}