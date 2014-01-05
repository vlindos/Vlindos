namespace Framework.Web.Tools
{
    public interface IStandardHeadersConstants
    {
        string Cookie { get; }
        string SetCookie { get; }
        string ContentType { get; }
    }

    public class StandardHeadersContants : IStandardHeadersConstants
    {
        public string Cookie { get { return "Cookie"; } }
        public string SetCookie { get { return "Set-Cookie"; } }
        public string ContentType { get { return "Content-Type"; } }
    }
}