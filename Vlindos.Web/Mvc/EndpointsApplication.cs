using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Logging;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using Spreed.Common.Web.Mvc;
using Spreed.Common.Windsor;
using Users.Common.Attributes;
using Users.Common.Models.Endpoint;

namespace Users.Common.Mvc
{
    public abstract class EndpointsApplication : MvcApplicationBase, IContainerAccessor
    {
        static IWindsorContainer _container;
        public IWindsorContainer Container
        {
            get { return _container ?? (_container = new WindsorContainer()); }
        }

        IExtendedLogger _logger = NullLogger.Instance;
        protected override IExtendedLogger Logger
        {
            get
            {
                if (_logger == NullLogger.Instance && Container.Kernel.HasComponent(typeof(IExtendedLoggerFactory)))
                    _logger = Container.Resolve<IExtendedLoggerFactory>().Create(GetType().BaseType);

                return _logger;
            }
        }

        protected void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new EndpointExceptionResultAttribute());
        }

        protected abstract InstallerBase ApplicationInstaller { get; }

        protected void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute("State", "State", 
                new { controller = "EndpointFailure", action = "State" });
            routes.MapRoute("InternalServerError", "InternalServerError", 
                new { controller = "EndpointFailure", action = "InternalServerError" });
            routes.MapRoute("SslIsRequired", "Require/Ssl",
                new { controller = "EndpointFailure", action = "SslIsRequired" });
            routes.MapRoute("ApiKeyIsRequired", "Require/ApiKey",
                new { controller = "EndpointFailure", action = "ApiKeyIsRequired" });
            routes.MapRoute("Root", "",
                new { controller = "EndpointFailure", action = "RedirectToState" });

            var endpointTypes = AppDomain.CurrentDomain
                                         .GetAssemblies()
                                         .SelectMany(s => s.GetTypes())
                                         .Where(p => 
                                                 p.IsInterface && 
                                                 p.GetInterfaces()
                                                    .Any(x => x.IsGenericType 
                                                        && x.GetGenericTypeDefinition() == typeof (IEndpoint<,>)));
            var endpointsRegisterer = Container.Resolve<IEndpointsRegisterer>();
            foreach (var endpointType in endpointTypes)
            {
                endpointsRegisterer.RegisterEndpoint(routes, Container.ResolveAll(endpointType));
            }
        }

        protected override void Application_Start()
        {
            Container.Install(ApplicationInstaller);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container));
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(Container));

            _container.Resolve<IFiltersManager>().Register(GlobalFilters.Filters);

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected override void Application_End()
        {
            base.Application_End();
        
            Container.Dispose();
        }
    }
}
