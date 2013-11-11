﻿using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Logging;
using Microsoft.Practices.ServiceLocation;

namespace Users.Common.Attributes
{
    public class EndpointExceptionResultAttribute : HandleErrorAttribute
    {
        private readonly IExtendedLogger _logger;

        public EndpointExceptionResultAttribute()
        {
            _logger = ServiceLocator.Current.GetInstance<IExtendedLogger>();
        }

        public override void OnException(ExceptionContext filterContext)
        {
            _logger.ErrorFormat(filterContext.Exception, "Exception in Controller: {0}",
                filterContext.Controller);

            filterContext.ExceptionHandled = true;

            filterContext.Result = new RedirectToRouteResult("InternalServerError", new RouteValueDictionary());
        }
    }
}
