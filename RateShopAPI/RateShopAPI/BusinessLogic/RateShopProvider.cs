using RateShopAPI.BusinessLogic.Interfaces;
using RateShopAPI.BusinessLogic.Providers;
using RateShopAPI.Models;

namespace RateShopAPI.BusinessLogic
{
    public class RateShopProvider : IRateShopProvider
    {
        private List<ICarrierApiProvider> _carrierApiProviders = new();


        public RateShopProvider()
        {
            RegisterCarrierApiProvider(new UpsCarrierApiProvider());
            RegisterCarrierApiProvider(new FedexCarrierApiProvider());
        }

        private void RegisterCarrierApiProvider(ICarrierApiProvider carrierApiProvider)
        {
            _carrierApiProviders.Add(carrierApiProvider);
        }

        public async Task<List<Rate>> GetRatesFromCarriers(Shipment shipment)
        {
            var tasks = _carrierApiProviders.Select(x => GetCarrierRates(shipment, x));

            var rates = await Task.WhenAll(tasks);

            List<Rate> result = new();

            foreach (var rate in rates)
                result.AddRange(rate);

            return result;

        }

        private static async Task<List<Rate>> GetCarrierRates(Shipment shipment, ICarrierApiProvider item)
        {
            item.CreateRequest(shipment);

            var httpResponse = await item.SendRequestAsync();

            return await item.MapResults(httpResponse);
        }
    }
}
