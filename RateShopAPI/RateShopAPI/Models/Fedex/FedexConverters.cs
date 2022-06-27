namespace RateShopAPI.Models.Fedex
{
    public static class FedexConverters
    {
        public static FedexPackage AsFedexPackage(this Package package) => new FedexPackage
        {
            dimensionsUnitOfMeasure = package.DimensionsUnitOfMeasure,
            height = package.Height,
            length = package.Length,
            weight = package.Weight,
            weightUnitOfMeasure = package.WeightUnitOfMeasure,
            width = package.Width
        };
        public static FedexAddress AsFedexAddress(this Address address) => new FedexAddress
        {
            city = address.City,
            countryCode = address.CountryCode,
            postalCode = address.PostalCode,
            stateCode = address.StateCode,
            street = address.Street
        };
        public static FedexRatesRequest AsFedexRatesRequest(this Shipment shipment)
        {
            return new FedexRatesRequest
            {
                sourceAddress = shipment.PickupAddress.AsFedexAddress(),
                destinationAddress = shipment.DeliveryAddress.AsFedexAddress(),
                packages = shipment.Packages?.Select(x => x.AsFedexPackage()).ToList()
            };
        }
    }
}
