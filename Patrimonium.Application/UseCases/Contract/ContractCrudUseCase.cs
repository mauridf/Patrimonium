using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Contract
{
    public class ContractCrudUseCase : BaseCrudUseCase<Patrimonium.Domain.Entities.Contract>
    {
        public ContractCrudUseCase(IRepository<Patrimonium.Domain.Entities.Contract> repo, IUnitOfWork uow)
            : base(repo, uow) { }
    }
}
