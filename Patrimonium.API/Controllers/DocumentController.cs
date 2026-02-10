using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Document;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.UseCases.Document;
using Patrimonium.Domain.Entities;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    [Authorize]
    public class DocumentController : BaseCrudController<Document>
    {
        private readonly ICreateDocumentUseCase _useCase;
        private readonly DocumentCrudUseCase _crudUseCase;

        protected override BaseCrudUseCase<Document> UseCase => _crudUseCase;

        public DocumentController(ICreateDocumentUseCase useCase, DocumentCrudUseCase crudUseCase)
        {
            _useCase = useCase;
            _crudUseCase = crudUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateDocumentDto dto)
        {
            var entity = await UseCase.GetById(id);
            if (entity == null) return NotFound();

            entity.Type = dto.Type;
            entity.Description = dto.Description;
            entity.FileUrl = dto.FileUrl;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = User.FindFirstValue("userId");

            await UseCase.Update(entity);
            return NoContent();
        }
    }
}
