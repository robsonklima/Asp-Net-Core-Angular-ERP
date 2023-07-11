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
    [ApiController]
    [Route("api/[controller]")]
    public class StatusServicoController : ControllerBase
    {
        private readonly IStatusServicoService _statusServicoService;

        public StatusServicoController(IStatusServicoService statusServicoService)
        {
            _statusServicoService = statusServicoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] StatusServicoParameters parameters)
        {
            return _statusServicoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codStatusServico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public StatusServico Get(int codStatusServico)
        {
            return _statusServicoService.ObterPorCodigo(codStatusServico);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public StatusServico Post([FromBody] StatusServico statusServico)
        {
            return _statusServicoService.Criar(statusServico: statusServico);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] StatusServico statusServico)
        {
            _statusServicoService.Atualizar(statusServico);
        }

        [HttpDelete("{codStatusServico:int}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codStatusServico)
        {
            _statusServicoService.Deletar(codStatusServico);
        }
    }
}
