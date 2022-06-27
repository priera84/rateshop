using FedexAPI.Models;
using FedexAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FedexAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class FedexRatesController : ControllerBase
    {
        private readonly ILogger<FedexRatesController> _logger;
        private readonly IRatesRepository _ratesRepository;

        public FedexRatesController(ILogger<FedexRatesController> logger, IRatesRepository ratesRepository)
        {
            _logger = logger;
            _ratesRepository = ratesRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fedexRatesRequest"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetFedexRates")]
        [Produces("text/xml")]
        [ProducesResponseType(typeof(IEnumerable<FedexRatesResponse>), 200, "text/xml")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        public async Task<IActionResult> GetUpsRates([FromBody] FedexRatesRequest fedexRatesRequest)
        {
            var rates = await _ratesRepository.GetRatesAsync("FDX", fedexRatesRequest.AsShipment());

            if (!rates.Any())
                return NoContent();

            return new OkObjectResult(rates.Select(x => new FedexRatesResponse { Quote = x.Amount, Currency = x.Currency }).ToList());
        }
    }
}