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
    public class InstalacaoMotivoResController : ControllerBase
    {
        private readonly IInstalacaoMotivoResService _instalacaoMotivoResService;

        public InstalacaoMotivoResController(
            IInstalacaoMotivoResService instalacaoMotivoResService
        )
        {
            _instalacaoMotivoResService = instalacaoMotivoResService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] InstalacaoMotivoResParameters parameters)
        {
            return _instalacaoMotivoResService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalMotivoRes}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public InstalacaoMotivoRes Get(int CodInstalMotivoRes)
        {
            return _instalacaoMotivoResService.ObterPorCodigo(CodInstalMotivoRes);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public InstalacaoMotivoRes Post([FromBody] InstalacaoMotivoRes instalacaoMotivoRes)
        {
            return _instalacaoMotivoResService.Criar(instalacaoMotivoRes);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] InstalacaoMotivoRes instalacaoMotivoRes)
        {
            _instalacaoMotivoResService.Atualizar(instalacaoMotivoRes);
        }

        [HttpDelete("{CodInstalMotivoRes}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int CodInstalMotivoRes)
        {
            _instalacaoMotivoResService.Deletar(CodInstalMotivoRes);
        }
    }
}
