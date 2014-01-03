namespace Vlindos.Common.Models
{
    public interface IUserRepository
    {
        bool Authenticate(string username, string password);
    }
}