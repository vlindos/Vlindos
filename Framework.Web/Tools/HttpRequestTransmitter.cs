﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Framework.Web.Application;
using Vlindos.Common.Logging;

namespace Framework.Web.Tools
{
    public interface IHttpRequestTransmitter
    {
        bool TransmitHttpRequest(
            WebServiceSettings settings,
            HttpRequest httpRequest, 
            List<string> messages, 
            out StreamReader streamReader);
    }

    public class HttpRequestTransmitter : IHttpRequestTransmitter
    {
        private readonly ILogger _logger;
        private readonly IHttpUrlMaterializer _httpUrlMaterializer;

        public HttpRequestTransmitter(ILogger logger, IHttpUrlMaterializer httpUrlMaterializer)
        {
            _logger = logger;
            _httpUrlMaterializer = httpUrlMaterializer;
        }
        
        public bool TransmitHttpRequest(
            WebServiceSettings settings, 
            HttpRequest httpRequest, 
            List<string> messages,
            out StreamReader streamReader)
        {
            var url = _httpUrlMaterializer.MaterializeHttpUrl(settings, httpRequest);
            _logger.Debug("Transporting request to '{0}'...", url);
            Stream responseStream;
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = httpRequest.HttpMethod.ToString();
                if (httpRequest.Headers != null && httpRequest.Headers.Count > 0)
                {
                    foreach (var headerKey in httpRequest.Headers.AllKeys)
                    {
                        webRequest.Headers.Add(headerKey, httpRequest.Headers[headerKey]);
                    }
                }
                // TODO: Fix this case
                // probably httpRequest needs client implementation
                //if (httpRequest.PostData != null)
                //{
                //    var stream = webRequest.GetRequestStream();
                //    foreach (var bytes in httpRequest.PostData)
                //    {
                //        stream.Write(bytes, 0, bytes.Length);
                //    }
                //}

                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                responseStream = webResponse.GetResponseStream();
            }
            catch (Exception ex)
            {
                messages.Add("Unable to retreive response data from the network. Exception:" 
                    + Environment.NewLine + ex.Message);
                streamReader = null;
                return false;
            }
            if (responseStream == null)
            {
                messages.Add("Failed to obtain response from the connection.");
                streamReader = null;
                return false;
            }
            streamReader = new StreamReader(responseStream);
            return true;
        }
    }
}
