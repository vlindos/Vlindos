using System.Collections.Generic;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IRequestValidator<in TRequest>
    {
        bool Validate(TRequest request, List<string> messages);
    }
}