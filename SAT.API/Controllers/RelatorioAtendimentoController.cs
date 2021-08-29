using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class RelatorioAtendimentoController : ControllerBase
    {
        private readonly IRelatorioAtendimentoService _raService;

        public RelatorioAtendimentoController(IRelatorioAtendimentoService raService)
        {
            _raService = raService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RelatorioAtendimentoParameters parameters)
        {
            return _raService.ObterPorParametros(parameters);
        }

        [HttpGet("{codRAT}")]
        public RelatorioAtendimento Get(int codRAT)
        {
            return _raService.ObterPorCodigo(codRAT);
        }

        [HttpPost]
        public RelatorioAtendimento Post([FromBody] RelatorioAtendimento relatorioAtendimento)
        {
            return _raService.Criar(relatorioAtendimento);
        }

        [HttpPut]
        public void Put([FromBody] RelatorioAtendimento relatorioAtendimento)
        {
            relatorioAtendimento.RelatorioAtendimentoDetalhes = null;
            _raService.Atualizar(relatorioAtendimento);
        }

        [HttpDelete("{codRAT}")]
        public void Delete(int codRAT)
        {
            _raService.Deletar(codRAT);
        }
    }
}
