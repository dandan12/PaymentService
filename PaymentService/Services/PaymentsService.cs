using PaymentService.Entities;
using PaymentService.Repositories.Interface;
using PaymentService.Services.Interface;
using PaymentService.Utils.Contants;

namespace PaymentService.Services
{
    public class PaymentsService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly INotificationRepository _notificationRepository;

        public PaymentsService(IPaymentRepository paymentRepository, INotificationRepository notificationRepository)
        {
            _paymentRepository = paymentRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<Payment> CompleteTransaction(Payment payment)
        {
            payment.Status = PaymentStatusConstant.Success;
            var response = await _paymentRepository.Update(payment.Id, payment);

            var notification = new Notification()
            {
                Payload = response.Resource
            };

            await _notificationRepository.Create(notification);
            return response.Resource;
        }
    }
}
