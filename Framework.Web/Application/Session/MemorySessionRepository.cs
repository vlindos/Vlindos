using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Framework.Web.Application.Session
{
    public class MemorySessionRepository : ISessionRepositoryManager
    {
        private readonly ConcurrentDictionary<SessionValue, Dictionary<string, string>> _cache;

        public MemorySessionRepository()
        {
            _cache = new ConcurrentDictionary<SessionValue, Dictionary<string, string>>();
        }
        public Dictionary<string, string> GetRepository(SessionValue sessionValue)
        {
            return _cache.GetOrAdd(sessionValue, x => new Dictionary<string, string>());
        }

        public void PersistSession(SessionValue sessionValue, Dictionary<string, string> session)
        {
            _cache.AddOrUpdate(sessionValue, x => session, (x, y) => session);
        }
    }
}
