using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Financial
{
    public class FinancialTransactionCrudUseCase : BaseCrudUseCase<Patrimonium.Domain.Entities.FinancialTransaction>
    {
        public FinancialTransactionCrudUseCase(IRepository<Patrimonium.Domain.Entities.FinancialTransaction> repo, IUnitOfWork uow)
            : base(repo, uow) { }
    }
}
