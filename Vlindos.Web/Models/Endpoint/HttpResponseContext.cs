﻿using System.Net;

namespace Vlindos.Web.Models.Endpoint
{
    public interface IHttpResponseContext
    {
        HttpStatusCode HttpStatusCode { get; set; }
    }

    public class HttpResponseContext : IHttpResponseContext
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public HttpResponseContext()
        {
            HttpStatusCode = HttpStatusCode.OK;
        }
    }
}
