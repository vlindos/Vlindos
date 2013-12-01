using System.Collections.Generic;
using Framework.Web.Application.Endpoints.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IRequestValidator<in T> where T : IEndpointRequest
    {
        bool Validate(T request, List<string> messages);
    }
}