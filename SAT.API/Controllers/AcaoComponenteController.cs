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
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class AcaoComponenteController : ControllerBase
    {
        private readonly IAcaoComponenteService _acaoComponenteService;

        public AcaoComponenteController(IAcaoComponenteService acaoComponenteService)
        {
            _acaoComponenteService = acaoComponenteService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AcaoComponenteParameters parameters)
        {
            return _acaoComponenteService.ObterPorParametros(parameters);
        }

        [HttpGet("{codAcaoComponente}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public AcaoComponente Get(int codAcaoComponente)
        {
            return _acaoComponenteService.ObterPorCodigo(codAcaoComponente);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] AcaoComponente acao)
        {
            _acaoComponenteService.Criar(acao);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] AcaoComponente acao)
        {
            _acaoComponenteService.Atualizar(acao);
        }

        [HttpDelete("{codAcaoComponente}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAcao)
        {
            _acaoComponenteService.Deletar(codAcao);
        }
    }
}
