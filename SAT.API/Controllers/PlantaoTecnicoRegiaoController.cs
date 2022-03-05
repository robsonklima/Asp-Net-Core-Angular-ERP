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
    public class PlantaoTecnicoRegiaoController : ControllerBase
    {
        private readonly IPlantaoTecnicoRegiaoService _plantaoTecnicoRegiaoService;

        public PlantaoTecnicoRegiaoController(IPlantaoTecnicoRegiaoService plantaoTecnicoRegiaoService)
        {
            _plantaoTecnicoRegiaoService = plantaoTecnicoRegiaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PlantaoTecnicoRegiaoParameters parameters)
        {
            return _plantaoTecnicoRegiaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPlantaoTecnicoRegiao}")]
        public PlantaoTecnicoRegiao Get(int codPlantaoTecnicoRegiao)
        {
            return _plantaoTecnicoRegiaoService.ObterPorCodigo(codPlantaoTecnicoRegiao);
        }

        [HttpPost]
        public PlantaoTecnicoRegiao Post([FromBody] PlantaoTecnicoRegiao plantaoTecnicoRegiao)
        {
            return _plantaoTecnicoRegiaoService.Criar(plantaoTecnicoRegiao);
        }

        [HttpPut]
        public void Put([FromBody] PlantaoTecnicoRegiao plantaoTecnicoRegiao)
        {
            _plantaoTecnicoRegiaoService.Atualizar(plantaoTecnicoRegiao);
        }

        [HttpDelete("{codPlantaoTecnicoRegiao}")]
        public void Delete(int codPlantaoTecnicoRegiao)
        {
            _plantaoTecnicoRegiaoService.Deletar(codPlantaoTecnicoRegiao);
        }
    }
}
