namespace Framework.Web.Session
{
    public interface ISessionIdLiteralSpecifier
    {
        string SessionId { get; }
    }

    public class SessionIdLiteralSpecifier : ISessionIdLiteralSpecifier
    {
        public string SessionId { get { return "SessionId"; } }
    }
}