using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;

public class PersonCrudUseCase : BaseCrudUseCase<Person>
{
    public PersonCrudUseCase(IRepository<Person> repo, IUnitOfWork uow)
        : base(repo, uow) { }
}
