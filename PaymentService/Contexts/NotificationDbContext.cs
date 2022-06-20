using PaymentService.Models;

namespace PaymentService.Contexts
{
    public class NotificationDbContext : CosmosDbContext
    {
        public NotificationDbContext(CosmosDbSetting setting) : base(setting)
        {
        }
    }
}
