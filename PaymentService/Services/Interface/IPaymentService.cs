using PaymentService.Entities;

namespace PaymentService.Services.Interface
{
    public interface IPaymentService
    {
        Task<Payment> CompleteTransaction(Payment payment);
    }
}
