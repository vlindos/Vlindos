using System.Collections.Generic;

namespace Framework.Web.Application
{
    public interface IRequestValidator
    {
        bool Validate<T>(T request, List<string> messages);
    }
}