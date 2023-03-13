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
    public class InstalacaoPleitoInstalController : ControllerBase
    {
        private readonly IInstalacaoPleitoInstalService _instalacaoPleitoInstalService;

        public InstalacaoPleitoInstalController(
            IInstalacaoPleitoInstalService instalacaoPleitoInstalService
        )
        {
            _instalacaoPleitoInstalService = instalacaoPleitoInstalService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoPleitoInstalParameters parameters)
        {
            return _instalacaoPleitoInstalService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalacao}/{CodInstalPleito}")]
        public InstalacaoPleitoInstal Get(int codInstalacao, int codInstalPleito)
        {
            return _instalacaoPleitoInstalService.ObterPorCodigo(codInstalacao, codInstalPleito);
        }

        [HttpPost]
        public InstalacaoPleitoInstal Post([FromBody] InstalacaoPleitoInstal instalacaoPleitoInstal)
        {
            return _instalacaoPleitoInstalService.Criar(instalacaoPleitoInstal);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoPleitoInstal instalacaoPleitoInstal)
        {
            _instalacaoPleitoInstalService.Atualizar(instalacaoPleitoInstal);
        }

        [HttpDelete("{CodInstalacao}/{CodInstalPleito}")]
        public void Delete(int codInstalacao, int codInstalPleito)
        {
            _instalacaoPleitoInstalService.Deletar(codInstalacao, codInstalPleito);
        }
    }
}
