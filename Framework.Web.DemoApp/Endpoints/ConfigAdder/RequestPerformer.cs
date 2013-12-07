using Database;
using Framework.Web.Application;
using Framework.Web.DemoApp.Models;
using Framework.Web.Service.Models;

namespace Framework.Web.DemoApp.Endpoints.ConfigAdder
{
    public interface IRequestPerformer : IRequestPerformer<IServiceResponse>
    {
    }

    public class RequestPerformer : IRequestPerformer
    {
        private readonly IDatabase<Config> _database;
        private readonly int _requestNumber;

        public RequestPerformer(IDatabase<Config> database, int requestNumber)
        {
            _database = database;
            _requestNumber = requestNumber;
        }

        public IServiceResponse Perform()
        {
            var result = _database.Add(new Config
            {
                Number = _requestNumber
            }).Perform();

            return new ServiceResponse
            {
                Success = result.Success,
                Messages = result.Messages,
            };
        }
    }
}
