using System.Collections.Generic;

namespace Framework.Web.Session
{
    public interface IHttpSessionFactory
    {
        IHttpSession GetHttpSession();
    }

    public interface IHttpSession
    {
        void Remove(string key);
        string this[string key] { get; set; }
    }

    public class MemoryHttpSession : IHttpSession
    {
        private readonly Dictionary<string, string> _dictionary;

        public MemoryHttpSession()
        {
            _dictionary = new Dictionary<string, string>();
        }
        public void Remove(string key)
        {
            _dictionary.Remove(key);
        }

        public string this[string key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }
    }
}