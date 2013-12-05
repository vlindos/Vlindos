namespace Vlindos.Common.Models
{
    public interface IUsernamePassword
    {
        string Username { get; set; }
        string Password { get; set; }
    }

    public class UsernamePassword
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
