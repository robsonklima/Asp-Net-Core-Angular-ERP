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
    public class OrcamentoMotivoController : ControllerBase
    {
        private readonly IOrcamentoMotivoService _orcamentoMotivoService;

        public OrcamentoMotivoController(IOrcamentoMotivoService orcamentoMotivoService)
        {
            _orcamentoMotivoService = orcamentoMotivoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrcamentoMotivoParameters parameters)
        {
            return _orcamentoMotivoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOrcMotivo}")]
        public OrcamentoMotivo Get(int codOrcMotivo)
        {
            return _orcamentoMotivoService.ObterPorCodigo(codOrcMotivo);
        }

        [HttpPost]
        public void Post([FromBody] OrcamentoMotivo orcamentoMotivo)
        {
            _orcamentoMotivoService.Criar(orcamentoMotivo);
        }

        [HttpPut("{codOrcMotivo}")]
        public void Put([FromBody] OrcamentoMotivo orcamentoMotivo)
        {
            _orcamentoMotivoService.Atualizar(orcamentoMotivo);
        }

        [HttpDelete("{codOrcMotivo}")]
        public void Delete(int codOrcMotivo)
        {
            _orcamentoMotivoService.Deletar(codOrcMotivo);
        }
    }
}
