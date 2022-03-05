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
        public ListViewModel Get([FromQuery] PlantaoTecnicoParameters parameters)
        {
            return _plantaoTecnicoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPlantaoTecnico}")]
        public PlantaoTecnico Get(int codPlantaoTecnico)
        {
            return _plantaoTecnicoService.ObterPorCodigo(codPlantaoTecnico);
        }

        [HttpPost]
        public PlantaoTecnico Post([FromBody] PlantaoTecnico plantaoTecnico)
        {
            return _plantaoTecnicoService.Criar(plantaoTecnico);
        }

        [HttpPut]
        public void Put([FromBody] PlantaoTecnico plantaoTecnico)
        {
            _plantaoTecnicoService.Atualizar(plantaoTecnico);
        }

        [HttpDelete("{codPlantaoTecnico}")]
        public void Delete(int codPlantaoTecnico)
        {
            _plantaoTecnicoService.Deletar(codPlantaoTecnico);
        }
    }
}
