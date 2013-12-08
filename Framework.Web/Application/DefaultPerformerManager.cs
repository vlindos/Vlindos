using Framework.Web.Models;
using Framework.Web.Models.FiltersObjects;
using Vlindos.InversionOfControl;

namespace Framework.Web.Application
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

        public TPerformer GetPerformer<TPerformer, TRequest, TResponse>(
            IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse) 
            where TPerformer : IRequestPerformer<TResponse>
        {
            return _containerAccessor.Container.Resolve<TPerformer>(
                request.FiltersObjects[_requestFiltersObjectsGroup]);
        }
    }
}