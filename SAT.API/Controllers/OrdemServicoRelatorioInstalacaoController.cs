using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class OrdemServicoRelatorioInstalacaoController : ControllerBase
    {
        private readonly IOrdemServicoRelatorioInstalacaoService _raInsService;

        public OrdemServicoRelatorioInstalacaoController(IOrdemServicoRelatorioInstalacaoService raInsService)
        {
            _raInsService = raInsService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrdemServicoRelatorioInstalacaoParameters parameters)
        {
            return _raInsService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOSRelatorioInstalacao}")]
        public OrdemServicoRelatorioInstalacao Get(int codOSRelatorioInstalacao)
        {
            return _raInsService.ObterPorCodigo(codOSRelatorioInstalacao);
        }

        [HttpPost]
        public OrdemServicoRelatorioInstalacao Post([FromBody] OrdemServicoRelatorioInstalacao relatorioInstalacao)
        {
            return _raInsService.Criar(relatorioInstalacao);
        }

        [HttpPut]
        public void Put([FromBody] OrdemServicoRelatorioInstalacao relatorioInstalacao)
        {
           // relatorioInstalacao.RelatorioAtendimentoDetalhes = null;
            _raInsService.Atualizar(relatorioInstalacao);
        }

        [HttpDelete("{codOSRelatorioInstalacao}")]
        public void Delete(int codOSRelatorioInstalacao)
        {
            _raInsService.Deletar(codOSRelatorioInstalacao);
        }
    }
}
