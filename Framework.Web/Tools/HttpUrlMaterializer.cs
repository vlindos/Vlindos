using System;
using System.Linq;
using System.Net;
using System.Text;
using Framework.Web.Models;

namespace Framework.Web.Tools
{
    public interface IHttpUrlMaterializer
    {
        Uri MaterializeHttpUrl(IHttpRequest httpRequest);
    }

    public class HttpUrlMaterializer : IHttpUrlMaterializer
    {
        public Uri MaterializeHttpUrl(IHttpRequest httpRequest)
        {	
            var sb = new StringBuilder();
            var rawUrl = httpRequest.RawUrl;
            if (httpRequest.RoutesValues != null)
            {
                rawUrl = httpRequest.RoutesValues.AllKeys.Aggregate(
                    rawUrl, 
                    (current, routesValueKey) => current.Replace("{" + routesValueKey + "}", 
                        httpRequest.RoutesValues[routesValueKey]));
            }
            sb.Append(rawUrl);
            
            if (httpRequest.QueryString == null) return new Uri(sb.ToString());

            for (var i = 0; i < httpRequest.QueryString.Count; i++)
            {
                var key = httpRequest.QueryString.AllKeys[i];
                // http://stackoverflow.com/questions/575440/url-encoding-using-c-sharp
                sb.AppendFormat("{0}{1}={2}", 
                    (i == 0 ? "?" : "&"), key, WebUtility.UrlEncode(httpRequest.QueryString[key]));
            }

            return new Uri(sb.ToString());
        }
    }
}
