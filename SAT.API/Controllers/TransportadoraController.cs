using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransportadoraController : ControllerBase
    {
        private readonly ITransportadoraRepository _transportadoraInterface;

        public TransportadoraController(ITransportadoraRepository transportadoraInterface)
        {
            _transportadoraInterface = transportadoraInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TransportadoraParameters parameters)
        {
            var transportadoras = _transportadoraInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = transportadoras,
                TotalCount = transportadoras.TotalCount,
                CurrentPage = transportadoras.CurrentPage,
                PageSize = transportadoras.PageSize,
                TotalPages = transportadoras.TotalPages,
                HasNext = transportadoras.HasNext,
                HasPrevious = transportadoras.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codTransportadora}")]
        public Transportadora Get(int codTransportadora)
        {
            return _transportadoraInterface.ObterPorCodigo(codTransportadora);
        }

        [HttpPost]
        public void Post([FromBody] Transportadora transportadora)
        {
            _transportadoraInterface.Criar(transportadora);
        }

        [HttpPut]
        public void Put([FromBody] Transportadora transportadora)
        {
            _transportadoraInterface.Atualizar(transportadora);
        }

        [HttpDelete("{codTransportadora}")]
        public void Delete(int codTransportadora)
        {
            _transportadoraInterface.Deletar(codTransportadora);
        }
    }
}
