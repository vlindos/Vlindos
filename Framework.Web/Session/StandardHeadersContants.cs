namespace Framework.Web.Session
{
    public interface IStandardHeadersConstants
    {
        string SetCookie { get; }
    }

    public class StandardHeadersContants : IStandardHeadersConstants
    {
        public string SetCookie { get { return "Set-Cookie"; }}
    }
}