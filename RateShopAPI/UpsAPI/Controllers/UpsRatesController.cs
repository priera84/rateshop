using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsAPI.Models;
using UpsAPI.Models.Db;
using UpsAPI.Repositories.Interfaces;

namespace UpsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpsRatesController : ControllerBase
    {
        private readonly ILogger<UpsRatesController> _logger;
        private readonly IRatesRepository _ratesRepository;

        public UpsRatesController(ILogger<UpsRatesController> logger, IRatesRepository ratesRepository)
        {
            _logger = logger;
            _ratesRepository = ratesRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upsRatesRequest"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetUpsRates")]
        [ProducesResponseType(typeof(IEnumerable<UpsRatesResponse>), 200, "application/json")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        public async Task<IActionResult> GetUpsRates([FromBody] UpsRatesRequest upsRatesRequest)
        {
            Thread.Sleep(2000);

            var rates = await _ratesRepository.GetRatesAsync("UPS", upsRatesRequest.AsShipment());

            if (!rates.Any())
                return NoContent();

            return new OkObjectResult(rates.Select(x => new UpsRatesResponse { Total = x.Amount, Currency = x.Currency }).ToList());
        }
    }
}