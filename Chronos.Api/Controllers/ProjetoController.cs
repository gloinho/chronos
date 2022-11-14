﻿using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] ProjetoRequest request)
        {
            var response = await _projetoService.CadastrarAsync(request);
            return Created(nameof(CadastrarAsync), response);
        }

        [HttpPost("{id}/colaboradores")]
        public async Task<IActionResult> AdicionarColaboradoresAsync(
            [FromRoute] int id,
            [FromBody] AdicionarColaboradoresRequest request
        )
        {
            var response = await _projetoService.AdicionarColaboradores(id, request);
            return Created(nameof(AdicionarColaboradoresAsync), response);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAsync()
        {
            var response = await _projetoService.ObterTodosAsync();
            return Ok(response);
        }
    }
}
