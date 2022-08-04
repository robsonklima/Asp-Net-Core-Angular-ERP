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
    public class OrcamentosFaturamentoController : ControllerBase
    {
        private readonly IOrcamentosFaturamentoService  _orcamentosFaturamentoService;

        public OrcamentosFaturamentoController(IOrcamentosFaturamentoService orcamentosFaturamentoService)
        {
            _orcamentosFaturamentoService = orcamentosFaturamentoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrcamentosFaturamentoParameters parameters)
        {
            return _orcamentosFaturamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOrcamentoFaturamento}")]
        public OrcamentosFaturamento Get(int codOrcamentoFaturamento)
        {
            return _orcamentosFaturamentoService.ObterPorCodigo(codOrcamentoFaturamento);
        }

        [HttpPost]
        public OrcamentosFaturamento Post([FromBody] OrcamentosFaturamento orcamento)
        {
            return _orcamentosFaturamentoService.Criar(orcamento);
        }

        [HttpPut]
        public OrcamentosFaturamento Put([FromBody] OrcamentosFaturamento orcamento)
        {
            return _orcamentosFaturamentoService.Atualizar(orcamento);
        }

        [HttpDelete("{codOrcamentoFaturamento}")]
        public void Delete(int codOrcamentoFaturamento)
        {
            _orcamentosFaturamentoService.Deletar(codOrcamentoFaturamento);
        }
    }
}