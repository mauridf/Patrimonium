using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Persons
{
    public class PersonCrudUseCase : BaseCrudUseCase<Patrimonium.Domain.Entities.Person>
    {
        public PersonCrudUseCase(IRepository<Patrimonium.Domain.Entities.Person> repo, IUnitOfWork uow)
            : base(repo, uow) { }
    }
}
