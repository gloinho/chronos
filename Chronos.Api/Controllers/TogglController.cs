using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TogglController : ControllerBase
    {
        private readonly ITogglService _togglService;

        public TogglController(ITogglService togglService)
        {
            _togglService = togglService;   
        }

        [HttpGet]
        public async Task<TogglDetailedResponse> ObterHorasToggl([FromQuery]TogglDetailedRequest request)
        {
            return await _togglService.ObterHorasToggl(request);
        }

    }
}
