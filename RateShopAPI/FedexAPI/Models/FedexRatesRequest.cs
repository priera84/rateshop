namespace FedexAPI.Models
{
    public class FedexRatesRequest
    {
        public FedexAddress sourceAddress { get; set; }
        public FedexAddress destinationAddress { get; set; }

        public List<FedexPackage> packages { get; set; }

        internal Shipment AsShipment()
        {
            return new Shipment
            {
                PickupAddress = sourceAddress.AsAddress(),
                DeliveryAddress = destinationAddress.AsAddress(),
                Packages = packages.Select(x => x.AsPackage()).ToList()
            };
        }       
    }
}