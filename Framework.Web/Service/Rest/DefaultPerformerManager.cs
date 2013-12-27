using Framework.Web.Models;
using Framework.Web.Models.FiltersObjects;
using Vlindos.InversionOfControl;

namespace Framework.Web.Service.Rest
{
    public class DefaultPerformerManager : IPerformerManager
    {
        private readonly IContainerAccessor _containerAccessor;
        private readonly IRequestFiltersObjectsGroup _requestFiltersObjectsGroup;

        public DefaultPerformerManager(IContainerAccessor containerAccessor, 
            IRequestFiltersObjectsGroup requestFiltersObjectsGroup)
        {
            _containerAccessor = containerAccessor;
            _requestFiltersObjectsGroup = requestFiltersObjectsGroup;
        }

        public T GetPerformer<T, TResponse>(HttpContext httpContext) 
            where T : IRequestPerformer<TResponse>
        {
            return _containerAccessor.Container.Resolve<T>(
                request.FiltersObjects[_requestFiltersObjectsGroup]);
        }
    }
}