using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Property
{
    public record CreatePropertyDto(
    string InternalName,
    PropertyType Type,
    PropertyPurpose Purpose,
    PropertyStatus Status,
    string Street,
    string Number,
    string? Complement,
    string Neighborhood,
    string City,
    string State,
    string Country,
    string ZipCode,
    double BuiltAreaM2,
    double TotalAreaM2,
    int Bedrooms,
    int Bathrooms,
    int Suites,
    int ParkingSpaces,
    bool Furnished,
    string? Description
);
}
