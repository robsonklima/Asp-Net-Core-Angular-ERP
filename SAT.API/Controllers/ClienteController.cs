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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteInterface;

        public ClienteController(IClienteRepository clienteInterface)
        {
            _clienteInterface = clienteInterface;
        }

        [HttpGet("{codCliente}")]
        public Cliente Get(int codCliente)
        {
            return _clienteInterface.ObterPorCodigo(codCliente);
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ClienteParameters parameters)
        {
            var clientes = _clienteInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = clientes,
                TotalCount = clientes.TotalCount,
                CurrentPage = clientes.CurrentPage,
                PageSize = clientes.PageSize,
                TotalPages = clientes.TotalPages,
                HasNext = clientes.HasNext,
                HasPrevious = clientes.HasPrevious
            };

            return lista;
        }
    }
}
