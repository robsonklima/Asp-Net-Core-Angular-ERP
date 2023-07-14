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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class RecursoBloqueadoController : ControllerBase
    {
        public IRecursoBloqueadoService _recursoBloqueadoService { get; }

        public RecursoBloqueadoController(IRecursoBloqueadoService recursoBloqueadoService)
        {
            _recursoBloqueadoService = recursoBloqueadoService;
        }

        [HttpGet]
          public ListViewModel Get([FromQuery] RecursoBloqueadoParameters parameters)
        {
            return _recursoBloqueadoService.ObterPorParametros(parameters);
        }

         
        [HttpGet("{CodRecursoBloqueado}")]
          public RecursoBloqueado Get(int codRecursoBloqueado)
        {
            return _recursoBloqueadoService.ObterPorCodigo(codRecursoBloqueado);
        }

        [HttpPost]
        public void Post([FromBody] RecursoBloqueado recursoBloqueado)
        {
            _recursoBloqueadoService.Criar(recursoBloqueado);
        }

        [HttpPut]
          public void Put([FromBody] RecursoBloqueado recursoBloqueado)
        {
            _recursoBloqueadoService.Atualizar(recursoBloqueado);
        }

        [HttpDelete("{CodRecursoBloqueado}")]
          public void Delete(int codRecursoBloqueado)
        {
            _recursoBloqueadoService.Deletar(codRecursoBloqueado);
        }
    }
}
