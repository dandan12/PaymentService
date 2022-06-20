using PaymentService.Entities;

namespace PaymentService.Repositories.Interface
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Payment GetPaymentByReferenceNumAndAmount(string referenceNumber, decimal amount);
        Payment GetPaymentByReferenceNumber(string referenceNumber);
    }
}
