using Newtonsoft.Json;
using PaymentService.Attributes;
using PaymentService.Utils.Contants;

namespace PaymentService.Entities
{
    [CosmosContainer(ContainerConstant.Notifications)]
    public class Notification : BaseEntity
    {
        [JsonProperty("payload")]
        public object Payload { get; set; }
    }
}
