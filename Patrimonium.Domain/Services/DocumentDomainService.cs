using Patrimonium.Domain.Entities;

namespace Patrimonium.Domain.Services
{
    public class DocumentDomainService
    {
        public void Validate(Document doc)
        {
            if (string.IsNullOrWhiteSpace(doc.FileUrl))
                throw new Exception("Arquivo é obrigatório");

            if (doc.PropertyId == null && doc.ContractId == null)
                throw new Exception("Documento deve estar vinculado a um imóvel ou contrato");
        }
    }
}
