using Microsoft.Azure.Cosmos;
using PaymentService.Utils.Contants;

namespace PaymentService.Models
{
    public class NotificationDbSetting : CosmosDbSetting
    {
        public override async Task ConfigureCosmosDb()
        {
            var client = new CosmosClient(ConnectionString, new CosmosClientOptions() { ConnectionMode = ConnectionMode.Gateway });
            var database = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);

            await database.Database.CreateContainerIfNotExistsAsync(ContainerConstant.Notifications, "/id");
        }
    }
}
