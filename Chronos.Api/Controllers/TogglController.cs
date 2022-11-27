using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Chronos.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
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
        public async Task<ActionResult<TogglDetailedResponse>> ObterHorasToggl(
            [FromQuery] TogglDetailedRequest request
        )
        {
            var response = await _togglService.ObterHorasToggl(request);

            return Ok(response);
        }
    }
}
