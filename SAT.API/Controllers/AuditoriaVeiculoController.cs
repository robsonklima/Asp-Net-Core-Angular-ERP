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
    public class AuditoriaVeiculoController : ControllerBase
    {
        private readonly IAuditoriaVeiculoService _auditoriaVeiculoService;

        public AuditoriaVeiculoController(IAuditoriaVeiculoService auditoriaVeiculoService)
        {
            _auditoriaVeiculoService = auditoriaVeiculoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] AuditoriaVeiculoParameters parameters)
        {
            return _auditoriaVeiculoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codAuditoriaVeiculo}")]
        public AuditoriaVeiculo Get(int codAuditoriaVeiculo)
        {
            return _auditoriaVeiculoService.ObterPorCodigo(codAuditoriaVeiculo);
        }

        [HttpPost]
        public void Post([FromBody] AuditoriaVeiculo auditoriaVeiculo)
        {
            _auditoriaVeiculoService.Criar(auditoriaVeiculo);
        }

        [HttpPut("{codAuditoriaVeiculo}")]
        public void Put([FromBody] AuditoriaVeiculo auditoriaVeiculo)
        {
            _auditoriaVeiculoService.Atualizar(auditoriaVeiculo);
        }

        [HttpDelete("{codAuditoriaVeiculo}")]
        public void Delete(int codAuditoriaVeiculo)
        {
            _auditoriaVeiculoService.Deletar(codAuditoriaVeiculo);
        }
    }
}
