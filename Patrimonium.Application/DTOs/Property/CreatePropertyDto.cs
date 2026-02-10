using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Property
{
    public class CreatePropertyDto
    {
        public string InternalName { get; set; } = default!;
        public PropertyType Type { get; set; }
        public PropertyPurpose Purpose { get; set; }
        public string Status { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string Neighborhood { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        public double BuiltAreaM2 { get; set; }
        public double TotalAreaM2 { get; set; }
    }
}
