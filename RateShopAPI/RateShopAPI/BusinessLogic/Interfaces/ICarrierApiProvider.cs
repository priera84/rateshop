using RateShopAPI.Models;

namespace RateShopAPI.BusinessLogic.Interfaces
{
    public interface ICarrierApiProvider
    {
        void CreateRequest(Shipment shipment);

        Task<HttpResponseMessage> SendRequestAsync();

        Task<List<Rate>> MapResults(HttpResponseMessage httpResponseMessage);
    }
}