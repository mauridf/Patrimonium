using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Property;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/properties")]
    [Authorize]
    public class PropertiesController : BaseCrudController<Property>
    {
        private readonly ICreatePropertyUseCase _createUseCase;
        private readonly BaseCrudUseCase<Property> _crudUseCase;

        protected override BaseCrudUseCase<Property> UseCase => _crudUseCase;

        public PropertiesController(
            ICreatePropertyUseCase createUseCase,
            BaseCrudUseCase<Property> crudUseCase
        )
        {
            _createUseCase = createUseCase;
            _crudUseCase = crudUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _createUseCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePropertyDTO dto)
        {
            var entity = await UseCase.GetById(id);
            if (entity == null) return NotFound();

            entity.InternalName = dto.InternalName;
            entity.Description = dto.Description;
            entity.UpdatedAt = DateTime.UtcNow;

            await UseCase.Update(entity);
            return NoContent();
        }
    }
}
