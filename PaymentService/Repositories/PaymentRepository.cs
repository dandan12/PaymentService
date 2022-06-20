using PaymentService.Contexts;
using PaymentService.Entities;
using PaymentService.Repositories.Interface;
using PaymentService.Utils.Contants;

namespace PaymentService.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(TransactionDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public Payment GetPaymentByReferenceNumber(string referenceNumber)
        {
            var query = Linq();
            var item = query
               .Where(x => x.ReferenceNumber == referenceNumber &&
               x.PartnerId == PartnerId)
               .AsEnumerable()
               .FirstOrDefault();

            return item;
        }

        public Payment GetPaymentByReferenceNumAndAmount(string referenceNumber, decimal amount)
        {
            var query = Linq();
            var item = query
                .Where(x => x.ReferenceNumber == referenceNumber && 
                x.Amount == amount &&
                x.Status == PaymentStatusConstant.Pending &&
                x.PartnerId == PartnerId)
                .AsEnumerable()
                .FirstOrDefault();

            return item;
        }
    }
}
