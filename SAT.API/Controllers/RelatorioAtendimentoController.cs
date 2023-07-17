using System.Security.Claims;
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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class RelatorioAtendimentoController : ControllerBase
    {
        private readonly IRelatorioAtendimentoService _raService;

        public RelatorioAtendimentoController(IRelatorioAtendimentoService raService)
        {
            _raService = raService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] RelatorioAtendimentoParameters parameters)
        {
            return _raService.ObterPorParametros(parameters);
        }

        [HttpGet("{codRAT}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public RelatorioAtendimento Get(int codRAT)
        {
            return _raService.ObterPorCodigo(codRAT);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public RelatorioAtendimento Post([FromBody] RelatorioAtendimento relatorioAtendimento)
        {
            return _raService.Criar(relatorioAtendimento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] RelatorioAtendimento relatorioAtendimento)
        {
            relatorioAtendimento.RelatorioAtendimentoDetalhes = null;
            _raService.Atualizar(relatorioAtendimento);
        }

        [HttpDelete("{codRAT}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public bool Delete(int codRAT)
        {
            return _raService.Deletar(codRAT);
        }
    }
}
