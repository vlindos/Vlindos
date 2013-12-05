namespace Framework.Web.Models.HttpMethods
{
    public class OptionsHttpMethod : IHttpMethod
    {
        public string MethodName { get { return "OPTIONS"; } }
    }
}