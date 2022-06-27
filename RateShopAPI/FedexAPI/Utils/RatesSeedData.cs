using FedexAPI.Models.Db;

namespace FedexAPI.Utils
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
                Description = "FedEx Ground",
                CarrierCode = "FDX",
                Code ="FDXG",
                Amount = 1,
                Currency = "USD"
            },
            new()
            {
                OriginCountryCode = "US",
                DestinationCountryCode = "CA",
                Description = "FedEx Ground",
                CarrierCode = "FDX",
                Code ="FDXG",
                Amount = 2,
                Currency = "USD"
            },
            new()
            {
                OriginCountryCode = "CA",
                DestinationCountryCode = "US",
                Description = "FedEx Ground",
                CarrierCode = "FDX",
                Code ="FDXG",
                Amount = 2,
                Currency = "USD"
            },
            new()
            {
                OriginCountryCode = "CA",
                DestinationCountryCode = "ES",
                Description = "FDX AIR",
                CarrierCode = "FDX",
                Code ="FDXA",
                Amount = 5,
                Currency = "USD"
            },
            new()
            {
                OriginCountryCode = "US",
                DestinationCountryCode = "ES",
                Description = "FDX AIR",
                CarrierCode = "FDX",
                Code ="FDXA",
                Amount = 5,
                Currency = "USD"
            }};

            await context.AddRangeAsync(rates);
            await context.SaveChangesAsync();

        }
    }
}
