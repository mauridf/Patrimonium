using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Property : AuditableEntity
    {
        public string InternalName { get; set; } = default!;
        public PropertyType Type { get; set; }
        public PropertyPurpose Purpose { get; set; }
        public string Status { get; set; } = default!;

        // Endereço
        public string Street { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string? Complement { get; set; }
        public string Neighborhood { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = "Brasil";
        public string ZipCode { get; set; } = default!;

        // Dados físicos
        public double BuiltAreaM2 { get; set; }
        public double TotalAreaM2 { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Suites { get; set; }
        public int ParkingSpaces { get; set; }
        public int YearBuilt { get; set; }
        public bool Furnished { get; set; }

        public string? Description { get; set; }

        // Navegação
        public User User { get; set; } = default!;
    }
}
