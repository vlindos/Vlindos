namespace Framework.Web.Models.HttpMethods
{
    public interface IHeadHttpMethod : IHttpMethod
    {
    }

    public class HeadHttpMethod : IHeadHttpMethod
    {
        public string MethodName { get { return "HEAD"; } }
    }
}