using Models.Account;
using System.ComponentModel.DataAnnotations;

namespace Models.Order
{
    public class OrderRequestDto
    {
        [Required]
        public string basketId { get; set; }  
        public int DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
