using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _cargoService;

        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] CargoParameters parameters)
        {
            return _cargoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codCargo}")]
        public Cargo Get(int codCargo)
        {
            return _cargoService.ObterPorCodigo(codCargo);
        }

        [HttpPost]
        public Cargo Post([FromBody] Cargo cargo)
        {
            return _cargoService.Criar(cargo);
        }

        [HttpPut]
        public void Put([FromBody] Cargo cargo)
        {
            _cargoService.Atualizar(cargo);
        }

        [HttpDelete("{codCargo}")]
        public void Delete(int codCargo)
        {
            _cargoService.Deletar(codCargo);
        }
    }
}
