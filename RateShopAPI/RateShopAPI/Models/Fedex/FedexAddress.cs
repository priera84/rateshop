namespace RateShopAPI.Models.Fedex
{
    public class FedexAddress
    {
        public string street { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string stateCode { get; set; }
        public string countryCode { get; set; }

        public Address AsAddress()
        {
            return new()
            {
                Street = street,
                City = city,
                PostalCode = postalCode,
                StateCode = stateCode,
                CountryCode = countryCode
            };
        }
    }
}