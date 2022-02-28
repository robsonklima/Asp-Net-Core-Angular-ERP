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
    public class TipoIntervencaoController : ControllerBase
    {
        private readonly ITipoIntervencaoService _tipoIntervencaoService;

        public TipoIntervencaoController(
            ITipoIntervencaoService tipoIntervencaoService
        )
        {
            _tipoIntervencaoService = tipoIntervencaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TipoIntervencaoParameters parameters)
        {
            return _tipoIntervencaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoIntervencao}")]
        public TipoIntervencao Get(int codTipoIntervencao)
        {
            return _tipoIntervencaoService.ObterPorCodigo(codTipoIntervencao);
        }

        [HttpPost]
        public TipoIntervencao Post([FromBody] TipoIntervencao tipoIntervencao)
        {
            return _tipoIntervencaoService.Criar(tipoIntervencao);
        }

        [HttpPut]
        public void Put([FromBody] TipoIntervencao tipoIntervencao)
        {
            _tipoIntervencaoService.Atualizar(tipoIntervencao);
        }

        [HttpDelete("{codTipoIntervencao}")]
        public void Delete(int codTipoIntervencao)
        {
            _tipoIntervencaoService.Deletar(codTipoIntervencao);
        }
    }
}
