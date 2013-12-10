using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mime;
using Framework.Web.Models.FiltersObjects;
using Vlindos.Common.Extensions.IEnumerable;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public interface IHttpResponseFactory
    {
        IHttpResponse<TRequest> GetHttpResponse<TRequest>();
    }

    public interface IHttpResponse<TResponse>
    {
        HttpStatusCode HttpStatusCode { get; set; }

        NameValueCollection Headers { get; set; }

        ContentType ContentType { get; set; }

        IOutputStream OutputStream { get; set; }

        TResponse Response { get; set; }

        Dictionary<IFiltersObjectsGroup, List<object>> FiltersObjects { get; }
    }

    public class HttpResponse<TResponse> : IHttpResponse<TResponse>
    {
        public HttpResponse(IEnumerable<IFiltersObjectsGroup> filtersObjectsGroups)
        {
            FiltersObjects = new Dictionary<IFiltersObjectsGroup, List<object>>();
            filtersObjectsGroups.ForEach(x => FiltersObjects.Add(x, new List<object>()));

            HttpStatusCode = HttpStatusCode.OK;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public NameValueCollection Headers { get; set; }

        public ContentType ContentType { get; set; }

        public IOutputStream OutputStream { get; set; }

        public TResponse Response { get; set; }

        public Dictionary<IFiltersObjectsGroup, List<object>> FiltersObjects { get; private set; }
    }
}