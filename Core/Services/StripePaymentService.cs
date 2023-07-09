using Core.Services.Contracts;
using Models.Payment;
using Stripe;

namespace Core.Services
{
    public class StripePaymentService : IPaymentService
    {
        public StripePaymentService() { }

        public async Task<StripeIntentResponse> CreateIntentAsync(long priceTotal)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = priceTotal * 100, // The amount in cents
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            if (paymentIntent == null)
            {
                throw new Exception("Failed to get a payment intent");
            }

            return new StripeIntentResponse
            {
                IntentId = paymentIntent.Id,
                ClientSecret = paymentIntent.ClientSecret,
                Status = paymentIntent.Status
            };
        }

        public async Task<StripeIntentResponse> UpdateIntentAsync(string intentId, long priceTotal)
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = priceTotal * 100, // The amount in cents
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.UpdateAsync(intentId, options);

            if (paymentIntent == null)
            {
                throw new Exception("Failed to get a payment intent");
            }

            return new StripeIntentResponse
            {
                IntentId = paymentIntent.Id,
                ClientSecret = paymentIntent.ClientSecret,
                Status = paymentIntent.Status
            };
        }

    }
}
