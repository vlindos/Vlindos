using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mime;
using Framework.Web.Models.FiltersObjects;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public interface IHttpResponseFactory<TRequest>
    {
        IHttpResponse<TRequest> GetHttpResponse();
    }

    public interface IHttpResponse<TResponse>
    {
        HttpStatusCode HttpStatusCode { get; set; }

        NameValueCollection Headers { get; set; }

        ContentType ContentType { get; set; }

        IOutputStream OutputStream { get; set; }

        TResponse Response { get; set; }

        Dictionary<IFiltersObjectsBagGroup, List<object>> FiltersObjects { get; set; }
    }

    public class HttpResponse<TResponse> : IHttpResponse<TResponse>
    {
        public HttpResponse(IEnumerable<IFiltersObjectsBagGroup> filtersObjectsBagGroups)
        {
            HttpStatusCode = HttpStatusCode.OK;
            FiltersObjects = new Dictionary<IFiltersObjectsBagGroup, List<object>>();
            foreach (var filtersObjectsBagGroup in filtersObjectsBagGroups)
            {
                FiltersObjects.Add(filtersObjectsBagGroup, new List<object>());
            }
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public NameValueCollection Headers { get; set; }

        public ContentType ContentType { get; set; }

        public IOutputStream OutputStream { get; set; }

        public TResponse Response { get; set; }

        public Dictionary<IFiltersObjectsBagGroup, List<object>> FiltersObjects { get; set; }
    }
}