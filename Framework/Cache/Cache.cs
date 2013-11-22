using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Timers;

namespace Vlindos.Common.Cache
{
    /// <summary>
    /// Called when ICache expiration event raises
    /// </summary>
    /// <typeparam name="T">The generic argument of the ICache</typeparam>
    /// <param name="output">In case callback returns 'true' value, 
    /// it should update the value of the argument, 
    /// which will be used to update the cache item</param>
    /// <returns>If returned true output parameter will be used to update the cache item, 
    /// otherwise it will be deleted</returns>
    public delegate bool CacheUpdateCallback<T>(out T output)
        where T : class, new();

    public interface ICache<T>
        where T : class, new()
    {
        void Set(string key, T item, TimeSpan? expiration = null, CacheUpdateCallback<T> expirationCallback = null);
        T GetOrAdd(string key, CacheUpdateCallback<T> getValueCallback, TimeSpan? expiration = null, CacheUpdateCallback<T> expirationCallback = null);
        bool TryRemove(string key, out T item);
    }

    public class Cache<T> : IDisposable, ICache<T>
        where T : class, new()
    {
        private readonly ConcurrentDictionary<string, T> _cache;
        private readonly Dictionary<string, Timer> _timers;
        private readonly Dictionary<string, CacheUpdateCallback<T>> _callbacks;

        public Cache()
        {
            _cache = new ConcurrentDictionary<string, T>();
            _timers = new Dictionary<string, Timer>();
            _callbacks = new Dictionary<string, CacheUpdateCallback<T>>();
        }

        protected void TimerCallback(string key)
        {
            T x;
            if (!_callbacks.ContainsKey(key))
            {
                _cache.TryRemove(key, out x);
                return;
            }
            var expirationCallback = _callbacks[key];
            if (expirationCallback(out x))
            {
                _cache.TryUpdate(key, x, x);
            }
            else
            {
                _cache.TryRemove(key, out x);
            }
        }

        protected Timer GetTimer(string key, TimeSpan expiration)
        {
            var timer = new Timer { Interval = expiration.TotalMilliseconds, Enabled = true };
            timer.Elapsed += (sender, args) => TimerCallback(key);
            timer.Start();

            return timer;
        }

        public void Set(string key, T item, TimeSpan? expiration = null, CacheUpdateCallback<T> expirationCallback = null)
        {
            _cache.AddOrUpdate(key, k =>
            {
                if (expiration.HasValue)
                {
                    _timers.Add(key, GetTimer(key, expiration.Value));

                    if (expirationCallback != null)
                    {
                        _callbacks.Add(key, expirationCallback);
                    }
                }

                return item;
            },
                (k, e) =>
                {
                    if (_timers.ContainsKey(key))
                    {
                        _timers[key].Stop();
                        if (!expiration.HasValue)
                        {
                            _timers.Remove(key);
                        }
                        else
                        {
                            _timers[key].Interval = expiration.Value.TotalMilliseconds;
                            _timers[key].Start();
                        }
                    }
                    else if (expiration.HasValue)
                    {
                        _timers.Add(key, GetTimer(key, expiration.Value));
                    }

                    if (expirationCallback != null)
                    {
                        _callbacks[key] = expirationCallback;
                    }
                    else if (_callbacks.ContainsKey(key))
                    {
                        _callbacks.Remove(key);
                    }

                    return item;
                });
        }

        public T GetOrAdd(string key,
            CacheUpdateCallback<T> getValueCallback,
            TimeSpan? expiration = null,
            CacheUpdateCallback<T> expirationCallback = null)
        {
            T item;
            // first try to get value from the cache
            if (_cache.TryGetValue(key, out item))
            {
                return item;
            }
            // then try to get the value from the dedicated callback
            if (getValueCallback(out item))
            {
                Set(key, item, expiration, expirationCallback);
                return item;
            }
            // if callback report failure avoid updating the cache
            return item;
        }

        public bool TryRemove(string key, out T item)
        {
            return _cache.TryRemove(key, out item);
        }

        public void Dispose()
        {
            _cache.Clear();
            _callbacks.Clear();
            foreach (var timer in _timers.Values)
            {
                timer.Stop();
            }
            _timers.Clear();
        }
    }
}
