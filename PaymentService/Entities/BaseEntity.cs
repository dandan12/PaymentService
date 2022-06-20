using Newtonsoft.Json;

namespace PaymentService.Entities
{
    public class BaseEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("modified_date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
