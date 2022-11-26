using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TogglController : ControllerBase
    {
        private readonly ITogglService _togglService;

        public TogglController(ITogglService togglService)
        {
            _togglService = togglService;
        }

        /// <summary>
        /// Através dessa rota você será capaz ver os registros de horas do Toggl e importa-los para o Chronos.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna as informações detalhadas do toggl </response>
        [HttpGet("importar")]
        public async Task<TogglDetailedResponse> ObterHorasToggl(
            [FromQuery] TogglDetailedRequest request
        )
        {
            return await _togglService.ObterHorasToggl(request);
        }
    }
}
