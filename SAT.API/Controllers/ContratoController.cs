using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoInterface;

        public ContratoController(IContratoService contratoInterface)
        {
            _contratoInterface = contratoInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoParameters parameters)
        {
            return _contratoInterface.ObterPorParametros(parameters);
        }

        [HttpGet("{codContrato}")]
        public Contrato Get(int codContrato)
        {
            return _contratoInterface.ObterPorCodigo(codContrato);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        [HttpPut]
        public void Put([FromBody] string value)
        {

        }

        [HttpDelete("{codContrato}")]
        public void Delete(int codContrato)
        {

        }
    }
}
