using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Media
{
    public class MediaCrudUseCase : BaseCrudUseCase<Patrimonium.Domain.Entities.Media>
    {
        public MediaCrudUseCase(IRepository<Patrimonium.Domain.Entities.Media> repo, IUnitOfWork uow)
            : base(repo, uow) { }
    }
}
