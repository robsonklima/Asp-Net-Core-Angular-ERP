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
    public class ClientePecaController : ControllerBase
    {
        private readonly IClientePecaService _clientePecaService;

        public ClientePecaController(IClientePecaService clientePecaService)
        {
            _clientePecaService = clientePecaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ClientePecaParameters parameters)
        {
            return _clientePecaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codClientePeca}")]
        public ClientePeca Get(int codClientePeca)
        {
            return _clientePecaService.ObterPorCodigo(codClientePeca);
        }

        [HttpPost]
        public void Post([FromBody] ClientePeca clientePeca)
        {
            _clientePecaService.Criar(clientePeca);
        }

        [HttpPut]
        public void Put([FromBody] ClientePeca clientePeca)
        {
            _clientePecaService.Atualizar(clientePeca);
        }

        [HttpDelete("{codClientePeca}")]
        public void Delete(int codClientePeca)
        {
            _clientePecaService.Deletar(codClientePeca);
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel([FromQuery] ClientePecaParameters parameters)
        {
            return _clientePecaService.ExportToExcel(parameters);
        }
    }
}