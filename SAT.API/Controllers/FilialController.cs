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
    public class FilialController : ControllerBase
    {
        private readonly IFilialService _filialService;

        public FilialController(IFilialService filialService)
        {
            _filialService = filialService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] FilialParameters parameters)
        {
            return _filialService.ObterPorParametros(parameters);
        }

        [HttpGet("{codFilial}")]
        public Filial Get(int codFilial)
        {
            return _filialService.ObterPorCodigo(codFilial);
        }

        [HttpPost]
        public void Post([FromBody] Filial filial)
        {
        }

        [HttpPut]
        public void Put([FromBody] Filial filial)
        {
        }

        [HttpDelete("{codFilial}")]
        public void Delete(int codFilial)
        {
        }
    }
}
