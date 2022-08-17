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
    public class AuditoriaVeiculoAcessorioController : ControllerBase
    {
        private readonly IAuditoriaVeiculoAcessorioService _auditoriaVeiculoAcessorioService;

        public AuditoriaVeiculoAcessorioController(IAuditoriaVeiculoAcessorioService auditoriaVeiculoAcessorioService)
        {
            _auditoriaVeiculoAcessorioService = auditoriaVeiculoAcessorioService;
        }

        [HttpGet]
        // public ListViewModel Get([FromQuery] AuditoriaVeiculoParameters parameters)
        // {
        //     return _auditoriaVeiculoService.ObterPorParametros(parameters);
        // }

        [HttpGet("{codAuditoriaVeiculoAcessorio}")]
        public AuditoriaVeiculoAcessorio Get(int codAuditoriaVeiculoAcessorio)
        {
            return _auditoriaVeiculoAcessorioService.ObterPorCodigo(codAuditoriaVeiculoAcessorio);
        }

        [HttpPost]
        public void Post([FromBody] AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio)
        {
            _auditoriaVeiculoAcessorioService.Criar(auditoriaVeiculoAcessorio);
        }

        [HttpPut("{codAuditoriaVeiculoAcessorio}")]
        public void Put([FromBody] AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio)
        {
            _auditoriaVeiculoAcessorioService.Atualizar(auditoriaVeiculoAcessorio);
        }

        [HttpDelete("{codAuditoriaVeiculoAcessorio}")]
        public void Delete(int codAuditoriaVeiculoAcessorio)
        {
            _auditoriaVeiculoAcessorioService.Deletar(codAuditoriaVeiculoAcessorio);
        }
    }
}
