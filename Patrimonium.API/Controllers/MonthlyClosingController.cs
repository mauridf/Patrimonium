using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/monthly-closing")]
    [Authorize]
    public class MonthlyClosingController : ControllerBase
    {
        private readonly IMonthlyClosingEngine _engine;
        private readonly IMonthlyClosingRepository _repository;

        public MonthlyClosingController(
            IMonthlyClosingEngine engine,
            IMonthlyClosingRepository repository)
        {
            _engine = engine;
            _repository = repository;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromQuery] int year, [FromQuery] int month)
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);

            var closing = await _engine.GenerateAsync(userId, year, month);

            return Ok(new
            {
                closing.Id,
                Period = closing.Period.ToString(),
                closing.Version,
                closing.Status,
                closing.GeneratedAt
            });
        }

        [HttpPost("{id}/lock")]
        public async Task<IActionResult> Lock(Guid id)
        {
            var closing = await _repository.GetByPeriodAsync(
                Guid.Parse(User.FindFirst("sub")!.Value),
                DateTime.UtcNow.Year,
                DateTime.UtcNow.Month
            );

            if (closing == null)
                return NotFound();

            closing.Lock();
            await _repository.SaveAsync(closing);

            return Ok(new { status = "locked", closing.Id });
        }

        [HttpGet("{year}/{month}")]
        public async Task<IActionResult> Get(int year, int month)
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);

            var closing = await _repository.GetByPeriodAsync(userId, year, month);

            if (closing == null)
                return NotFound();

            return Ok(closing);
        }

        [HttpGet("history")]
        public async Task<IActionResult> History()
        {
            // preparado para versionamento e histórico completo
            return Ok("Histórico completo será implementado no próximo passo");
        }

        [HttpGet("compare/{y1}/{m1}/{y2}/{m2}")]
        public IActionResult Compare(int y1, int m1, int y2, int m2)
        {
            return Ok("Comparador mensal será implementado");
        }

        [HttpGet("export/{year}/{month}")]
        public IActionResult Export(int year, int month)
        {
            return Ok("Exportação contábil/fiscal será implementada");
        }
    }
}
