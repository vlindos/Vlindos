namespace Framework.Web.Models.HttpMethods
{
    public interface IOptionsHttpMethod : IHttpMethod
    {
    }

    public class OptionsHttpMethod : IOptionsHttpMethod
    {
        public string MethodName { get { return "OPTIONS"; } }
    }
}