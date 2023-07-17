using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaAdiantamentoController : ControllerBase
    {
        private readonly IDespesaAdiantamentoService _despesaAdiantamentoService;

        public DespesaAdiantamentoController(IDespesaAdiantamentoService despesaAdiantamentoService) =>
            _despesaAdiantamentoService = despesaAdiantamentoService;

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaAdiantamentoParameters parameters) =>
            _despesaAdiantamentoService.ObterPorParametros(parameters);

        [HttpGet("{codDespesaAdiantamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public DespesaAdiantamento Get(int codDespesaAdiantamento) =>
             _despesaAdiantamentoService.ObterPorCodigo(codDespesaAdiantamento);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] DespesaAdiantamento despesa) =>
            _despesaAdiantamentoService.Criar(despesa);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] DespesaAdiantamento despesa) =>
            _despesaAdiantamentoService.Atualizar(despesa);

        [HttpDelete("{codDespesaAdiantamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDespesaAdiantamento) =>
            _despesaAdiantamentoService.Deletar(codDespesaAdiantamento);

        [HttpGet("Media/{codTecnico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewMediaDespesasAdiantamento> GetMedia(int codTecnico) =>
            _despesaAdiantamentoService.ObterMediaAdiantamentos(codTecnico);

        [HttpPost("Solicitacao")]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public DespesaAdiantamentoSolicitacao Post([FromBody] DespesaAdiantamentoSolicitacao solicitacao)
        {
            return _despesaAdiantamentoService.CriarSolicitacao(solicitacao);
        }

        [HttpGet("Pendentes")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel GetByView([FromQuery] DespesaAdiantamentoParameters parameters) =>
            _despesaAdiantamentoService.ObterPorView(parameters);
    }
}