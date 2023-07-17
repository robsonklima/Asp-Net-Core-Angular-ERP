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
    public class PlantaoTecnicoController : ControllerBase
    {
        private readonly IPlantaoTecnicoService _plantaoTecnicoService;

        public PlantaoTecnicoController(IPlantaoTecnicoService plantaoTecnicoService)
        {
            _plantaoTecnicoService = plantaoTecnicoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PlantaoTecnicoParameters parameters)
        {
            return _plantaoTecnicoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPlantaoTecnico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PlantaoTecnico Get(int codPlantaoTecnico)
        {
            return _plantaoTecnicoService.ObterPorCodigo(codPlantaoTecnico);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public PlantaoTecnico Post([FromBody] PlantaoTecnico plantaoTecnico)
        {
            return _plantaoTecnicoService.Criar(plantaoTecnico);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PlantaoTecnico plantaoTecnico)
        {
            _plantaoTecnicoService.Atualizar(plantaoTecnico);
        }

        [HttpDelete("{codPlantaoTecnico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPlantaoTecnico)
        {
            _plantaoTecnicoService.Deletar(codPlantaoTecnico);
        }
    }
}
