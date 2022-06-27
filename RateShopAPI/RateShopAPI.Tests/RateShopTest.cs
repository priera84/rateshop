using RateShopAPI.BusinessLogic;
using RateShopAPI.BusinessLogic.Interfaces;
using RateShopAPI.Models;

namespace RateShopAPI.Tests
{
    public class RateShopTest
    {
        [Fact]
        public void TestIfBestRateIsReturned()
        {
            IRateShop rateShop = new RateShop(null);

            Shipment shipment = new()
            {
                PickupAddress = new()
                {
                    Street = "1st Av",
                    City = "New York",
                    PostalCode = "10035",
                    StateCode = "NY",
                    CountryCode = "US"
                },
                DeliveryAddress = new()
                {
                    Street = "1st Av",
                    City = "Los Angeles",
                    PostalCode = "10035",
                    StateCode = "CA",
                    CountryCode = "US"
                },
                Packages = new(){ new(){Height = 10, Length= 10,Width= 10, DimensionsUnitOfMeasure= "CM", Weight= 1, WeightUnitOfMeasure="KG"}}
            };

            List<Rate> rates = new()
            {
                new(){Amount = 50, Currency="USD"},
                new(){Amount = 40, Currency="USD"},
                new(){Amount = 54, Currency="USD"},
                new(){Amount = 70, Currency="USD"},
                new(){Amount = 65.5m, Currency="USD"},
                new(){Amount = 67, Currency="USD"}
            };

            Rate? bestRate = rateShop.GetCheapestRate(shipment, rates).Result;

            Assert.Equal(40, bestRate?.Amount);
            
            Assert.Equal("USD", bestRate?.Currency);
        }

         public void TestIfAllAreReturned()
        {
            IRateShop rateShop = new RateShop(null);

            Shipment shipment = new()
            {
                PickupAddress = new()
                {
                    Street = "1st Av",
                    City = "New York",
                    PostalCode = "10035",
                    StateCode = "NY",
                    CountryCode = "US"
                },
                DeliveryAddress = new()
                {
                    Street = "1st Av",
                    City = "Los Angeles",
                    PostalCode = "10035",
                    StateCode = "CA",
                    CountryCode = "US"
                },
                Packages = new(){ new(){Height = 10, Length= 10,Width= 10, DimensionsUnitOfMeasure= "CM", Weight= 1, WeightUnitOfMeasure="KG"}}
            };

            List<Rate> rates = new()
            {
                new(){Amount = 50, Currency="USD"},
                new(){Amount = 40, Currency="USD"},
                new(){Amount = 54, Currency="USD"},
                new(){Amount = 70, Currency="USD"},
                new(){Amount = 65.5m, Currency="USD"},
                new(){Amount = 67, Currency="USD"}
            };

            var result = rateShop.GetAllRates(shipment, rates).Result;

            Assert.Equal(6, result.Count);            
        }
    }
}