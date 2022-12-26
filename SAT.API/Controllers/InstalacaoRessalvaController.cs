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
        private readonly IInstalacaoRessalvaService _instalacaoRessalvaService;

        public InstalacaoRessalvaController(
            IInstalacaoRessalvaService instalacaoRessalvaService
        )
        {
            _instalacaoRessalvaService = instalacaoRessalvaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoRessalvaParameters parameters)
        {
            return _instalacaoRessalvaService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalRessalva}")]
        public InstalacaoRessalva Get(int codInstalRessalva)
        {
            return _instalacaoRessalvaService.ObterPorCodigo(codInstalRessalva);
        }

        [HttpPost]
        public InstalacaoRessalva Post([FromBody] InstalacaoRessalva instalacaoRessalva)
        {
            return _instalacaoRessalvaService.Criar(instalacaoRessalva);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoRessalva instalacaoRessalva)
        {
            _instalacaoRessalvaService.Atualizar(instalacaoRessalva);
        }

        [HttpDelete("{CodInstalRessalva}")]
        public void Delete(int codInstalRessalva)
        {
            _instalacaoRessalvaService.Deletar(codInstalRessalva);
        }
    }
}
