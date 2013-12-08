
namespace Framework.Web.Models.FiltersObjects
{
    public interface IRequestFiltersObjectsBagGroup : IFiltersObjectsBagGroup
    {

    }

    public class RequestFiltersObjectsBagGroup : IRequestFiltersObjectsBagGroup
    {
        public string Id { get { return "Request"; } }
    }
}
