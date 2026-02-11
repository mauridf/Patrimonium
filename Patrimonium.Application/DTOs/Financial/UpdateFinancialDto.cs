using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Financial
{
    public class UpdateFinancialDto
    {
        public FinancialType Type { get; set; }
        public FinancialCategory Category { get; set; }
        public string Description { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CompetenceMonth { get; set; }
        public bool IsPaid { get; set; }
    }
}
