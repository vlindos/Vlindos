using System.Collections.Generic;

namespace Framework.Web.Application.Endpoints
{
    public interface IRequestValidator<in T>
    {
        bool Validate(T request, List<string> messages);
    }
}