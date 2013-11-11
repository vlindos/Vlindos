using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Users.Common.Attributes
{
    public class ApiKeyFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        readonly string _controller;
        readonly string _action;
        public ApiKeyFilter(string controller, string action)
        {
            if (string.IsNullOrWhiteSpace(controller) || string.IsNullOrWhiteSpace(action))
                throw new Exception("'controller' and 'action' parameters must contain non-empty values");

            _controller = controller;
            _action = action;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CheckIfRedirectRoute(filterContext.RouteData.Values)) // destined controller / action is always allowed
                return;
            var apiKey = GetApiKey(filterContext);
            if (CheckIfApiKeyValid(apiKey))
            {
                return;
            }

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = _controller, action = _action }));
        }

        private Guid GetApiKey(AuthorizationContext filterContext)
        {
            Guid apiKey;

            var apiKeyString = filterContext.RouteData.Values["ApiKey"] as string ?? filterContext.RouteData.Values["apikey"] as string; 
            if (apiKeyString != null)
            {
                if (Guid.TryParse(apiKeyString, out apiKey))
                {
                    return apiKey;
                }
            }

            apiKeyString = filterContext.HttpContext.Request.Headers["ApiKey"] ?? filterContext.HttpContext.Request.Headers["apikey"];
            if (apiKeyString != null)
            {
                if (Guid.TryParse(apiKeyString, out apiKey))
                {
                    return apiKey;
                }
            }

            apiKeyString = filterContext.HttpContext.Request.QueryString["ApiKey"] ?? filterContext.HttpContext.Request.QueryString["apikey"];
            if (apiKeyString != null)
            {
                if (Guid.TryParse(apiKeyString, out apiKey))
                {
                    return apiKey;
                }
            }

            return default(Guid);
        }

        bool CheckIfRedirectRoute(RouteValueDictionary routeDictionary)
        {
            if (routeDictionary["controller"].ToString() == _controller && routeDictionary["action"].ToString() == _action)
                return true;

            return false;
        } 

        protected bool CheckIfApiKeyValid(Guid apiKey)
        {
            return !apiKey.Equals(default(Guid));
        }

    }
}