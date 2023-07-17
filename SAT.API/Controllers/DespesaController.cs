using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaService _despesaService;

        public DespesaController(IDespesaService despesaService) =>
            _despesaService = despesaService;

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaParameters parameters) =>
            _despesaService.ObterPorParametros(parameters);

        [HttpGet("{codDespesa}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Despesa Get(int codDespesa) =>
             _despesaService.ObterPorCodigo(codDespesa);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Despesa Post([FromBody] Despesa despesa) {
            return _despesaService.Criar(despesa);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Despesa despesa) =>
            _despesaService.Atualizar(despesa);

        [HttpDelete("{codDespesa}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDespesa) =>
            _despesaService.Deletar(codDespesa);

        [HttpGet("Impressao")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewDespesaImpressaoItem> Impressao([FromQuery] DespesaParameters parameters) =>
            _despesaService.Impressao(parameters);
    }
}