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
    public class InstalacaoTipoPleitoController : ControllerBase
    {
        private readonly IInstalacaoTipoPleitoService _instalacaoTipoPleitoService;

        public InstalacaoTipoPleitoController(
            IInstalacaoTipoPleitoService instalacaoTipoPleitoService
        )
        {
            _instalacaoTipoPleitoService = instalacaoTipoPleitoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoTipoPleitoParameters parameters)
        {
            return _instalacaoTipoPleitoService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalTipoPleito}")]
        public InstalacaoTipoPleito Get(int codInstalTipoPleito)
        {
            return _instalacaoTipoPleitoService.ObterPorCodigo(codInstalTipoPleito);
        }

        [HttpPost]
        public InstalacaoTipoPleito Post([FromBody] InstalacaoTipoPleito instalacaoTipoPleito)
        {
            return _instalacaoTipoPleitoService.Criar(instalacaoTipoPleito);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoTipoPleito instalacaoTipoPleito)
        {
            _instalacaoTipoPleitoService.Atualizar(instalacaoTipoPleito);
        }

        [HttpDelete("{CodInstalTipoPleito}")]
        public void Delete(int codInstalTipoPleito)
        {
            _instalacaoTipoPleitoService.Deletar(codInstalTipoPleito);
        }
    }
}
