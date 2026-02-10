using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.UseCases.Document
{
    public class DocumentCrudUseCase : BaseCrudUseCase<Patrimonium.Domain.Entities.Document>
    {
        public DocumentCrudUseCase(IRepository<Patrimonium.Domain.Entities.Document> repo, IUnitOfWork uow)
            : base(repo, uow) { }
    }
}
