using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Person;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/people")]
    [Authorize]
    public class PersonController : BaseCrudController<Person>
    {
        private readonly ICreatePersonUseCase _useCase;
        private readonly PersonCrudUseCase _crudUseCase;

        protected override BaseCrudUseCase<Person> UseCase => _crudUseCase;

        public PersonController(ICreatePersonUseCase useCase, PersonCrudUseCase crudUseCase)
        {
            _useCase = useCase;
            _crudUseCase = crudUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePersonDto dto)
        {
            var entity = await UseCase.GetById(id);
            if (entity == null) return NotFound();

            entity.Name = dto.Name;
            entity.Type = dto.Type;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.Profession = dto.Profession;
            entity.IncomeEstimation = dto.IncomeEstimation;
            entity.ScoreInternal = dto.ScoreInternal;
            entity.Notes = dto.Notes;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = User.FindFirstValue("userId");

            await UseCase.Update(entity);
            return NoContent();
        }
    }
}
