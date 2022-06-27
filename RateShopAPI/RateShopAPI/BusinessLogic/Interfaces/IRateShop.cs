using RateShopAPI.Models;

namespace RateShopAPI.BusinessLogic.Interfaces
{
    public interface IRateShop
    {
        Task<Rate?> GetCheapestRate(Shipment shipment, List<Rate> rates);        
    }
}