namespace Patrimonium.Application.DTOs.Consolidation
{
    public class FinancialTransactionDto
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid? ContractId { get; set; }

        public string Type { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
