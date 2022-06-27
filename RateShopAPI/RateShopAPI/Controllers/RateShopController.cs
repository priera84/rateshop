using Microsoft.AspNetCore.Mvc;
using RateShopAPI.BusinessLogic.Interfaces;
using RateShopAPI.Models;


namespace RateShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RateShopController : Controller
    {
        private readonly IRateShop rateShop;

        public RateShopController(IRateShop rateShop)
        {
            this.rateShop = rateShop;
        }

        /// <summary>
        /// Returns the cheapest rate from rate shop.
        /// </summary>
        /// <param name="rateShopRequest">Object of type<paramref name="rateShopRequest"/></param>
        /// <returns>The cheapest rate from rate shop.</returns>
        [HttpPost("cheapestrate")]
        public async Task<IActionResult> GetCheapestRate([FromBody] RateShopRequest rateShopRequest)
        {
            Shipment shipment = rateShopRequest.ToShipment();

            List<Rate> rates = new List<Rate>();

            Rate? cheapestRate = await rateShop.GetCheapestRate(shipment, rates);

            if (cheapestRate != null)
            {
                var response = cheapestRate.ToRateShopResponse();

                return new OkObjectResult(response);
            }
            else
                return NoContent();
        }

          /// <summary>
        /// Returns the cheapest rate from rate shop.
        /// </summary>
        /// <param name="rateShopRequest">Object of type<paramref name="rateShopRequest"/></param>
        /// <returns>The cheapest rate from rate shop.</returns>
        [HttpPost("allrates")]
        public async Task<IActionResult> GetAllRates([FromBody] RateShopRequest rateShopRequest)
        {
            Shipment shipment = rateShopRequest.ToShipment();

            List<Rate> rates = new List<Rate>();

            rates = await rateShop.GetAllRates(shipment, rates);

            if (rates.Any())
            {
                var response = rates.Select(x => x.ToRateShopResponse()).ToList();

                return new OkObjectResult(response);
            }
            else
                return NoContent();
        }

 
    }
}
