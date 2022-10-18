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
    public class ORController : ControllerBase
    {
        private readonly IORService _orService;

        public ORController(IORService orService)
        {
            _orService = orService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ORParameters parameters)
        {
            return orService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOR}")]
        public OR Get(int codOR)
        {
            return _orService.ObterPorCodigo(codOR);
        }

        [HttpPost]
        public void Post([FromBody] OR or)
        {
            _orService.Criar(or);
        }

        [HttpPut]
        public void Put([FromBody] OR or)
        {
            _orService.Atualizar(or);
        }

        [HttpDelete("{codOR}")]
        public void Delete(int codOR)
        {
            _orService.Deletar(codOR);
        }
    }
}
