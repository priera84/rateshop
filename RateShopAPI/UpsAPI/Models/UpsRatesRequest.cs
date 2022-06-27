namespace UpsAPI.Models
{
    public class UpsRatesRequest
    {
        public UpsAddress ContactAddress { get; set; }
        public UpsAddress WarehouseAddress { get; set; }

        public UpsCarton CartonDimensions { get; set; }

        internal Shipment AsShipment()
        {
            return new Shipment
            {
                PickupAddress = ContactAddress.AsAddress(),
                DeliveryAddress = WarehouseAddress.AsAddress(),
                Packages = new() { CartonDimensions.AsPackage() }
            };
        }

    }
}