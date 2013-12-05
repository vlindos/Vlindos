using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Framework.Web.Models;
using Framework.Web.Tools;
using Vlindos.Common.Logging;
using Vlindos.Common.Models;

namespace Framework.Web.Client.Tools
{
    public interface IHttpRequestTransmitter
    {
        bool TransmitHttpRequest(
            IHttpRequest httpRequest,
            IUsernamePassword usernamePassword,
            out StreamReader streamReader,
            List<string> messages);
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
            IHttpRequest httpRequest, 
            IUsernamePassword usernamePassword,
            out StreamReader streamReader, 
            List<string> messages)
        {
            var url = _httpUrlMaterializer.MaterializeHttpUrl(httpRequest);
            _logger.Debug("Transporting request to '{0}'...", url);
            Stream responseStream;
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = httpRequest.HttpMethod.ToString();
                if (!string.IsNullOrWhiteSpace(usernamePassword.Username))
                {
                    webRequest.Credentials = new NetworkCredential(usernamePassword.Username,
                                                                   usernamePassword.Password);
                }
                if (httpRequest.Headers != null && httpRequest.Headers.Count > 0)
                {
                    foreach (var headerKey in httpRequest.Headers.AllKeys)
                    {
                        webRequest.Headers.Add(headerKey, httpRequest.Headers[headerKey]);
                    }
                }
                if (httpRequest.PostData != null)
                {
                    var stream = webRequest.GetRequestStream();
                    foreach (var bytes in httpRequest.PostData)
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
