namespace Framework.Web.HttpMethods
{
    public interface IOptionsHttpMethod : IHttpMethod
    {
    }

    public class OptionsHttpMethod : IOptionsHttpMethod
    {
        public string MethodName { get { return "OPTIONS"; } }
    }
}