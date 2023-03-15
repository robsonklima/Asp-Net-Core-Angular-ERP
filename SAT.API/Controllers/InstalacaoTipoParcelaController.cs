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
    public class InstalacaoTipoParcelaController : ControllerBase
    {
        private readonly IInstalacaoTipoParcelaService _instalacaoTipoParcelaService;

        public InstalacaoTipoParcelaController(
            IInstalacaoTipoParcelaService instalacaoTipoParcelaService
        )
        {
            _instalacaoTipoParcelaService = instalacaoTipoParcelaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoTipoParcelaParameters parameters)
        {
            return _instalacaoTipoParcelaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codInstalTipoParcela}")]
        public InstalacaoTipoParcela Get(int codInstalTipoParcela)
        {
            return _instalacaoTipoParcelaService.ObterPorCodigo(codInstalTipoParcela);
        }

        [HttpPost]
        public InstalacaoTipoParcela Post([FromBody] InstalacaoTipoParcela instalacaoTipoParcela)
        {
            return _instalacaoTipoParcelaService.Criar(instalacaoTipoParcela);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoTipoParcela instalacaoTipoParcela)
        {
            _instalacaoTipoParcelaService.Atualizar(instalacaoTipoParcela);
        }

        [HttpDelete("{CodInstalTipoParcela}")]
        public void Delete(int codInstalacaoTipoParcela)
        {
            _instalacaoTipoParcelaService.Deletar(codInstalacaoTipoParcela);
        }
    }
}
