using Models.Payment;

namespace Core.Services.Contracts
{
    public interface IPaymentService
    {
        public Task<StripeIntentResponse> CreateIntentAsync(long priceTotal);
        public Task UpdateIntentAsync(string IntentId, long priceTotal);
    }
}
