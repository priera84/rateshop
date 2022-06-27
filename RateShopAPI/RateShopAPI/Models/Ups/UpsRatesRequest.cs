using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RateShopAPI.Models.Ups
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

        internal string GenerateBodyContent()
        {
            JsonSerializerSettings formatSettings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(this, formatSettings);
        }
    }
}