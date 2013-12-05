﻿using System.Net.Mime;

namespace Framework.Web.Models
{
    public class StandardHeaders
    {
        public string HttpUsername { get; set; }

        public string HttpPassword { get; set; }

        public string UserAgent { get; set; }

        public ContentType ContentType { get; set; }
    }
}