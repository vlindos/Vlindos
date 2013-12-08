using System;
using System.Linq;
using System.Net;
using System.Text;
using Framework.Web.Models;

namespace Framework.Web.Tools
{
    public interface IHttpUrlMaterializer
    {
        Uri MaterializeHttpUrl<TRequest>(IHttpRequest<TRequest> requestContext);
    }

    public class HttpUrlMaterializer : IHttpUrlMaterializer
    {
        public Uri MaterializeHttpUrl<TRequest>(IHttpRequest<TRequest> requestContext)
        {	
            var sb = new StringBuilder();
            var rawUrl = requestContext.RawUrl;
            if (requestContext.RoutesValues != null)
            {
                rawUrl = requestContext.RoutesValues.AllKeys.Aggregate(
                    rawUrl, 
                    (current, routesValueKey) => current.Replace("{" + routesValueKey + "}", 
                        requestContext.RoutesValues[routesValueKey]));
            }
            sb.Append(rawUrl);
            if (requestContext.QueryString != null)
            {
                for (var i = 0; i < requestContext.QueryString.Count; i++)
                {
                    var key = requestContext.QueryString.AllKeys[i];
                    // http://stackoverflow.com/questions/575440/url-encoding-using-c-sharp
                    sb.AppendFormat("{0}{1}={2}", 
                        (i == 0 ? "?" : "&"), key, WebUtility.UrlEncode(requestContext.QueryString[key]));
                }
            }
	            
            return new Uri(sb.ToString());
        }
    }
}
