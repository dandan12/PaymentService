using Microsoft.Azure.Cosmos;
using PaymentService.Contexts;
using PaymentService.Entities;
using PaymentService.Utils.Contants;

namespace PaymentService.Utils.Seed
{
    public class PaymentSeed
    {
        public static async Task Seed(TransactionDbContext context)
        {
            var payment1 = new Payment()
            {
                Id = "12720b36-6ed7-4ad9-8f25-c3d666f629f6",
                Amount = 500,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ReferenceNumber = "A1",
                Status = PaymentStatusConstant.Pending
            };

            await context.Get<Payment>().CreateItemAsync(payment1, new PartitionKey(payment1.Id));

            var payment2 = new Payment()
            {
                Id = "89d4afa3-ed6a-424c-8f3e-0950f0bcda61",
                Amount = 600,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ReferenceNumber = "B2",
                Status = PaymentStatusConstant.Pending
            };

            await context.Get<Payment>().CreateItemAsync(payment2, new PartitionKey(payment2.Id));

            var payment3 = new Payment()
            {
                Id = "22edbc14-0ff3-4060-975c-f90e1e317c23",
                Amount = 700,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ReferenceNumber = "C3",
                Status = PaymentStatusConstant.Success
            };

            await context.Get<Payment>().CreateItemAsync(payment3, new PartitionKey(payment3.Id));
        }
    }
}
