using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentService.Controllers;
using PaymentService.Entities;
using PaymentService.Repositories.Interface;
using PaymentService.Services.Interface;
using PaymentService.Utils.Contants;

namespace PaymentService.Tests
{
    public class PaymentControllerTests
    {
        private Mock<IPaymentRepository> _paymentRepository;
        private Mock<IPaymentService> _paymentService;
        public PaymentControllerTests()
        {
            _paymentRepository = new Mock<IPaymentRepository>();
            _paymentService = new Mock<IPaymentService>();
        }

        [Fact]
        public void ValidateTransaction_should_return_true()
        {
            var payment = GetPayment();
            _paymentRepository.Setup(x => x.GetPaymentByReferenceNumAndAmount(It.IsAny<string>(), It.IsAny<decimal>())).Returns(payment);

            var controller = new PaymentController(_paymentRepository.Object, _paymentService.Object);
            var response = controller.ValidateTransaction("112233", 500);
            Assert.IsType<OkObjectResult>(response);

            var obj = response as OkObjectResult;
            Assert.IsType<bool>(obj.Value);

            obj.Value.Should().Be(true);
        }

        [Fact]
        public void ValidateTransaction_should_return_false()
        {
            _paymentRepository.Setup(x => x.GetPaymentByReferenceNumAndAmount(It.IsAny<string>(), It.IsAny<decimal>())).Returns((Payment)null);

            var controller = new PaymentController(_paymentRepository.Object, _paymentService.Object);
            var response = controller.ValidateTransaction("112233", 500);
            Assert.IsType<OkObjectResult>(response);

            var obj = response as OkObjectResult;
            Assert.IsType<bool>(obj.Value);

            obj.Value.Should().Be(false);
        }

        [Fact]
        public async Task CompleteTransaction_should_return_notfound()
        {
            _paymentRepository.Setup(x => x.GetPaymentByReferenceNumber(It.IsAny<string>())).Returns((Payment)null);
            
            var controller = new PaymentController(_paymentRepository.Object, _paymentService.Object);
            var response = await controller.CompleteTransaction("112233");
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task CompleteTransaction_should_return_payment_object()
        {
            var payment = GetPayment();
            _paymentRepository.Setup(x => x.GetPaymentByReferenceNumber(It.IsAny<string>())).Returns(payment);

            payment.Status = PaymentStatusConstant.Success;
            _paymentService.Setup(x => x.CompleteTransaction(It.IsAny<Payment>())).ReturnsAsync(payment);

            var controller = new PaymentController(_paymentRepository.Object, _paymentService.Object);
            var response = await controller.CompleteTransaction("112233");
            Assert.IsType<OkObjectResult>(response);

            var obj = response as OkObjectResult;
            Assert.IsType<Payment>(obj.Value);

            var paymentResponse = obj.Value as Payment;
            paymentResponse.Should().NotBeNull();
            paymentResponse.Should().BeEquivalentTo(payment);
        }

        private Payment GetPayment()
        {
            return new Payment()
            {
                Id = "12720b36-6ed7-4ad9-8f25-c3d666f629f6",
                Amount = 500,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ReferenceNumber = "A1",
                Status = PaymentStatusConstant.Pending
            };
        }
    }
}