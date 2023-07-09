using Models.Payment;

namespace Core.Services.Contracts
{
    public interface IPaymentService
    {
        public Task<StripeIntentResponse> CreateIntentAsync(long priceTotal);
        public Task<StripeIntentResponse> UpdateIntentAsync(string IntentId, long priceTotal);
    }
}
