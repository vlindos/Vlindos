using Database;
using Database.Entity;
using Framework.Web.Application;
using Framework.Web.Application.ServiceEndpoint.Models;

namespace Framework.Web.DemoApp.ExamplePerformers
{
    public class Config : IEntity
    {
        public int Number { get; set; }
    }

    public class ConfigAdderPerformer : IRequestPerformer<IServiceResponse>
    {
        private readonly IDatabase<Config> _database;
        private readonly int _requestNumber;

        public ConfigAdderPerformer(IDatabase<Config> database, int requestNumber)
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
