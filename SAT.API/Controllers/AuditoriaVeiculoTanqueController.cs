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
    public class AuditoriaVeiculoTanqueController : ControllerBase
    {
        private readonly IAuditoriaVeiculoTanqueService _auditoriaVeiculoTanqueService;

        public AuditoriaVeiculoTanqueController(IAuditoriaVeiculoTanqueService auditoriaVeiculoTanqueService)
        {
            _auditoriaVeiculoTanqueService = auditoriaVeiculoTanqueService;
        }

        [HttpGet]
        // public ListViewModel Get([FromQuery] AuditoriaVeiculoParameters parameters)
        // {
        //     return _auditoriaVeiculoService.ObterPorParametros(parameters);
        // }

        [HttpGet("{codAuditoriaVeiculoTanque}")]
        public AuditoriaVeiculoTanque Get(int codAuditoriaVeiculoTanque)
        {
            return _auditoriaVeiculoTanqueService.ObterPorCodigo(codAuditoriaVeiculoTanque);
        }

        [HttpPost]
        public void Post([FromBody] AuditoriaVeiculoTanque auditoriaVeiculoTanque)
        {
            _auditoriaVeiculoTanqueService.Criar(auditoriaVeiculoTanque);
        }

        [HttpPut("{codAuditoriaVeiculoTanque}")]
        public void Put([FromBody] AuditoriaVeiculoTanque auditoriaVeiculoTanque)
        {
            _auditoriaVeiculoTanqueService.Atualizar(auditoriaVeiculoTanque);
        }

        [HttpDelete("{codAuditoriaVeiculoTanque}")]
        public void Delete(int codAuditoriaVeiculoTanque)
        {
            _auditoriaVeiculoTanqueService.Deletar(codAuditoriaVeiculoTanque);
        }
    }
}
