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
    public class InstalacaoMotivoMultaController : ControllerBase
    {
        private readonly IInstalacaoMotivoMultaService _instalacaoMotivoMultaService;

        public InstalacaoMotivoMultaController(
            IInstalacaoMotivoMultaService instalacaoMotivoMultaService
        )
        {
            _instalacaoMotivoMultaService = instalacaoMotivoMultaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoMotivoMultaParameters parameters)
        {
            return _instalacaoMotivoMultaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codInstalMotivoMulta}")]
        public InstalacaoMotivoMulta Get(int codInstalMotivoMulta)
        {
            return _instalacaoMotivoMultaService.ObterPorCodigo(codInstalMotivoMulta);
        }

        [HttpPost]
        public InstalacaoMotivoMulta Post([FromBody] InstalacaoMotivoMulta instalacaoMotivoMulta)
        {
            return _instalacaoMotivoMultaService.Criar(instalacaoMotivoMulta);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoMotivoMulta instalacaoMotivoMulta)
        {
            _instalacaoMotivoMultaService.Atualizar(instalacaoMotivoMulta);
        }

        [HttpDelete("{CodInstalMotivoMulta}")]
        public void Delete(int codInstalacaoMotivoMulta)
        {
            _instalacaoMotivoMultaService.Deletar(codInstalacaoMotivoMulta);
        }
    }
}
