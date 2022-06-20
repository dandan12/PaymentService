namespace PaymentService.Models
{
    public class AuthenticationSetting
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
