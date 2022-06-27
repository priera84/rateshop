namespace RateShopAPI.Models
{
    public class Rate
    {
        public decimal Amount { get; set; }   
        public string? Currency { get; set; }

        public RateShopResponse ToRateShopResponse()
        {
            return new RateShopResponse
            {
                Total = Amount,
                Currency = Currency
            };
        }
    }
}
