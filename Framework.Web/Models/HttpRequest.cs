using System.Collections.Generic;
using System.Collections.Specialized;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models.FiltersObjects;
using Framework.Web.Models.HttpMethods;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public interface IHttpRequestFactory
    {
        IHttpRequest<TRequest> GetHttpRequest<TRequest>();
        IHttpRequest GetHttpRequest();
    }

    public interface IHttpRequest
    {
        IHttpMethod HttpMethod { get; set; }

        IEnumerable<byte[]> PostData { get; set; }

        string UserHostAddress { get; set; }

        string RawUrl { get; set; }

        NameValueCollection Headers { get; set; }

        NameValueCollection RoutesValues { get; set; }

        NameValueCollection QueryString { get; set; }

        IInputStream InputStream { get; set; }

        Dictionary<string, string> Session { get; set; }

        IServerSideHttpEndpoint Endpoint { get; set; }

        Dictionary<IFiltersObjectsGroup, List<object>> FiltersObjects { get; }
    }

    public interface IHttpRequest<TRequest> : IHttpRequest
    {
        TRequest Request { get; set; }
    }

    public class HttpRequest : IHttpRequest
    {
        public IHttpMethod HttpMethod { get; set; }

        public IEnumerable<byte[]> PostData { get; set; }

        public string UserHostAddress { get; set; }

        public string RawUrl { get; set; }
        
        public NameValueCollection Headers { get; set; }

        public NameValueCollection RoutesValues { get; set; }

        public NameValueCollection QueryString { get; set; }

        public IInputStream InputStream { get; set; }

        public IServerSideHttpEndpoint Endpoint { get; set; }

        private Dictionary<string, string> _session;
        public Dictionary<string, string> Session
        {
            get
            {
                return _session ?? (_session = new Dictionary<string, string>());
            }
            set
            {
                _session = value;
            }
        }

        private Dictionary<IFiltersObjectsGroup, List<object>> _filtersObjects;
        public Dictionary<IFiltersObjectsGroup, List<object>> FiltersObjects
        {
            get
            {
                return _filtersObjects ?? (_filtersObjects = new Dictionary<IFiltersObjectsGroup, List<object>>());
            }
            set
            {
                _filtersObjects = value;
            }
        }
    }

    public class HttpRequest<TRequest> : HttpRequest, IHttpRequest<TRequest>
    {
        public TRequest Request { get; set; }
    }
}
