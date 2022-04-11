using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegiaoAutorizadaController : ControllerBase
    {
        private IRegiaoAutorizadaService _regiaoAutorizadaService;

        public RegiaoAutorizadaController(IRegiaoAutorizadaService regiaoAutorizadaService)
        {
            _regiaoAutorizadaService = regiaoAutorizadaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RegiaoAutorizadaParameters parameters)
        {
            return _regiaoAutorizadaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codRegiao}/{codAutorizada}/{codFilial}")]
        public RegiaoAutorizada Get(int codRegiao, int codAutorizada, int codFilial)
        {
            return this._regiaoAutorizadaService.ObterPorCodigo(codRegiao, codAutorizada, codFilial);
        }

        [HttpPost]
        public void Post([FromBody] RegiaoAutorizada regiaoAutorizada)
        {
            _regiaoAutorizadaService.Criar(regiaoAutorizada);
        }

        [HttpPut("{codRegiao}/{codAutorizada}/{codFilial}")]
        public void Put(int codRegiao, int codAutorizada, int codFilial, [FromBody] RegiaoAutorizada regiaoAutorizada)
        {
            _regiaoAutorizadaService.Atualizar(regiaoAutorizada, codRegiao, codAutorizada, codFilial);
        }
    }
}
