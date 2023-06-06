namespace Models.Payment
{
    public class StripeIntentResponse
    {
        public string IntentId { get; set; }
        public string ClientSecret { get; set; }
        public string Status { get; set; }
    }
}
