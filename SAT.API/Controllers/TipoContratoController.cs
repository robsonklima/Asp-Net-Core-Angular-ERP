using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class TipoContratoController : ControllerBase
    {
        private readonly ITipoContratoService _tipoContratoService;

        public TipoContratoController(ITipoContratoService tipoContratoService)
        {
            _tipoContratoService = tipoContratoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TipoContratoParameters parameters)
        {
            return _tipoContratoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoContrato}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TipoContrato Get(int codTipoContrato)
        {
            return _tipoContratoService.ObterPorCodigo(codTipoContrato);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] TipoContrato tipoContrato)
        {
            _tipoContratoService.Criar(tipoContrato);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] TipoContrato tipoContrato)
        {
            _tipoContratoService.Atualizar(tipoContrato);
        }

        [HttpDelete("{codTipoContrato}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codTipoContrato)
        {
            _tipoContratoService.Deletar(codTipoContrato);
        }
    }
}
