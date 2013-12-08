namespace Framework.Web.Application.Session
{
    public interface ISessionIdSpecifier
    {
        string SessionId { get; }
    }

    public class SessionIdSpecifier : ISessionIdSpecifier
    {
        public string SessionId { get { return "SessionId"; } }
    }
}