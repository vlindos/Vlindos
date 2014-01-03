namespace Framework.Web.HttpMethods
{
    public interface IDeleteHttpMethod : IHttpMethod
    {
    }

    public class DeleteHttpMethod : IDeleteHttpMethod
    {
        public string MethodName { get { return "DELETE"; } }
    }
}