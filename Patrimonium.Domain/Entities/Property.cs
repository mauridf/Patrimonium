using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Property : BaseEntity
    {
        public Guid UserId { get; private set; }

        public string InternalName { get; private set; } = null!;
        public PropertyType Type { get; private set; }
        public PropertyPurpose Purpose { get; private set; }
        public PropertyStatus Status { get; private set; }

        public string Street { get; private set; } = null!;
        public string Number { get; private set; } = null!;
        public string? Complement { get; private set; }
        public string Neighborhood { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string State { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public string ZipCode { get; private set; } = null!;

        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }

        public double BuiltAreaM2 { get; private set; }
        public double TotalAreaM2 { get; private set; }

        public int Bedrooms { get; private set; }
        public int Bathrooms { get; private set; }
        public int Suites { get; private set; }
        public int ParkingSpaces { get; private set; }

        public int? YearBuilt { get; private set; }
        public bool Furnished { get; private set; }

        public string? Description { get; private set; }

        protected Property() { }

        public Property(
            Guid userId,
            string internalName,
            PropertyType type,
            PropertyPurpose purpose,
            PropertyStatus status,
            string street,
            string number,
            string? complement,
            string neighborhood,
            string city,
            string state,
            string country,
            string zipCode,
            double builtAreaM2,
            double totalAreaM2,
            int bedrooms,
            int bathrooms,
            int suites,
            int parkingSpaces,
            bool furnished,
            string? description
        )
        {
            UserId = userId;
            InternalName = internalName;
            Type = type;
            Purpose = purpose;
            Status = status;

            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            BuiltAreaM2 = builtAreaM2;
            TotalAreaM2 = totalAreaM2;

            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            Suites = suites;
            ParkingSpaces = parkingSpaces;

            Furnished = furnished;
            Description = description;
        }

        public void UpdateStatus(PropertyStatus status)
        {
            Status = status;
            MarkUpdated();
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
            MarkUpdated();
        }
    }
}
