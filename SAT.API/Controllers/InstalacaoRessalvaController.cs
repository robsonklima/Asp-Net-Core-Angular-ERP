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
    public class InstalacaoRessalvaController : ControllerBase
    {
        private readonly IInstalacaoRessalvaService _instalacaoLoteService;

        public InstalacaoRessalvaController(
            IInstalacaoRessalvaService instalacaoLoteService
        )
        {
            _instalacaoLoteService = instalacaoLoteService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoRessalvaParameters parameters)
        {
            return _instalacaoLoteService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalRessalva}")]
        public InstalacaoRessalva Get(int codInstalRessalva)
        {
            return _instalacaoLoteService.ObterPorCodigo(codInstalRessalva);
        }

        [HttpPost]
        public InstalacaoRessalva Post([FromBody] InstalacaoRessalva instalacaoLote)
        {
            return _instalacaoLoteService.Criar(instalacaoLote);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoRessalva instalacaoLote)
        {
            _instalacaoLoteService.Atualizar(instalacaoLote);
        }

        [HttpDelete("{CodInstalRessalva}")]
        public void Delete(int codInstalRessalva)
        {
            _instalacaoLoteService.Deletar(codInstalRessalva);
        }
    }
}
