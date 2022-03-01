using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransportadoraController : ControllerBase
    {
        private readonly ITransportadoraService _transportadoraService;

        public TransportadoraController(ITransportadoraService transportadoraService)
        {
            _transportadoraService = transportadoraService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TransportadoraParameters parameters)
        {
            return _transportadoraService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTransportadora}")]
        public Transportadora Get(int codTransportadora)
        {
            return _transportadoraService.ObterPorCodigo(codTransportadora);
        }

        [HttpPost]
        public void Post([FromBody] Transportadora transportadora)
        {
            _transportadoraService.Criar(transportadora);
        }

        [HttpPut]
        public void Put([FromBody] Transportadora transportadora)
        {
            _transportadoraService.Atualizar(transportadora);
        }

        [HttpDelete("{codTransportadora}")]
        public void Delete(int codTransportadora)
        {
            _transportadoraService.Deletar(codTransportadora);
        }
    }
}
