using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpsAPI.Models.Db
{
    public class Rate
    {
        public int RateID { get; set; }
        public string CarrierCode { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public string OriginCountryCode { get; set; }
        public string DestinationCountryCode { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
