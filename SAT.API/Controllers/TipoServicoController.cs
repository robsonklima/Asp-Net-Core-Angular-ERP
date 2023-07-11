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
    public class TipoServicoController : ControllerBase
    {
        private readonly ITipoServicoService _tipoServicoService;

        public TipoServicoController(
            ITipoServicoService tipoServicoService
        )
        {
            _tipoServicoService = tipoServicoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TipoServicoParameters parameters)
        {
            return _tipoServicoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoServico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TipoServico Get(int codTipoServico)
        {
            return _tipoServicoService.ObterPorCodigo(codTipoServico);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] TipoServico tipoServico)
        {
            _tipoServicoService.Criar(tipoServico);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] TipoServico tipoServico)
        {
            _tipoServicoService.Atualizar(tipoServico);
        }

        [HttpDelete("{codServico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codServico)
        {
            _tipoServicoService.Deletar(codServico);
        }
    }
}
