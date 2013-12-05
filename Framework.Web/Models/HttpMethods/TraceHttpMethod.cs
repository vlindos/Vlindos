namespace Framework.Web.Models.HttpMethods
{
    public class TraceHttpMethod : IHttpMethod
    {
        public string MethodName { get { return "TRACE"; } }
    }
}