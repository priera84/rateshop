using UpsAPI.Models.Db;

namespace UpsAPI.Utils
{
    public class RatesSeedData
    {
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            RatesDBContext context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<RatesDBContext>();

            List<Rate> rates = new(){ new()
            {
                OriginCountryCode = "US",
                DestinationCountryCode = "US",
                Description = "UPS Ground",
                CarrierCode = "UPS",
                Code ="UPSG",
                Amount = 14,
                Currency = "USD"
            },
            new()
            {
                OriginCountryCode = "US",
                DestinationCountryCode = "CA",
                Description = "UPS Ground",
                CarrierCode = "UPS",
                Code ="UPSG",
                Amount = 22,
                Currency = "USD"
            },
            new()
            {
                OriginCountryCode = "CA",
                DestinationCountryCode = "US",
                Description = "UPS Ground",
                CarrierCode = "UPS",
                Code ="UPSG",
                Amount = 22,
                Currency = "USD"
            },
            new()
            {
                OriginCountryCode = "CA",
                DestinationCountryCode = "ES",
                Description = "UPS AIR",
                CarrierCode = "UPS",
                Code ="UPSA",
                Amount = 50,
                Currency = "USD"
            },
            new()
            {
                OriginCountryCode = "US",
                DestinationCountryCode = "ES",
                Description = "UPS AIR",
                CarrierCode = "UPS",
                Code ="UPSA",
                Amount = 55,
                Currency = "USD"
            }};

            await context.AddRangeAsync(rates);
            await context.SaveChangesAsync();

        }
    }
}
