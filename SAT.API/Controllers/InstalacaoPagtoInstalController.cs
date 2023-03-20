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
    public class InstalacaoPagtoInstalController : ControllerBase
    {
        private readonly IInstalacaoPagtoInstalService _instalacaoPagtoInstalService;

        public InstalacaoPagtoInstalController(
            IInstalacaoPagtoInstalService instalacaoPagtoInstalService
        )
        {
            _instalacaoPagtoInstalService = instalacaoPagtoInstalService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoPagtoInstalParameters parameters)
        {
            return _instalacaoPagtoInstalService.ObterPorParametros(parameters);
        }

        [HttpGet("{codInstalacao}/{codInstalPagto}/{codInstalTipoParcela}")]
        public InstalacaoPagtoInstal Get(int codInstalacao, int codInstalPagto, int codInstalTipoParcela)
        {
            return _instalacaoPagtoInstalService.ObterPorCodigo(codInstalacao, codInstalPagto, codInstalTipoParcela);
        }

        [HttpPost]
        public InstalacaoPagtoInstal Post([FromBody] InstalacaoPagtoInstal instalacaoPagtoInstal)
        {
            return _instalacaoPagtoInstalService.Criar(instalacaoPagtoInstal);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoPagtoInstal instalacaoPagtoInstal)
        {
            _instalacaoPagtoInstalService.Atualizar(instalacaoPagtoInstal);
        }

        [HttpDelete("{codInstalacao}/{codInstalPagto}/{codInstalTipoParcela}")]
        public void Delete(int codInstalacao, int codInstalPagto, int codInstalTipoParcela)
        {
            _instalacaoPagtoInstalService.Deletar(codInstalacao, codInstalPagto, codInstalTipoParcela);
        }
    }
}
