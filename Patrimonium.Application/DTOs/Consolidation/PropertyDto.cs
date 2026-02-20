namespace Patrimonium.Application.DTOs.Consolidation
{
    public class PropertyDto
    {
        public Guid Id { get; set; }
        public string InternalName { get; set; } = "";
        public string Type { get; set; } = "";
        public string Purpose { get; set; } = "";
        public decimal EstimatedValue { get; set; }
        public decimal InvestedValue { get; set; }
    }
}
