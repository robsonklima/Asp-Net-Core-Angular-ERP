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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class IntegracaoCobraController : ControllerBase
    {
        private readonly IIntegracaoCobraService _integracaoCobraService;

        public IntegracaoCobraController(IIntegracaoCobraService integracaoCobraService)
        {
            _integracaoCobraService = integracaoCobraService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] IntegracaoCobraParameters parameters)
        {
            return _integracaoCobraService.ObterPorParametros(parameters);
        }

        [HttpGet("{codIntegracaoCobra}")]
        public IntegracaoCobra Get(int codIntegracaoCobra)
        {
            return _integracaoCobraService.ObterPorCodigo(codIntegracaoCobra);
        }

        [HttpPost]
        public IntegracaoCobra Post([FromBody] IntegracaoCobra integracaoCobra)
        {
            return _integracaoCobraService.Criar(integracaoCobra);
        }

        [HttpPut]
        public void Put([FromBody] IntegracaoCobra integracaoCobra)
        {
            _integracaoCobraService.Atualizar(integracaoCobra);
        }

        [HttpDelete("{codIntegracaoCobra}")]
        public void Delete(int codIntegracaoCobra)
        {
            _integracaoCobraService.Deletar(codIntegracaoCobra);
        }
    }
}
