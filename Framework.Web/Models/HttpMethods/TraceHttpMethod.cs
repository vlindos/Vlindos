namespace Framework.Web.Models.HttpMethods
{
    public interface ITraceHttpMethod : IHttpMethod
    {
    }

    public class TraceHttpMethod : ITraceHttpMethod
    {
        public string MethodName { get { return "TRACE"; } }
    }
}