using System;
using System.Collections.Generic;
using System.Linq;
using Vlindos.Common.Logging;
using Vlindos.Webserver.Configuration;

namespace Vlindos.Webserver.Webserver
{
    public interface ITcpServer
    {
        bool Start(IEnumerable<Bind> binds, IHttpRequestProcessor requestProcessor);
        void Stop();
    }

    public class TcpServer : ITcpServer
    {
        private readonly ILogger _logger;
        private readonly IBinderFactory _binderFactory;
        private readonly List<IBinder> _binders;

        public TcpServer(ILogger logger, IBinderFactory binderFactory)
        {
            _logger = logger;
            _binderFactory = binderFactory;
            _binders = new List<IBinder>();
        }

        public bool Start(IEnumerable<Bind> binds, IHttpRequestProcessor requestProcessor)
        {
            foreach (var bind in binds)
            {
                try
                {
                    _binders.Add(_binderFactory.GetBinder(bind, requestProcessor));
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