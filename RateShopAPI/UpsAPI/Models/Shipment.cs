namespace UpsAPI.Models
{
    public class Shipment
    {
        public Address PickupAddress { get; set; }
        public Address DeliveryAddress { get; set; }

        public List<Package>? Packages { get; set; }
    }
}
