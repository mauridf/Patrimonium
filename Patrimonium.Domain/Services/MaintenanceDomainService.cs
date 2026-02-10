using Patrimonium.Domain.Entities;

namespace Patrimonium.Domain.Services
{
    public class MaintenanceDomainService
    {
        public void Validate(Maintenance m)
        {
            if (m.PropertyId == Guid.Empty)
                throw new Exception("Imóvel obrigatório");

            if (string.IsNullOrWhiteSpace(m.Title))
                throw new Exception("Título obrigatório");

            if (m.Priority < 1 || m.Priority > 4)
                throw new Exception("Prioridade inválida");

            if (m.OpenedAt == default)
                throw new Exception("Data de abertura inválida");
        }
    }
}
