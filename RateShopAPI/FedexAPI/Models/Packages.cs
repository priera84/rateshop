namespace FedexAPI.Models
{
    public class Package
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string? DimensionsUnitOfMeasure { get; set; }

        public double Weight { get; set; }
        public string? WeightUnitOfMeasure { get; set; }
    }
}