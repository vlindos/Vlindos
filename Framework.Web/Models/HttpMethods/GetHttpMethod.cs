namespace Framework.Web.Models.HttpMethods
{
    public interface IGetHttpMethod : IHttpMethod
    {
    }

    public class GetHttpMethod : IGetHttpMethod
    {
        public string MethodName { get { return "GET"; } }
    }
}