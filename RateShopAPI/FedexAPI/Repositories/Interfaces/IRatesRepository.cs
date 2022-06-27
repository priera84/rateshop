using FedexAPI.Models;
using FedexAPI.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedexAPI.Repositories.Interfaces
{
    public interface IRatesRepository
    {
        Task<List<Rate>> GetRatesAsync(string carrierCode, Shipment shipment);
    }
}
