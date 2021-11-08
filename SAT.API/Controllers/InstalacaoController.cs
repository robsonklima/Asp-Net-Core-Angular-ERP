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
    public class InstalacaoController : ControllerBase
    {
        private readonly IInstalacaoService _instalacaoService;

        public InstalacaoController(IInstalacaoService instalacaoService)
        {
            _instalacaoService = instalacaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoParameters parameters)
        {
            return _instalacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codInstalacao}")]
        public Instalacao Get(int codInstalacao)
        {
            return _instalacaoService.ObterPorCodigo(codInstalacao);
        }

        [HttpPost]
        public void Post([FromBody] Instalacao instalacao)
        {
            _instalacaoService.Criar(instalacao);
        }

        [HttpPut]
        public void Put([FromBody] Instalacao instalacao)
        {
            _instalacaoService.Atualizar(instalacao);
        }

        [HttpDelete("{codInstalacao}")]
        public void Delete(int codInstalacao)
        {
            _instalacaoService.Deletar(codInstalacao);
        }
    }
}
