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
    public class InstalacaoLoteController : ControllerBase
    {
        private readonly IInstalacaoLoteService _instalLoteService;

        public InstalacaoLoteController(
            IInstalacaoLoteService instalLoteService
        )
        {
            _instalLoteService = instalLoteService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoLoteParameters parameters)
        {
            return _instalLoteService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalLote}")]
        public InstalacaoLote Get(int codInstalLote)
        {
            return _instalLoteService.ObterPorCodigo(codInstalLote);
        }

        [HttpPost]
        public InstalacaoLote Post([FromBody] InstalacaoLote instalLote)
        {
            return _instalLoteService.Criar(instalLote);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoLote instalLote)
        {
            _instalLoteService.Atualizar(instalLote);
        }

        [HttpDelete("{CodInstalLote}")]
        public void Delete(int codInstalLote)
        {
            _instalLoteService.Deletar(codInstalLote);
        }
    }
}
