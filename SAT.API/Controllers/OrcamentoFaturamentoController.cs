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
    public class OrcamentoFaturamentoController : ControllerBase
    {
        private readonly IOrcamentoFaturamentoService  _OrcamentoFaturamentoService;

        public OrcamentoFaturamentoController(IOrcamentoFaturamentoService OrcamentoFaturamentoService)
        {
            _OrcamentoFaturamentoService = OrcamentoFaturamentoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrcamentoFaturamentoParameters parameters)
        {
            return _OrcamentoFaturamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOrcamentoFaturamento}")]
        public OrcamentoFaturamento Get(int codOrcamentoFaturamento)
        {
            return _OrcamentoFaturamentoService.ObterPorCodigo(codOrcamentoFaturamento);
        }

        [HttpPost]
        public OrcamentoFaturamento Post([FromBody] OrcamentoFaturamento orcamento)
        {
            return _OrcamentoFaturamentoService.Criar(orcamento);
        }

        [HttpPut]
        public OrcamentoFaturamento Put([FromBody] OrcamentoFaturamento orcamento)
        {
            return _OrcamentoFaturamentoService.Atualizar(orcamento);
        }

        [HttpDelete("{codOrcamentoFaturamento}")]
        public void Delete(int codOrcamentoFaturamento)
        {
            _OrcamentoFaturamentoService.Deletar(codOrcamentoFaturamento);
        }
    }
}