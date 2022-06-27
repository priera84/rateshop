namespace UpsAPI.Models
{
    public class UpsAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }

        public Address AsAddress()
        {
            return new()
            {
                Street = this.Street,
                City = this.City,
                PostalCode = this.PostalCode,
                StateCode = this.StateCode,
                CountryCode = this.CountryCode                
            };
        }
    }
}