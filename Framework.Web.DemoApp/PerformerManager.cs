using Framework.Web.Application;
using Framework.Web.Models;
using Vlindos.InversionOfControl;

namespace Framework.Web.DemoApp
{
    public class PerformerManager : IPerformerManager
    {
        private readonly IContainer _container;

        public PerformerManager(IContainer container)
        {
            _container = container;
        }

        public TPerformer GetPerformer<TPerformer, TResponse>(IHttpRequest request, IHttpResponse httpResponse) 
            where TPerformer : IRequestPerformer<TResponse>
        {
            return _container.Resolve<TPerformer>(request.FiltersObjects);
        }
    }
}
