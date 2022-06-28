using RateShopAPI.BusinessLogic.Interfaces;
using RateShopAPI.Models;
using RateShopAPI.Models.Fedex;
using System.Net;
using System.Net.Http.Headers;
using System.Xml.Serialization;

namespace RateShopAPI.BusinessLogic.Providers
{
    public class FedexCarrierApiProvider : ICarrierApiProvider
    {
        private FedexRatesRequest? _fedexRateRequest;
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FedexCarrierApiProvider(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public void CreateRequest(Shipment shipment)
        {
            _fedexRateRequest = shipment.AsFedexRatesRequest();
        }

        public async Task<List<Rate>> MapResults(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    List<FedexRatesResponse>? fedexApiResponse = await DeserializeResponse(httpResponseMessage);

                    if (fedexApiResponse != null)
                    {
                        List<Rate> result = new List<Rate>(fedexApiResponse.Count);

                        foreach (var rate in fedexApiResponse)
                        {
                            result.Add(new()
                            {
                                Amount = rate.Quote,
                                Currency = rate.Currency
                            });
                        }
                        return result;
                    }

                    return null;
                    
                }

            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new Exception("Service Unavailable.");
            }

            return null;
        }

        private async Task<List<FedexRatesResponse>> DeserializeResponse(HttpResponseMessage httpResponseMessage)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<FedexRatesResponse>));

                string xml = await httpResponseMessage.Content.ReadAsStringAsync();

                var fedexApiResponse = xmlSerializer.Deserialize(new StringReader(xml)) as List<FedexRatesResponse>;

                return fedexApiResponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<HttpResponseMessage> SendRequestAsync()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("FedexAPIClient");

            httpClient.BaseAddress = new Uri(_configuration["FedexAPIUrl"]);      

            var requestBody = _fedexRateRequest.GenerateBodyContent();

            StringContent content = new StringContent(requestBody);

            content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

            try
            {
                var response = await httpClient.PostAsync(_configuration["FedexAPIEndpoint"], content);

                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calling Fedex API: {e.InnerException?.InnerException?.Message ?? e.Message}", e);
            }
        }
    }
}
