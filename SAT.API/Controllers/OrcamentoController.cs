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

        [HttpGet("{codOrcamento}")]
        public Orcamento Get(int codOrcamento)
        {
            return _orcamentoService.ObterPorCodigo(codOrcamento);
        }

        [HttpPost]
        public void Post([FromBody] Orcamento orcamento)
        {
            _orcamentoService.Criar(orcamento);
        }

        [HttpPut]
        public void Put([FromBody] Orcamento orcamento)
        {
            _orcamentoService.Atualizar(orcamento);
        }

        [HttpDelete("{codOrcamento}")]
        public void Delete(int codOrcamento)
        {
            _orcamentoService.Deletar(codOrcamento);
        }
    }
}
