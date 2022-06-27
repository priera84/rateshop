using RateShopAPI.Models;

namespace RateShopAPI.BusinessLogic.Interfaces
{
    public interface IRateShopProvider
    {
        Task<List<Rate>> GetRatesFromCarriers(Shipment shipment);
    }
}