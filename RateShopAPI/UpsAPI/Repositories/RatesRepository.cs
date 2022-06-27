using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpsAPI.Models;
using UpsAPI.Models.Db;
using UpsAPI.Repositories.Interfaces;

namespace UpsAPI.Repositories
{
    internal class RatesRepository : IRatesRepository
    {
        private readonly RatesDBContext _ratesDBContext;

        public RatesRepository(RatesDBContext ratesDBContext)
        {
            _ratesDBContext = ratesDBContext;
        }
        public async Task<List<Rate>> GetRatesAsync(string carrierCode, Shipment shipment)
        {
            return await _ratesDBContext.Rates.Where(x=> x.CarrierCode == carrierCode 
                                                && x.OriginCountryCode == shipment.PickupAddress.CountryCode
                                                && x.DestinationCountryCode == shipment.DeliveryAddress.CountryCode).ToListAsync();
            
        }
    }
}
