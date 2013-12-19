namespace Framework.Web.Models.HttpMethods
{
    public interface IDeleteHttpMethod : IHttpMethod
    {
    }

    public class DeleteHttpMethod : IDeleteHttpMethod
    {
        public string MethodName { get { return "DELETE"; } }
    }
}