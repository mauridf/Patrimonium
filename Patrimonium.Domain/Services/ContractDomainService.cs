using Patrimonium.Domain.Entities;

namespace Patrimonium.Domain.Services
{
    public class ContractDomainService
    {
        public void Validate(Contract c)
        {
            if (c.PropertyId == Guid.Empty)
                throw new Exception("Imóvel obrigatório");

            if (c.PersonId == Guid.Empty)
                throw new Exception("Pessoa obrigatória");

            if (c.StartDate == default)
                throw new Exception("Data inicial obrigatória");

            if (c.MonthlyValue <= 0 && c.DailyValue <= 0)
                throw new Exception("Valor do contrato inválido");

            if (c.FinePercentage < 0 || c.FinePercentage > 100)
                throw new Exception("Multa inválida");

            if (c.AdjustmentPeriodMonths < 0)
                throw new Exception("Período de reajuste inválido");
        }
    }
}
