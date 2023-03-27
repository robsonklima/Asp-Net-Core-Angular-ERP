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
    public class RelatorioAtendimentoPecaStatusController : ControllerBase
    {
        public IRelatorioAtendimentoPecaStatusService _relatorioAtendimentoPecaStatusService { get; }

        public RelatorioAtendimentoPecaStatusController(IRelatorioAtendimentoPecaStatusService relatorioAtendimentoPecaStatusService)
        {
            _relatorioAtendimentoPecaStatusService = relatorioAtendimentoPecaStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RelatorioAtendimentoPecaStatusParameters parameters)
        {
            return _relatorioAtendimentoPecaStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodRATPecasStatus}")]
        public RelatorioAtendimentoPecaStatus Get(int codRatpecasStatus)
        {
            return _relatorioAtendimentoPecaStatusService.ObterPorCodigo(codRatpecasStatus);
        }

        [HttpPost]
        public void Post([FromBody] RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus)
        {
            _relatorioAtendimentoPecaStatusService.Criar(relatorioAtendimentoPecaStatus);
        }

        [HttpPut]
        public void Put([FromBody] RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus)
        {
            _relatorioAtendimentoPecaStatusService.Atualizar(relatorioAtendimentoPecaStatus);
        }

        [HttpDelete("{CodRATPecasStatus}")]
        public void Delete(int codRatpecasStatus)
        {
            _relatorioAtendimentoPecaStatusService.Deletar(codRatpecasStatus);
        }
    }
}
