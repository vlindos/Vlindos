using System.Collections.Generic;
using System.Collections.Specialized;
using Framework.Web.Models.FiltersObjects;
using Framework.Web.Models.HttpMethods;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public interface IHttpRequestFactory<TRequest>
    {
        IHttpRequest<TRequest> GetHttpRequest();
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

        Dictionary<IFiltersObjectsBagGroup, List<object>> FiltersObjects { get; set; }
    }

    public class HttpRequest<TRequest> : IHttpRequest<TRequest>
    {
        public HttpRequest(IEnumerable<IFiltersObjectsBagGroup> filtersObjectsBagGroups)
        {
            FiltersObjects = new Dictionary<IFiltersObjectsBagGroup, List<object>>();
            foreach (var filtersObjectsBagGroup in filtersObjectsBagGroups)
            {
                FiltersObjects.Add(filtersObjectsBagGroup, new List<object>());
            }
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

        public Dictionary<IFiltersObjectsBagGroup, List<object>> FiltersObjects { get; set; }
    }
}
