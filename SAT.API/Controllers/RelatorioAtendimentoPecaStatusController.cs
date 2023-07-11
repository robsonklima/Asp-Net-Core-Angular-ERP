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
    public class RelatorioAtendimentoPecaStatusController : ControllerBase
    {
        public IRelatorioAtendimentoPecaStatusService _relatorioAtendimentoPecaStatusService { get; }

        public RelatorioAtendimentoPecaStatusController(IRelatorioAtendimentoPecaStatusService relatorioAtendimentoPecaStatusService)
        {
            _relatorioAtendimentoPecaStatusService = relatorioAtendimentoPecaStatusService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] RelatorioAtendimentoPecaStatusParameters parameters)
        {
            return _relatorioAtendimentoPecaStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodRATPecasStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public RelatorioAtendimentoPecaStatus Get(int codRatpecasStatus)
        {
            return _relatorioAtendimentoPecaStatusService.ObterPorCodigo(codRatpecasStatus);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus)
        {
            _relatorioAtendimentoPecaStatusService.Criar(relatorioAtendimentoPecaStatus);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus)
        {
            _relatorioAtendimentoPecaStatusService.Atualizar(relatorioAtendimentoPecaStatus);
        }

        [HttpDelete("{CodRATPecasStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codRatpecasStatus)
        {
            _relatorioAtendimentoPecaStatusService.Deletar(codRatpecasStatus);
        }
    }
}
