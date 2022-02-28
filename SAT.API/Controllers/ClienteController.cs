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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("{codCliente}")]
        public Cliente Get(int codCliente)
        {
            return _clienteService.ObterPorCodigo(codCliente);
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ClienteParameters parameters)
        {
            return _clienteService.ObterPorParametros(parameters);
        }
    }
}
