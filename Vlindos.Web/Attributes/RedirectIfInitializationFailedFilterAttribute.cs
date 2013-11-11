using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using Spreed.Common.Initialization;

namespace Users.Common.Attributes
{
    public class RedirectIfInitializationFailedFilterAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        readonly string _controller;
        readonly string _action;

        readonly IInitializationContext _initializationContext;
        static bool _initialized;

        public RedirectIfInitializationFailedFilterAttribute(string controller, string action)
        {
            if (_initialized)
                return;

            if (string.IsNullOrWhiteSpace(controller) || string.IsNullOrWhiteSpace(action))
                throw new Exception("'controller' and 'action' parameters must contain non-empty values");

            _controller = controller;
            _action = action;

            try
            {
                _initializationContext = ServiceLocator.Current.GetInstance<IInitializationContext>();
            }
            catch (Exception ex)
            {
                var exception = 
                    new InvalidOperationException("IInitializationContext not found in container. " +
                                                  "Did you forget to add " +
                                                  "Spreed.Common.Initialization.InitializationFacility to the container?", 
                                                  ex);
                throw exception;
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (_initialized)
                return;

            if (checkIfRedirectRoute(filterContext.RouteData.Values))
                return;

            if (_initializationContext.AggregateState != InitializationState.Success)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = _controller, action = _action }));
            else
                _initialized = true;
        }

        bool checkIfRedirectRoute(RouteValueDictionary routeDictionary)
        {
            if (routeDictionary["controller"].ToString() == _controller && routeDictionary["action"].ToString() == _action)
                return true;

            return false;
        }
    }
}
