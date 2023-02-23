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
    public class InstalacaoStatusController : ControllerBase
    {
        private readonly IInstalacaoStatusService _instalacaoStatusService;

        public InstalacaoStatusController(
            IInstalacaoStatusService instalacaoStatusService
        )
        {
            _instalacaoStatusService = instalacaoStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoStatusParameters parameters)
        {
            return _instalacaoStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalRessalva}")]
        public InstalacaoStatus Get(int codInstalRessalva)
        {
            return _instalacaoStatusService.ObterPorCodigo(codInstalRessalva);
        }

        [HttpPost]
        public InstalacaoStatus Post([FromBody] InstalacaoStatus instalacaoStatus)
        {
            return _instalacaoStatusService.Criar(instalacaoStatus);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoStatus instalacaoStatus)
        {
            _instalacaoStatusService.Atualizar(instalacaoStatus);
        }

        [HttpDelete("{CodInstalRessalva}")]
        public void Delete(int codInstalRessalva)
        {
            _instalacaoStatusService.Deletar(codInstalRessalva);
        }
    }
}
