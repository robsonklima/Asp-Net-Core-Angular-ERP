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
    public class InstalacaoPleitoController : ControllerBase
    {
        private readonly IInstalacaoPleitoService _instalacaoPleitoService;

        public InstalacaoPleitoController(
            IInstalacaoPleitoService instalacaoPleitoService
        )
        {
            _instalacaoPleitoService = instalacaoPleitoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoPleitoParameters parameters)
        {
            return _instalacaoPleitoService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalPleito}")]
        public InstalacaoPleito Get(int codInstalacaoPleito)
        {
            return _instalacaoPleitoService.ObterPorCodigo(codInstalacaoPleito);
        }

        [HttpPost]
        public InstalacaoPleito Post([FromBody] InstalacaoPleito instalacaoPleito)
        {
            return _instalacaoPleitoService.Criar(instalacaoPleito);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoPleito instalacaoPleito)
        {
            _instalacaoPleitoService.Atualizar(instalacaoPleito);
        }

        [HttpDelete("{CodInstalPleito}")]
        public void Delete(int codInstalacaoPleito)
        {
            _instalacaoPleitoService.Deletar(codInstalacaoPleito);
        }
    }
}
