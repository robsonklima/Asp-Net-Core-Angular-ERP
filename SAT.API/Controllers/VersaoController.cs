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
    public class VersaoController : ControllerBase
    {
        private readonly IVersaoService _versaoService;

        public VersaoController(IVersaoService versaoService)
        {
            _versaoService = versaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] VersaoParameters parameters)
        {
            return _versaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codSatVersao}")]
        public Versao Get(int codSatVersao)
        {
            return _versaoService.ObterPorCodigo(codSatVersao);
        }

        [HttpPost]
        public void Post([FromBody] Versao Versao)
        {
            _versaoService.Criar(Versao);
        }

        [HttpPut]
        public void Put([FromBody] Versao Versao)
        {
            _versaoService.Atualizar(Versao);
        }

        [HttpDelete("{codSatVersao}")]
        public void Delete(int codSatVersao)
        {
            _versaoService.Deletar(codSatVersao);
        }
    }
}
