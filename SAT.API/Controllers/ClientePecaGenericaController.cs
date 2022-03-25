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
    public class ClientePecaGenericaController : ControllerBase
    {
        private readonly IClientePecaGenericaService _clientePecaGenericaService;

        public ClientePecaGenericaController(IClientePecaGenericaService clientePecaGenericaService)
        {
            _clientePecaGenericaService = clientePecaGenericaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ClientePecaGenericaParameters parameters)
        {
            return _clientePecaGenericaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codClientePecaGenerica}")]
        public ClientePecaGenerica Get(int codClientePecaGenerica)
        {
            return _clientePecaGenericaService.ObterPorCodigo(codClientePecaGenerica);
        }

        [HttpPost]
        public void Post([FromBody] ClientePecaGenerica pecaGenerica)
        {
            _clientePecaGenericaService.Criar(pecaGenerica);
        }

        [HttpPut]
        public void Put([FromBody] ClientePecaGenerica pecaGenerica)
        {
            _clientePecaGenericaService.Atualizar(pecaGenerica);
        }

        [HttpDelete("{codPecaGenerica}")]
        public void Delete(int codPecaGenerica)
        {
            _clientePecaGenericaService.Deletar(codPecaGenerica);
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel([FromQuery] ClientePecaGenericaParameters parameters)
        {
            return _clientePecaGenericaService.ExportToExcel(parameters);
        }
    }
}