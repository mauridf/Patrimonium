using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Media;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.UseCases.Media;
using Patrimonium.Domain.Entities;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/media")]
    [Authorize]
    public class MediaController : BaseCrudController<Media>
    {
        private readonly ICreateMediaUseCase _useCase;
        private readonly MediaCrudUseCase _crudUseCase;

        protected override BaseCrudUseCase<Media> UseCase => _crudUseCase;

        public MediaController(ICreateMediaUseCase useCase, MediaCrudUseCase crudUseCase)
        {
            _useCase = useCase;
            _crudUseCase = crudUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMediaDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateMediaDto dto)
        {
            var entity = await UseCase.GetById(id);
            if (entity == null) return NotFound();

            entity.Type = dto.Type;
            entity.Url = dto.Url;
            entity.IsCover = dto.IsCover;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = User.FindFirstValue("userId");

            await UseCase.Update(entity);
            return NoContent();
        }
    }
}
