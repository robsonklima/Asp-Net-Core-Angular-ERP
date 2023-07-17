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
    public class PlantaoTecnicoClienteController : ControllerBase
    {
        private readonly IPlantaoTecnicoClienteService _plantaoTecnicoClienteService;

        public PlantaoTecnicoClienteController(IPlantaoTecnicoClienteService plantaoTecnicoClienteService)
        {
            _plantaoTecnicoClienteService = plantaoTecnicoClienteService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PlantaoTecnicoClienteParameters parameters)
        {
            return _plantaoTecnicoClienteService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPlantaoTecnicoCliente}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PlantaoTecnicoCliente Get(int codPlantaoTecnicoCliente)
        {
            return _plantaoTecnicoClienteService.ObterPorCodigo(codPlantaoTecnicoCliente);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public PlantaoTecnicoCliente Post([FromBody] PlantaoTecnicoCliente plantaoTecnicoCliente)
        {
            return _plantaoTecnicoClienteService.Criar(plantaoTecnicoCliente);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PlantaoTecnicoCliente plantaoTecnicoCliente)
        {
            _plantaoTecnicoClienteService.Atualizar(plantaoTecnicoCliente);
        }

        [HttpDelete("{codPlantaoTecnicoCliente}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPlantaoTecnicoCliente)
        {
            _plantaoTecnicoClienteService.Deletar(codPlantaoTecnicoCliente);
        }
    }
}
