using Newtonsoft.Json;
using RateShopAPI.BusinessLogic.Interfaces;
using RateShopAPI.Models;
using RateShopAPI.Models.Ups;
using System.Net;
using System.Net.Http.Headers;

namespace RateShopAPI.BusinessLogic.Providers
{
    public class UpsCarrierApiProvider : ICarrierApiProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private UpsRatesRequest? _upsRatesRequest;

        public UpsCarrierApiProvider(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public void CreateRequest(Shipment shipment)
        {
            _upsRatesRequest = shipment.AsUpsRatesRequest();
        }

        public async Task<List<Rate>> MapResults(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var upsApiResponse =
                        JsonConvert.DeserializeObject<List<UpsRatesResponse>>(await httpResponseMessage.Content.ReadAsStringAsync());

                    List<Rate> result = new List<Rate>(upsApiResponse.Count);

                    foreach (var rate in upsApiResponse)
                    {
                        result.Add(new() { Amount = rate.Total, 
                                           Currency = rate.Currency });
                    }

                    return result;
                }
                
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new Exception("Service Unavailable.");
            }
            
            return null;            
        }

        public async Task<HttpResponseMessage> SendRequestAsync()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("UpsAPIClient");

            httpClient.BaseAddress = new Uri(_configuration["UpsAPIUrl"]);
         
            string requestBody = _upsRatesRequest.GenerateBodyContent();

            StringContent content = new StringContent(requestBody);

            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            try
            {
                //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                var response = await httpClient.PostAsync(_configuration["UpsAPIEndpoint"], content);

                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calling UPS API: {e.InnerException?.InnerException?.Message ?? e.Message}", e);
            }
        }
    }
}
