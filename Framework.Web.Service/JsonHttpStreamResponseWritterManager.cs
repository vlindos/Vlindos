using System;
using System.Collections.Generic;

namespace Framework.Web.Service
{
    public interface IJsonHttpStreamResponseWritterManager
    {
        IJsonHttpStreamResponseWritter<T> GetJsonHttpStreamResponseWritter<T>();
    }

    public class JsonHttpStreamResponseWritterManager : IJsonHttpStreamResponseWritterManager
    {
        private readonly IJsonHttpStreamResponseWritterFactory _factory;
        private Dictionary<Type, object> _cache;

        public JsonHttpStreamResponseWritterManager(IJsonHttpStreamResponseWritterFactory factory)
        {
            _factory = factory;
            _cache = new Dictionary<Type, object>();
        }

        public IJsonHttpStreamResponseWritter<T> GetJsonHttpStreamResponseWritter<T>()
        {
            lock (_cache)
            {
                if (!_cache.ContainsKey(typeof (T)))
                {
                    _cache[typeof (T)] = _factory.GetJsonHttpStreamResponseWritter<T>();
                }

                return (IJsonHttpStreamResponseWritter<T>)_cache[typeof(T)];
            }
        }
    }
}