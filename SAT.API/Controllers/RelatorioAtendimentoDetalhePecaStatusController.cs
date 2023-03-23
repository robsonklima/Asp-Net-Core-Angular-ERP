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
    public class RelatorioAtendimentoDetalhePecaStatusController : ControllerBase
    {
        public IRelatorioAtendimentoDetalhePecaStatusService _relatorioAtendimentoDetalhePecaStatusService { get; }

        public RelatorioAtendimentoDetalhePecaStatusController(IRelatorioAtendimentoDetalhePecaStatusService relatorioAtendimentoDetalhePecaStatusService)
        {
            _relatorioAtendimentoDetalhePecaStatusService = relatorioAtendimentoDetalhePecaStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RelatorioAtendimentoDetalhePecaStatusParameters parameters)
        {
            return _relatorioAtendimentoDetalhePecaStatusService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodRATDetalhesPecasStatus}")]
        public RelatorioAtendimentoDetalhePecaStatus Get(int codRATDetalhesPecasStatus)
        {
            return _relatorioAtendimentoDetalhePecaStatusService.ObterPorCodigo(codRATDetalhesPecasStatus);
        }

        [HttpPost]
        public void Post([FromBody] RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus)
        {
            _relatorioAtendimentoDetalhePecaStatusService.Criar(relatorioAtendimentoDetalhePecaStatus);
        }

        [HttpPut]
        public void Put([FromBody] RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus)
        {
            _relatorioAtendimentoDetalhePecaStatusService.Atualizar(relatorioAtendimentoDetalhePecaStatus);
        }

        [HttpDelete("{CodRATDetalhesPecasStatus}")]
        public void Delete(int codRATDetalhesPecasStatus)
        {
            _relatorioAtendimentoDetalhePecaStatusService.Deletar(codRATDetalhesPecasStatus);
        }
    }
}
