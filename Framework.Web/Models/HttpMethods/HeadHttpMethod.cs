namespace Framework.Web.Models.HttpMethods
{
    public class HeadHttpMethod : IHttpMethod
    {
        public string MethodName { get { return "HEAD"; } }
    }
}