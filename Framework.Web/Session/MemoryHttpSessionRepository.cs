using System.Collections.Concurrent;

namespace Framework.Web.Session
{
    public interface IHttpSessionRepository
    {
        IHttpSession GetHttpSession(string sessionId);
    }

    public class MemoryHttpSessionRepository : IHttpSessionRepository
    {
        private readonly IHttpSessionFactory _httpSessionFactory;

        private readonly ConcurrentDictionary<string, IHttpSession> _repository; 
        public MemoryHttpSessionRepository(IHttpSessionFactory httpSessionFactory)
        {
            _httpSessionFactory = httpSessionFactory;
            _repository = new ConcurrentDictionary<string, IHttpSession>();
        }

        public IHttpSession GetHttpSession(string sessionId)
        {
            return _repository.GetOrAdd(sessionId, _httpSessionFactory.GetHttpSession());
        }
    }
}