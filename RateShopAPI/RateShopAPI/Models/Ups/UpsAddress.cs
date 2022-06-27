namespace RateShopAPI.Models.Ups
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
                Street = Street,
                City = City,
                PostalCode = PostalCode,
                StateCode = StateCode,
                CountryCode = CountryCode
            };
        }
    }
}