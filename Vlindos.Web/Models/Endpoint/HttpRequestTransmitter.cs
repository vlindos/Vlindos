using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Vlindos.Web.Models.Endpoint
{
    //public interface IHttpRequestTransmitter
    //{
    //    bool TransmitHttpRequest(IHttpRequest requestContext,
    //                             out StreamReader streamReader, List<string> messages);
    //}

    //public class HttpRequestTransmitter : IHttpRequestTransmitter
    //{
    //    private readonly IExtendedLogger _logger;

    //    public HttpRequestTransmitter(IExtendedLogger logger)
    //    {
    //        _logger = logger;
    //    }

    //    public bool TransmitHttpRequest(IHttpRequest requestContext,
    //        out StreamReader streamReader, List<string> messages)
    //    {
    //        var url = requestContext.GetUri();
    //        _logger.DebugFormat("Transporting request to '{0}'...", url);
    //        Stream responseStream;
    //        try
    //        {
    //            var webRequest = (HttpWebRequest)WebRequest.Create(url);
    //            webRequest.Method = requestContext.Method;
    //            if (!string.IsNullOrWhiteSpace(requestContext.HttpUsername))
    //            {
    //                webRequest.Credentials = new NetworkCredential(requestContext.HttpUsername,
    //                                                               requestContext.HttpPassword);
    //            }
    //            if (requestContext.Headers != null && requestContext.Headers.Count > 0)
    //            {
    //                foreach (var headerKey in requestContext.Headers.AllKeys)
    //                {
    //                    webRequest.Headers.Add(headerKey, requestContext.Headers[headerKey]);
    //                }
    //            }
    //            if (requestContext.PostData != null)
    //            {
    //                var stream = webRequest.GetRequestStream();
    //                foreach (var bytes in requestContext.PostData)
    //                {
    //                    stream.Write(bytes, 0, bytes.Length);
    //                }
    //            }

    //            var webResponse = (HttpWebResponse)webRequest.GetResponse();
    //            responseStream = webResponse.GetResponseStream();
    //        }
    //        catch (Exception ex)
    //        {
    //            messages.Add("Unable to retreive response data from the network. Exception:\r\n" + ex.Message);
    //            streamReader = null;
    //            return false;
    //        }
    //        if (responseStream == null)
    //        {
    //            messages.Add("Failed to obtain response from the connection.");
    //            streamReader = null;
    //            return false;
    //        }
    //        streamReader = new StreamReader(responseStream);
    //        return true;
    //    }
    //}
}
