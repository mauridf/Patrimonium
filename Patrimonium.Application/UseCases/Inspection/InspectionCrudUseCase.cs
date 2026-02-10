using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.UseCases.Inspections
{
    public class InspectionCrudUseCase : BaseCrudUseCase<Inspection>
    {
        public InspectionCrudUseCase(IRepository<Inspection> repo, IUnitOfWork uow)
            : base(repo, uow) { }
    }

}
