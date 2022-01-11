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
    public class OrcamentoOutroServicoController : ControllerBase
    {
        private readonly IOrcamentoOutroServicoService _orcamentoOutroServicoService;

        public OrcamentoOutroServicoController(IOrcamentoOutroServicoService orcamentoOutroServicoService)
        {
            _orcamentoOutroServicoService= orcamentoOutroServicoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrcamentoOutroServicoParameters parameters)
        {
            return _orcamentoOutroServicoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOrcamentoOutroServico}")]
        public OrcamentoOutroServico Get(int codOrcamentoOutroServico)
        {
            return _orcamentoOutroServicoService.ObterPorCodigo(codOrcamentoOutroServico);
        }

        [HttpPost]
        public OrcamentoOutroServico Post([FromBody] OrcamentoOutroServico orcamento)
        {
            return _orcamentoOutroServicoService.Criar(orcamento);
        }

        [HttpPut]
        public OrcamentoOutroServico Put([FromBody] OrcamentoOutroServico orcamentoOutroServico)
        {
            return _orcamentoOutroServicoService.Atualizar(orcamentoOutroServico);
        }

        [HttpDelete("{codOrcamentoOutroServico}")]
        public void Delete(int codOrcamentoOutroServico)
        {
            _orcamentoOutroServicoService.Deletar(codOrcamentoOutroServico);
        }
    }
}