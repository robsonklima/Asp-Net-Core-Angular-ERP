using System.Collections.Generic;
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
        public ListViewModel Get([FromQuery] OrcamentoParameters parameters)
        {
            return _orcamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("View")]
        public ListViewModel GetView([FromQuery] OrcamentoParameters parameters)
        {
            return _orcamentoService.ObterPorView(parameters);
        }

        [HttpGet("{codOrcamento}")]
        public Orcamento Get(int codOrcamento)
        {
            return _orcamentoService.ObterPorCodigo(codOrcamento);
        }

        [HttpPost]
        public Orcamento Post([FromBody] Orcamento orcamento)
        {
            return _orcamentoService.Criar(orcamento);
        }

        [HttpPut]
        public Orcamento Put([FromBody] Orcamento orcamento)
        {
            return _orcamentoService.Atualizar(orcamento);
        }

        [HttpDelete("{codOrcamento}")]
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