using System.Collections.Generic;

namespace Vlindos.Web.Models.Endpoint
{
    public interface IRequestValidator<in T> where T : IEndpointRequest
    {
        bool Validate(T request, List<string> messages);
    }
}