using System;

namespace Framework.Web.Session
{
    public interface ISessionValueReader
    {
        SessionValue ReadSessionValue(string sessionValue);
    }

    public class SessionValueReader : ISessionValueReader
    {
        public SessionValue ReadSessionValue(string sessionValue)
        {
            throw new NotImplementedException();
        }
    }
}