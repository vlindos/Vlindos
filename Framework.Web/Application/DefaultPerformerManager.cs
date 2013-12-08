using Framework.Web.Models;
using Framework.Web.Models.FiltersObjects;
using Vlindos.InversionOfControl;

namespace Framework.Web.Application
{
    public class DefaultPerformerManager : IPerformerManager
    {
        private readonly IContainerAccessor _containerAccessor;
        private readonly IRequestFiltersObjectsBagGroup _requestFiltersObjectsBagGroup;

        public DefaultPerformerManager(IContainerAccessor containerAccessor, 
            IRequestFiltersObjectsBagGroup requestFiltersObjectsBagGroup)
        {
            _containerAccessor = containerAccessor;
            _requestFiltersObjectsBagGroup = requestFiltersObjectsBagGroup;
        }

        public TPerformer GetPerformer<TPerformer, TRequest, TResponse>(
            IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse) 
            where TPerformer : IRequestPerformer<TResponse>
        {
            return _containerAccessor.Container.Resolve<TPerformer>(
                request.FiltersObjects[_requestFiltersObjectsBagGroup]);
        }
    }
}