using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

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
        public ListViewModel Get([FromQuery] DespesaAdiantamentoParameters parameters) =>
            _despesaAdiantamentoService.ObterPorParametros(parameters);

        [HttpGet("{codDespesaAdiantamento}")]
        public DespesaAdiantamento Get(int codDespesaAdiantamento) =>
             _despesaAdiantamentoService.ObterPorCodigo(codDespesaAdiantamento);

        [HttpPost]
        public void Post([FromBody] DespesaAdiantamento despesa) =>
            _despesaAdiantamentoService.Criar(despesa);

        [HttpPut]
        public void Put([FromBody] DespesaAdiantamento despesa) =>
            _despesaAdiantamentoService.Atualizar(despesa);

        [HttpDelete("{codDespesaAdiantamento}")]
        public void Delete(int codDespesaAdiantamento) =>
            _despesaAdiantamentoService.Deletar(codDespesaAdiantamento);
    }
}