using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Domain.Entities;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseCrudController<T> : ControllerBase where T : AuditableEntity
    {
        protected abstract BaseCrudUseCase<T> UseCase { get; }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirst("id")!.Value);
            var result = await UseCase.GetAll(userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var result = await UseCase.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await UseCase.Delete(id);
            return NoContent();
        }
    }

}
