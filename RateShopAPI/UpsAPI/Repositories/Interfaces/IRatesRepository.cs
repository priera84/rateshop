using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpsAPI.Models;
using UpsAPI.Models.Db;

namespace UpsAPI.Repositories.Interfaces
{
    public interface IRatesRepository
    {
        Task<List<Rate>> GetRatesAsync(string carrierCode, Shipment shipment);
    }
}
