using FedexAPI.Models;
using FedexAPI.Models.Db;
using FedexAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedexAPI.Repositories
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
            return await _ratesDBContext.Rates.Where(x => x.CarrierCode == carrierCode
                                                && x.OriginCountryCode == shipment.PickupAddress.CountryCode
                                                && x.DestinationCountryCode == shipment.DeliveryAddress.CountryCode).ToListAsync();

        }
    }
}
