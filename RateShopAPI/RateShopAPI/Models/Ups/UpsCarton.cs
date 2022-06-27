namespace RateShopAPI.Models.Ups
{
    public class UpsCarton
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string? DimensionsUnitOfMeasure { get; set; }

        public double Weight { get; set; }
        public string? WeightUnitOfMeasure { get; set; }

        public Package AsPackage()
        {
            return new()
            {
                Length = Length,
                Width = Width,
                Height = Height,
                DimensionsUnitOfMeasure = DimensionsUnitOfMeasure,
                Weight = Weight,
                WeightUnitOfMeasure = WeightUnitOfMeasure
            };
        }
    }
}