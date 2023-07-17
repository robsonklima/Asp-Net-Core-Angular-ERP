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
    public class ORTransporteController : ControllerBase
    {
        private readonly IORTransporteService _ORTransporteService;

        public ORTransporteController(IORTransporteService ORTransporteService)
        {
            _ORTransporteService = ORTransporteService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ORTransporteParameters parameters)
        {
            return _ORTransporteService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTransportadora}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ORTransporte Get(int codTransportadora)
        {
            return _ORTransporteService.ObterPorCodigo(codTransportadora);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ORTransporte ORTransporte)
        {
            _ORTransporteService.Criar(ORTransporte);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ORTransporte ORTransporte)
        {
            _ORTransporteService.Atualizar(ORTransporte);
        }

        [HttpDelete("{codTransportadora}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codTransportadora)
        {
            _ORTransporteService.Deletar(codTransportadora);
        }
    }
}
