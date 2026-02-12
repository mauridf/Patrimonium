using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Enums;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Contract
{
    public class ContractLifecycleUseCase : IContractLifecycleUseCase
    {
        private readonly IRepository<Patrimonium.Domain.Entities.Contract> _repo;
        private readonly IUnitOfWork _uow;

        public ContractLifecycleUseCase(IRepository<Patrimonium.Domain.Entities.Contract> repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task Activate(Guid id)
        {
            var c = await _repo.GetByIdAsync(id) ?? throw new Exception("Contrato não encontrado");
            c.Status = ContractStatus.Active;
            await Save(c);
        }

        public async Task Suspend(Guid id)
        {
            var c = await _repo.GetByIdAsync(id) ?? throw new Exception("Contrato não encontrado");
            c.Status = ContractStatus.Suspended;
            await Save(c);
        }

        public async Task Cancel(Guid id)
        {
            var c = await _repo.GetByIdAsync(id) ?? throw new Exception("Contrato não encontrado");
            c.Status = ContractStatus.Cancelled;
            await Save(c);
        }

        public async Task Finish(Guid id)
        {
            var c = await _repo.GetByIdAsync(id) ?? throw new Exception("Contrato não encontrado");
            c.Status = ContractStatus.Finished;
            c.EndDate = DateTime.UtcNow;
            await Save(c);
        }

        private async Task Save(Patrimonium.Domain.Entities.Contract c)
        {
            c.UpdatedAt = DateTime.UtcNow;
            _repo.Update(c);
            await _uow.CommitAsync();
        }
    }
}
