namespace Framework.Web.Authentication
{
    public interface IAuthenticationSessionContants
    {
        string Authenticated { get; }
        string ReturnUrl { get; }
        string Username { get; }
    }

    public class AuthenticationSessionConstants : IAuthenticationSessionContants
    {
        public string Authenticated { get { return "Authenticated"; } }
        public string ReturnUrl { get { return "ReturnUrl"; } }
        public string Username { get { return "Username"; } }
    }
}