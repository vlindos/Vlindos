using System.Collections.Generic;

namespace Users.Common.Models.Endpoint
{
    public interface IRequestValidator<in T> where T : IEndpointRequest
    {
        bool Validate(T request, List<string> messages);
    }
}