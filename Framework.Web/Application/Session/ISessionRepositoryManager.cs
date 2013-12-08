using System.Collections.Generic;

namespace Framework.Web.Application.Session
{
    public interface ISessionRepositoryManager
    {
        Dictionary<string, string> GetRepository(SessionValue sessionValue);
        void PersistSession(SessionValue sessionValue, Dictionary<string, string> session);
    }
}
