﻿using Framework.Web.Models.HttpMethods;

namespace Framework.Web.Application.HttpEndpoint.Models
{
    public class HttpUrlDescription
    {
        public IHttpMethod[] HttpMethods { get; set; }

        public string Path { get; set; }
    }
}