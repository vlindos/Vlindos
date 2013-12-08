﻿using System;
using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Service
{
    public interface IJsonHttpStreamResponseReader<TResponse> : IHttpStreamResponseReader<TResponse>
    {
    }

    public class JsonHttpStreamResponseReader<TResponse> : IJsonHttpStreamResponseReader<TResponse>
    {
        public bool Read(IHttpResponse<TResponse> httpResponse, List<string> messages)
        {
            httpResponse.Response = default(TResponse);
            try
            {
                //var serializer = new JsonSerializer();
                //var reader = new JsonTextReader(httpRequest.InputStream);
                //response = serializer.Deserialize<T>(reader);
            }
            catch (Exception ex)
            {
                messages.Add("Unable to deserialize response to JSON. Exception:\r\n" + ex.Message);
                return false;
            }

            return true;
        }
    }
}
