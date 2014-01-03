using System.Text;
using Framework.Web.Application;

namespace Framework.Web.Tools
{
    public interface IBaseUrlGenerator
    {
        string GenerateUrlBase(HttpRequest httpRequest);
    }

    public class BaseUrlGenerator : IBaseUrlGenerator
    {
        public string GenerateUrlBase(HttpRequest httpRequest)
        {
            var sb = new StringBuilder();
            sb.Append(!httpRequest.UsesSsl ? "http" : "https");
            sb.Append("://");
            sb.Append(httpRequest.ServerDomain);

            if ((!httpRequest.UsesSsl && httpRequest.Port == 80) ||
                (httpRequest.UsesSsl && httpRequest.Port == 443))
            {
                return sb.ToString();   
            }
            
            sb.Append(":");
            sb.Append(httpRequest.Port);

            return sb.ToString();
        }
    }
}