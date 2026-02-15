using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.People;
using Patrimonium.Application.Services;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/people")]
    [Authorize]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _service;

        public PeopleController(PersonService service)
        {
            _service = service;
        }

        private Guid UserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonDto dto)
        {
            await _service.Create(UserId, dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMine()
        {
            var result = await _service.GetMyPeople(UserId);
            return Ok(result);
        }
    }
}
