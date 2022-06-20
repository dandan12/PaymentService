using Microsoft.Azure.Cosmos;
using PaymentService.Attributes;
using PaymentService.Models;

namespace PaymentService.Contexts
{
    public abstract class CosmosDbContext
    {
        private readonly CosmosDbSetting _setting;
        private CosmosClient _client;

        public CosmosDbContext(CosmosDbSetting setting)
        {
            _client = new CosmosClient(setting.ConnectionString);
            _setting = setting;
        }

        public Container Get<T>()
        {
            var type = typeof(T);
            var attrs = type.GetCustomAttributes(false);
            var containerAttribute = attrs.Where(x => x is CosmosContainerAttribute).FirstOrDefault();
            if (containerAttribute == null)
                throw new Exception("No CosmosContainerAttribute attribute");

            var _containerAttribute = containerAttribute as CosmosContainerAttribute;
            return _client.GetContainer(_setting.DatabaseName, _containerAttribute?.Name);
        }
    }
}
