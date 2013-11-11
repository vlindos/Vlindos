using System;
using System.Collections.Generic;
using System.Linq;
using Vlindos.Common.Logging;

namespace Vlindos.Webserver.Webserver
{
    public interface IWebserver
    {
        bool Start(Configuration.Configuration configuration);
        void Stop();
    }

    public class Webserver : IWebserver
    {
        private readonly ILogger _logger;
        private readonly IBinderFactory _binderFactory;
        private readonly List<IBinder> _binders;

        public Webserver(ILogger logger,
            IRequestProcessor requestProcessor, IBinderFactory binderFactory)
        {
            _logger = logger;
            _binderFactory = binderFactory;
            _binders = new List<IBinder>();
        }

        public bool Start(Configuration.Configuration configuration)
        {
            var binds = configuration.Websites
                .SelectMany(x => x.Value.Binds)
                .Select(x => x.Value)
                .ToArray();
            foreach (var bind in binds)
            {
                try
                {
                    _binders.Add(_binderFactory.GetBinder(configuration.NetworkSettings, bind));
                }
                catch (Exception exception)
                {
                    _logger.Fatal(exception, "Unable to bind to {0}:{1}", bind.IpAddress, bind.Port);
                    _binders.ForEach(x => x.Unbind());

                    return false;
                }
            }

            return true;
        }

        public void Stop()
        {
            _binders.ForEach(x => x.Unbind());
        }
    }
}