using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Maintenance
{
    public class MaintenanceCrudUseCase : BaseCrudUseCase<Patrimonium.Domain.Entities.Maintenance>
    {
        public MaintenanceCrudUseCase(IRepository<Patrimonium.Domain.Entities.Maintenance> repo, IUnitOfWork uow)
            : base(repo, uow) { }
    }

}
