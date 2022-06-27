using RateShopAPI.Utils;
using System.Xml.Serialization;

namespace RateShopAPI.Models.Fedex
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

        public string GenerateBodyContent()
        {
            using (var sw = new Utf8StringWriter())
            {
                var serializer = new XmlSerializer(GetType());
                serializer.Serialize(sw, this);
                return sw.ToString();
            }
        }
    }
}