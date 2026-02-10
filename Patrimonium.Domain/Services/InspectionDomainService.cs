using Patrimonium.Domain.Entities;

namespace Patrimonium.Domain.Services
{
    public class InspectionDomainService
    {
        public void Validate(Inspection inspection)
        {
            if (inspection.PropertyId == Guid.Empty)
                throw new Exception("Imóvel é obrigatório");

            if (string.IsNullOrWhiteSpace(inspection.Report))
                throw new Exception("Relatório é obrigatório");

            if (inspection.ScoreCondition < 0 || inspection.ScoreCondition > 100)
                throw new Exception("Score deve ser entre 0 e 100");

            if (inspection.InspectionDate == default)
                throw new Exception("Data da vistoria inválida");
        }
    }
}
