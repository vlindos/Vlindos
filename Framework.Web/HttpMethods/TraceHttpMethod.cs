namespace Framework.Web.HttpMethods
{
    public interface ITraceHttpMethod : IHttpMethod
    {
    }

    public class TraceHttpMethod : ITraceHttpMethod
    {
        public string MethodName { get { return "TRACE"; } }
    }
}