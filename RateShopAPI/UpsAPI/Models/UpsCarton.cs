namespace UpsAPI.Models
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
                Length = this.Length,
                Width = this.Width,
                Height = this.Height,
                DimensionsUnitOfMeasure = this.DimensionsUnitOfMeasure,
                Weight = this.Weight,
                WeightUnitOfMeasure = this.WeightUnitOfMeasure
            };
        }
    }
}