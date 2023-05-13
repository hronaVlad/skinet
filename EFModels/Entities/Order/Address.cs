namespace EFModels.Entities.Order
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string firstName, string lastName, string city, string state, string street, string postalCode)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            State = state;
            Street = street;
            PostalCode = postalCode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
