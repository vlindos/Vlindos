namespace Framework.Web.Models.HttpMethods
{
    public class GetHttpMethod : IHttpMethod
    {
        public string MethodName { get { return "GET"; } }
    }
}