namespace PaymentService.Attributes
{
    public class CosmosContainerAttribute : Attribute
    {
        public string Name { get; set; }
        public CosmosContainerAttribute(string name)
        {
            Name = name;
        }
    }
}
