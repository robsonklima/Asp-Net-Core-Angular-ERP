using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
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

        [HttpGet("{CodInstalacaoLote}")]
        public InstalacaoLote Get(int codInstalacaoLote)
        {
            return _instalLoteService.ObterPorCodigo(codInstalacaoLote);
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

        [HttpDelete("{CodInstalacaoLote}")]
        public void Delete(int codInstalacaoLote)
        {
            _instalLoteService.Deletar(codInstalacaoLote);
        }
    }
}
