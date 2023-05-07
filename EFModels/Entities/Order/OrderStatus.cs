using System.Runtime.Serialization;

namespace EFModels.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Panding")]
        Panding,

        [EnumMember(Value = "Payment Success")]
        PanymentSuccess,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed,
    }
}
