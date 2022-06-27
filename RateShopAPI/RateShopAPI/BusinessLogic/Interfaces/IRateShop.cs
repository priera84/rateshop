using RateShopAPI.Models;

namespace RateShopAPI.BusinessLogic.Interfaces
{
    public interface IRateShop
    {
        Task<Rate?> GetCheapestRate(Shipment shipment, List<Rate> rates);
        Task<List<Rate>> GetAllRates(Shipment shipment, List<Rate> rates);
    }
}