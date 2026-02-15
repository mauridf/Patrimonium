using Patrimonium.Application.DTOs.People;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class PersonService
    {
        private readonly IPersonRepository _repo;

        public PersonService(IPersonRepository repo)
        {
            _repo = repo;
        }

        public async Task Create(Guid userId, CreatePersonDto dto)
        {
            var person = new Person(
                userId,
                dto.Name,
                dto.Type,
                dto.CpfCnpj,
                dto.Email,
                dto.Phone,
                dto.Profession,
                dto.IncomeEstimation,
                dto.ScoreInternal,
                dto.Notes
            );

            await _repo.Add(person);
            await _repo.SaveChanges();
        }

        public Task<List<Person>> GetMyPeople(Guid userId)
            => _repo.GetByUser(userId);
    }
}
