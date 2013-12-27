using System.Collections.Generic;

namespace Framework.Web.Service.Rest
{
    public interface IRequestValidator<in TRequest>
    {
        bool Validate(TRequest request, List<string> messages);
    }
}