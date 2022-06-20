using Microsoft.Azure.Cosmos;
using PaymentService.Utils.Contants;

namespace PaymentService.Models
{
    public abstract class CosmosDbSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public abstract Task ConfigureCosmosDb();

    }
}
