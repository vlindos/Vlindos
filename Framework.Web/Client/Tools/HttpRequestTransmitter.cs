using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Framework.Web.Models;
using Vlindos.Common.Logging;

namespace Framework.Web.Client.Tools
{
    public interface IHttpRequestTransmitter
    {
        bool TransmitHttpRequest(HttpRequest requestContext,
                                 out StreamReader streamReader, List<string> messages);
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

        public bool TransmitHttpRequest(HttpRequest requestContext,
            out StreamReader streamReader, List<string> messages)
        {
            var url = _httpUrlMaterializer.MaterializeHttpUrl(requestContext);
            _logger.Debug("Transporting request to '{0}'...", url);
            Stream responseStream;
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = requestContext.HttpMethod.ToString();
                if (!string.IsNullOrWhiteSpace(requestContext.HttpUsername))
                {
                    webRequest.Credentials = new NetworkCredential(requestContext.HttpUsername,
                                                                   requestContext.HttpPassword);
                }
                if (requestContext.Headers != null && requestContext.Headers.Count > 0)
                {
                    foreach (var headerKey in requestContext.Headers.AllKeys)
                    {
                        webRequest.Headers.Add(headerKey, requestContext.Headers[headerKey]);
                    }
                }
                if (requestContext.PostData != null)
                {
                    var stream = webRequest.GetRequestStream();
                    foreach (var bytes in requestContext.PostData)
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

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
