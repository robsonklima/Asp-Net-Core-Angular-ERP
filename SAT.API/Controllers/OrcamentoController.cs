using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentoController : ControllerBase
    {
        private readonly IOrcamentoService _orcamentoService;

        public OrcamentoController(IOrcamentoService orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OrcamentoParameters parameters)
        {
            return _orcamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("View")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel GetView([FromQuery] OrcamentoParameters parameters)
        {
            return _orcamentoService.ObterPorView(parameters);
        }

        [HttpGet("{codOrcamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Orcamento Get(int codOrcamento)
        {
            return _orcamentoService.ObterPorCodigo(codOrcamento);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Orcamento Post([FromBody] Orcamento orcamento)
        {
            return _orcamentoService.Criar(orcamento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public Orcamento Put([FromBody] Orcamento orcamento)
        {
            return _orcamentoService.Atualizar(orcamento);
        }

        [HttpDelete("{codOrcamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codOrcamento)
        {
            _orcamentoService.Deletar(codOrcamento);
        }

        [AllowAnonymous]
        [HttpPost("AprovacaoCliente")]
        public OrcamentoAprovacao Post([FromBody] OrcamentoAprovacao aprovacao)
        {
            return _orcamentoService.Aprovar(aprovacao);
        }
    }
}