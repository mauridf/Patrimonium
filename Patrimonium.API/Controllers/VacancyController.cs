using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/financial/vacancy")]
    [Authorize]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyEngine _engine;

        public VacancyController(IVacancyEngine engine)
        {
            _engine = engine;
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> Vacancy(Guid propertyId)
            => Ok(await _engine.Calculate(propertyId));
    }

}
