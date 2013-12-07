using System.Collections.Generic;
using Framework.Web.Application;

namespace Framework.Web.DemoApp.Endpoints.ConfigAdder
{
    public interface IRequestValidator : IRequestValidator<int>
    {
    }

    public class RequestValidator : IRequestValidator
    {
        public bool Validate(int request, List<string> messages)
        {
            if (request == 0)
            {
                messages.Add("Cannot be 0");
                return false;
            }

            return true;
        }
    }
}