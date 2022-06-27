namespace RateShopAPI.Models.Fedex
{
    public class FedexPackage
    {
        public double length { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public string? dimensionsUnitOfMeasure { get; set; }

        public double weight { get; set; }
        public string? weightUnitOfMeasure { get; set; }

        public Package AsPackage()
        {
            return new()
            {
                Length = length,
                Width = width,
                Height = height,
                DimensionsUnitOfMeasure = dimensionsUnitOfMeasure,
                Weight = weight,
                WeightUnitOfMeasure = weightUnitOfMeasure
            };
        }
    }
}