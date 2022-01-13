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
    public class OrcamentoDescontoController : ControllerBase
    {
        private readonly IOrcamentoDescontoService _orcamentoDescService;

        public OrcamentoDescontoController(IOrcamentoDescontoService orcamentoDescService)
        {
            _orcamentoDescService = orcamentoDescService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrcamentoDescontoParameters parameters) =>
            _orcamentoDescService.ObterPorParametros(parameters);

        [HttpGet("{codOrcamentoDesc}")]
        public OrcamentoDesconto Get(int codOrcamentoDesc) =>
            _orcamentoDescService.ObterPorCodigo(codOrcamentoDesc);

        [HttpPost]
        public OrcamentoDesconto Post([FromBody] OrcamentoDesconto desconto) =>
            _orcamentoDescService.Criar(desconto);

        [HttpPut]
        public OrcamentoDesconto Put([FromBody] OrcamentoDesconto desconto) =>
            _orcamentoDescService.Atualizar(desconto);

        [HttpDelete("{codOrcamentoDesc}")]
        public void Delete(int codOrcamentoDesc) =>
            _orcamentoDescService.Deletar(codOrcamentoDesc);
    }
}