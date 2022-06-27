﻿using Newtonsoft.Json;
using RateShopAPI.BusinessLogic.Interfaces;
using RateShopAPI.Models;
using RateShopAPI.Models.Ups;
using System.Net;
using System.Net.Http.Headers;

namespace RateShopAPI.BusinessLogic.Providers
{
    public class UpsCarrierApiProvider : ICarrierApiProvider
    {
        private UpsRatesRequest? _upsRatesRequest;
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
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("https://localhost:7121");
         
            string requestBody = _upsRatesRequest.GenerateBodyContent();

            StringContent content = new StringContent(requestBody);

            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            try
            {
                //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                var response = await httpClient.PostAsync("api/UpsRates", content);

                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calling UPS API: {e.InnerException?.InnerException?.Message ?? e.Message}", e);
            }
        }
    }
}