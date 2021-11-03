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
    public class OrdemServicoRelatorioInstalacaoNaoConformidadeController : ControllerBase
    {
        private readonly IOrdemServicoRelatorioInstalacaoNaoConformidadeService _raInsService;

        public OrdemServicoRelatorioInstalacaoNaoConformidadeController(IOrdemServicoRelatorioInstalacaoNaoConformidadeService raInsService)
        {
            _raInsService = raInsService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrdemServicoRelatorioInstalacaoNaoConformidadeParameters parameters)
        {
            return _raInsService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOSRelatorioInstalacaoNaoConformidade}")]
        public OrdemServicoRelatorioInstalacaoNaoConformidade Get(int codOSRelatorioInstalacaoNaoConformidade)
        {
            return _raInsService.ObterPorCodigo(codOSRelatorioInstalacaoNaoConformidade);
        }

        [HttpPost]
        public OrdemServicoRelatorioInstalacaoNaoConformidade Post([FromBody] OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacao)
        {
            return _raInsService.Criar(relatorioInstalacao);
        }

        [HttpPut]
        public void Put([FromBody] OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacao)
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
