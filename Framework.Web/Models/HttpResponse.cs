using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mime;
using Framework.Web.Models.FiltersObjects;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public class HttpResponse
    {
        public HttpResponse()
        {
            HttpStatusCode = HttpStatusCode.OK;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public NameValueCollection Headers { get; set; }

        public ContentType ContentType { get; set; }

        public IOutputStream OutputStream { get; set; }

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
}