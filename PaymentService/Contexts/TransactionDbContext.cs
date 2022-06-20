using Microsoft.Azure.Cosmos;
using PaymentService.Attributes;
using PaymentService.Models;
using PaymentService.Utils.Contants;

namespace PaymentService.Contexts
{
    public class TransactionDbContext : CosmosDbContext
    {
        public TransactionDbContext(CosmosDbSetting setting) : base(setting)
        {
        }
    }
}
