using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Logging;
using Users.Common.Models.Endpoint;
using Users.Common.Tools;

namespace Users.Common.Mvc
{
    // TODO: This component uses reflection (which should not be used) and sux because of typesafety it misses:
    // needs to be:
    //  - type-safed 
    public interface IEndpointsRegisterer
    {
        void RegisterEndpoint(RouteCollection routes, Array endpoints);
    }

    public class EndpointsRegisterer : IEndpointsRegisterer
    {
        private readonly IExtendedLogger _logger;

        public EndpointsRegisterer(IExtendedLogger logger)
        {
            _logger = logger;
        }

        public void RegisterEndpoint(RouteCollection routes, Array endpoints)
        {
            if (endpoints.Length == 0) return;
            var endpointInterfaceType = endpoints.GetValue(0).GetType();
            var genericDefinition =
                endpointInterfaceType.GetInterfaces()
                            .FirstOrDefault(
                                x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEndpoint<,>));
            if (genericDefinition == null) return;
            var genericTypes = genericDefinition.GetGenericArguments();
            var requestType = genericTypes[0];
            var responseType = genericTypes[1];
            foreach (var endpoint in endpoints)
            {
                var endpointType = endpoint.GetType();
                var endpointDescriptorMethod = endpointType.GetProperty("Descriptor").GetGetMethod();
                var endpointDescriptor = endpointDescriptorMethod.Invoke(endpoint, new object[] { });
                var endpointDescriptorType = typeof(IEndpointDescriptor<>).MakeGenericType(requestType);
                var endpointDescriptorRouteDescriptionMethod = endpointDescriptorType.GetProperty("RouteDescription").GetGetMethod();
                var description = (IRouteDescription)endpointDescriptorRouteDescriptionMethod.Invoke(endpointDescriptor, new object[] { });
                var defaultConfiguration = new
                {
                    controller = "Endpoint",
                    action = "Perform",
                    endpoint,
                    requestType,
                    responseType
                };
                var configuration = TypeMerger.MergeTypes(defaultConfiguration, description.Configuration);
                routes.MapRoute(description.Id, description.Path, configuration);

                _logger.InfoFormat("Registered route '{0}'.", description.Path);
            }
        }

    }
}
