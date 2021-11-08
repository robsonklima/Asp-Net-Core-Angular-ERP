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
    public class DespesaConfiguracaoCombustivelController : ControllerBase
    {
        private readonly IDespesaConfiguracaoCombustivelService _despesaService;

        public DespesaConfiguracaoCombustivelController(IDespesaConfiguracaoCombustivelService despesaService) =>
            _despesaService = despesaService;

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaConfiguracaoCombustivelParameters parameters) =>
            _despesaService.ObterPorParametros(parameters);

        [HttpGet("{codDespesa}")]
        public DespesaConfiguracaoCombustivel Get(int codDespesa) =>
             _despesaService.ObterPorCodigo(codDespesa);

        [HttpPost]
        public void Post([FromBody] DespesaConfiguracaoCombustivel despesa) =>
            _despesaService.Criar(despesa);

        [HttpPut]
        public void Put([FromBody] DespesaConfiguracaoCombustivel despesa) =>
            _despesaService.Atualizar(despesa);

        [HttpDelete("{codDespesa}")]
        public void Delete(int codDespesa) =>
            _despesaService.Deletar(codDespesa);
    }
}