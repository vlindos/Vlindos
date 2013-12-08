using System.Collections.Generic;
using System.Collections.Specialized;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models.FiltersObjects;
using Framework.Web.Models.HttpMethods;
using Vlindos.Common.Extensions.IEnumerable;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public interface IHttpRequestFactory
    {
        IHttpRequest<TRequest> GetHttpRequest<TRequest>();
    }

    public interface IHttpRequest<TRequest>
    {
        IHttpMethod HttpMethod { get; set; }

        IEnumerable<byte[]> PostData { get; set; }

        string UserHostAddress { get; set; }

        string RawUrl { get; set; }

        NameValueCollection Headers { get; set; }

        NameValueCollection RoutesValues { get; set; }

        NameValueCollection QueryString { get; set; }

        IInputStream InputStream { get; set; }

        TRequest Request { get; set; }
        
        Dictionary<string, string> Session { get; set; }

        IServerSideHttpEndpoint Endpoint { get; set; }

        Dictionary<IFiltersObjectsGroup, List<object>> FiltersObjects { get; }
    }

    public class HttpRequest<TRequest> : IHttpRequest<TRequest>
    {
        public HttpRequest(IEnumerable<IFiltersObjectsGroup> filtersObjectsGroups)
        {
            FiltersObjects = new Dictionary<IFiltersObjectsGroup, List<object>>();
            filtersObjectsGroups.ForEach(x => FiltersObjects.Add(x, new List<object>()));
        }

        public IHttpMethod HttpMethod { get; set; }

        public IEnumerable<byte[]> PostData { get; set; }

        public string UserHostAddress { get; set; }

        public string RawUrl { get; set; }
        
        public NameValueCollection Headers { get; set; }

        public NameValueCollection RoutesValues { get; set; }

        public NameValueCollection QueryString { get; set; }

        public IInputStream InputStream { get; set; }

        public TRequest Request { get; set; }

        public Dictionary<string, string> Session { get; set; }

        public IServerSideHttpEndpoint Endpoint { get; set; }

        public Dictionary<IFiltersObjectsGroup, List<object>> FiltersObjects { get; private set; }
    }
}
