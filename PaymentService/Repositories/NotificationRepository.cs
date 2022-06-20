using Microsoft.Azure.Cosmos;
using PaymentService.Contexts;
using PaymentService.Entities;
using PaymentService.Repositories.Interface;

namespace PaymentService.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(NotificationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
