using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaConfiguracaoController : ControllerBase
    {
        private readonly IDespesaConfiguracaoService _despesaService;

        public DespesaConfiguracaoController(IDespesaConfiguracaoService despesaService)
        {
            _despesaService = despesaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaConfiguracaoParameters parameters)
        {
            return _despesaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codDespesaConfiguracao}")]
        public DespesaConfiguracao Get(int codDespesaConfiguracao) =>
             _despesaService.ObterPorCodigo(codDespesaConfiguracao);

        [HttpPost]
        public void Post([FromBody] DespesaConfiguracao despesa) =>
            _despesaService.Criar(despesa);

        [HttpPut]
        public void Put([FromBody] DespesaConfiguracao despesa) =>
            _despesaService.Atualizar(despesa);
    }
}