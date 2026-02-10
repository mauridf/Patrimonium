using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Person : AuditableEntity
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
