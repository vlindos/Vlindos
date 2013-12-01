using System.Collections.Generic;

namespace Framework.Web.Application.Endpoints.Models
{
    public interface IEndpointResponse
    {
        bool Success { get; set; }

        List<string> Messages { get; set; }
    }
}
