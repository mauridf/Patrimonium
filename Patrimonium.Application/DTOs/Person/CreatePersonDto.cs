using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Person
{
    public class CreatePersonDto
    {
        public string Name { get; set; } = default!;
        public PersonType Type { get; set; }
        public string CpfCnpj { get; set; } = default!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Profession { get; set; }
        public decimal? IncomeEstimation { get; set; }
        public int ScoreInternal { get; set; }
        public string? Notes { get; set; }
    }
}
