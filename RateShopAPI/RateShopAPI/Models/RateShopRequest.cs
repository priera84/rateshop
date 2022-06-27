namespace RateShopAPI.Models
{
    public class RateShopRequest
    {
        public Address ContactAddress { get; set; }
        public Address WarehouseAddress { get; set; }

        public List<Package>? PackageDimensions { get; set; }

        internal Shipment ToShipment()
        {
            return new Shipment
            {
                PickupAddress = ContactAddress,
                DeliveryAddress = WarehouseAddress,
                Packages = PackageDimensions
            };
        }
    }
}
