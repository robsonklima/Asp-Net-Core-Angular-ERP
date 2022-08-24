using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;

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
        public ListViewModel Get([FromQuery] DespesaParameters parameters) =>
            _despesaService.ObterPorParametros(parameters);

        [HttpGet("{codDespesa}")]
        public Despesa Get(int codDespesa) =>
             _despesaService.ObterPorCodigo(codDespesa);

        [HttpPost]
        public Despesa Post([FromBody] Despesa despesa) {
            return _despesaService.Criar(despesa);
        }

        [HttpPut]
        public void Put([FromBody] Despesa despesa) =>
            _despesaService.Atualizar(despesa);

        [HttpDelete("{codDespesa}")]
        public void Delete(int codDespesa) =>
            _despesaService.Deletar(codDespesa);

        [HttpGet("Impressao")]
        public List<ViewDespesaImpressaoItem> Impressao([FromQuery] DespesaParameters parameters) =>
            _despesaService.Impressao(parameters);
    }
}