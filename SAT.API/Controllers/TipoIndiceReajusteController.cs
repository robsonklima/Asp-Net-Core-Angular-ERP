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
    public class TipoIndiceReajusteController : ControllerBase
    {
        private readonly ITipoIndiceReajusteService _tipoIndiceReajusteService;

        public TipoIndiceReajusteController(ITipoIndiceReajusteService tipoIndiceReajusteService)
        {
            _tipoIndiceReajusteService = tipoIndiceReajusteService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TipoIndiceReajusteParameters parameters)
        {
            return _tipoIndiceReajusteService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoIndiceReajuste}")]
        public TipoIndiceReajuste Get(int codTipoIndiceReajuste)
        {
            return _tipoIndiceReajusteService.ObterPorCodigo(codTipoIndiceReajuste);
        }

        [HttpPost]
        public void Post([FromBody] TipoIndiceReajuste tipoIndiceReajuste)
        {
            _tipoIndiceReajusteService.Criar(tipoIndiceReajuste);
        }

        [HttpPut]
        public void Put([FromBody] TipoIndiceReajuste tipoIndiceReajuste)
        {
            _tipoIndiceReajusteService.Atualizar(tipoIndiceReajuste);
        }

        [HttpDelete("{codTipoIndiceReajuste}")]
        public void Delete(int codTipoIndiceReajuste)
        {
            _tipoIndiceReajusteService.Deletar(codTipoIndiceReajuste);
        }
    }
}
