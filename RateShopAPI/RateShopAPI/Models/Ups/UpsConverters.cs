namespace RateShopAPI.Models.Ups
{
    public static class UpsConverters
    {
        public static UpsCarton AsUpsCarton(this List<Package> packages)
        {
            if (packages == null)
                return null;

            if (packages.Count != 1)
            {
                throw new Exception("Ups API only accepts shipments with one package");
            }

            return new UpsCarton
            {
                DimensionsUnitOfMeasure = packages[0].DimensionsUnitOfMeasure,
                Height = packages[0].Height,
                Length = packages[0].Length,
                Weight = packages[0].Weight,
                WeightUnitOfMeasure = packages[0].WeightUnitOfMeasure,
                Width = packages[0].Width
            };
        }
        public static UpsAddress AsUpsAddress(this Address address) => new UpsAddress
        {
            City = address.City,
            CountryCode = address.CountryCode,
            PostalCode = address.PostalCode,
            StateCode = address.StateCode,
            Street = address.Street
        };

        public static UpsRatesRequest AsUpsRatesRequest(this Shipment shipment) => new UpsRatesRequest
        {
            ContactAddress = shipment.PickupAddress.AsUpsAddress(),
            WarehouseAddress = shipment.DeliveryAddress.AsUpsAddress(),
            CartonDimensions = shipment.Packages?.AsUpsCarton()
        };
    }
}
