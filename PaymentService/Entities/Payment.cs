using Newtonsoft.Json;
using PaymentService.Attributes;
using PaymentService.Utils.Contants;

namespace PaymentService.Entities
{
    [CosmosContainer(ContainerConstant.Payments)]
    public class Payment : BaseEntity
    {
        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("partner_id")]
        public string PartnerId { get; set; }

        internal void Should()
        {
            throw new NotImplementedException();
        }
    }
}
