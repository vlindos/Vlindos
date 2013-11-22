using System;

namespace Vlindos.Web.Mvc
{
    //public class WindsorControllerFactory : DefaultControllerFactory
    //{
    //    private readonly IWindsorContainer _container;
    //    private readonly WindsorControllerActionInvoker _actionInvoker;

    //    public WindsorControllerFactory(IWindsorContainer container)
    //    {
    //        _container = container;
    //        _actionInvoker = new WindsorControllerActionInvoker(_container);
    //    }

    //    public override void ReleaseController(IController controller)
    //    {
    //        _container.Kernel.ReleaseComponent(controller);
    //    }

    //    public override IController CreateController(RequestContext requestContext, string controllerName)
    //    {
    //        var requestType = requestContext.RouteData.Values["requestType"] as Type;
    //        var responseType = requestContext.RouteData.Values["responseType"] as Type;
    //        Type controllerType;
    //        if (requestType == null || responseType == null)
    //        {
    //            if (controllerName != "EndpointFailure")
    //            {
    //                return base.CreateController(requestContext, controllerName);
    //            }

    //            controllerType = typeof (IEndpointFailureController);
    //        }
    //        else
    //        {
    //            controllerType = typeof(IEndpointController<,>).MakeGenericType(new[] { requestType, responseType });   
    //        }
    //        var controller = (Controller)_container.Resolve(controllerType);
    //        controller.ActionInvoker = _actionInvoker;

    //        return controller;
    //    }

    //    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
    //    {
    //        if (controllerType != null)
    //        {
    //            var controller = (Controller) _container.Resolve(controllerType);
    //            controller.ActionInvoker = _actionInvoker;
    //            return controller;
    //        }
    //        return base.GetControllerInstance(requestContext, null);
    //    }
    //}
}