using RateShopAPI.BusinessLogic.Interfaces;
using RateShopAPI.Models;

namespace RateShopAPI.BusinessLogic
{
    public class RateShop : IRateShop
    {
        private readonly IRateShopProvider _rateShopProvider;

        public RateShop(IRateShopProvider rateShopProvider)
        {
            _rateShopProvider = rateShopProvider;
        }
       
        /// <summary>
        /// Returns the cheapest rates from the list of rates. 
        /// </summary>
        /// <param name="shipment">Object with requested shipment.</param>
        /// <param name="rates">List of rates to filter.</param>
        /// <returns>The cheapest rate from the list.</returns>
        public async Task<Rate?> GetCheapestRate(Shipment shipment, List<Rate> rates)
        {
            if (!rates.Any())
                rates = await _rateShopProvider.GetRatesFromCarriers(shipment); 

            if (rates.Any())
            {
                return rates.OrderBy(x => x.Amount).First();
            }

            return null;
        }
    }
}
