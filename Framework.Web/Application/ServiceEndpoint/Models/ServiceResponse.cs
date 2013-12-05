using System.Collections.Generic;

namespace Framework.Web.Application.ServiceEndpoint.Models
{
    public interface IServiceResponse
    {
        bool Success { get; set; }

        List<string> Messages { get; set; }
    }

    public class ServiceResponse : IServiceResponse
    {
        public ServiceResponse()
        {
            Messages = new List<string>();
        }

        public bool Success { get; set; }
        public List<string> Messages { get; set; }
    }
}