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
            return this._clienteService.ObterPorCodigo(codCliente);
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ClienteParameters parameters)
        {
            return this._clienteService.ObterPorParametros(parameters);
        }

        [HttpPost]
        public Cliente Post([FromBody] Cliente cliente)
        {
            return this._clienteService.Criar(cliente);
        }

        [HttpPut]
        public void Put([FromBody] Cliente cliente)
        {
            this._clienteService.Atualizar(cliente);
        }

        [HttpDelete("{codCliente}")]
        public void Delete(int codCliente)
        {
            this._clienteService.Deletar(codCliente);
        }
    }
}
