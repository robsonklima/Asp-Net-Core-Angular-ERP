using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    public class TipoContratoController : ControllerBase
    {
        private readonly ITipoContratoService _tipoContratoService;

        public TipoContratoController(ITipoContratoService tipoContratoService)
        {
            _tipoContratoService = tipoContratoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TipoContratoParameters parameters)
        {
            return _tipoContratoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoContrato}")]
        public TipoContrato Get(int codTipoContrato)
        {
            return _tipoContratoService.ObterPorCodigo(codTipoContrato);
        }

        [HttpPost]
        public void Post([FromBody] TipoContrato tipoContrato)
        {
            _tipoContratoService.Criar(tipoContrato);
        }

        [HttpPut]
        public void Put([FromBody] TipoContrato tipoContrato)
        {
            _tipoContratoService.Atualizar(tipoContrato);
        }

        [HttpDelete("{codTipoContrato}")]
        public void Delete(int codTipoContrato)
        {
            _tipoContratoService.Deletar(codTipoContrato);
        }
    }
}
