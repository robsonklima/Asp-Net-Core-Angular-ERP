using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioAtendimentoDetalhePecaController : ControllerBase
    {
        private readonly IRelatorioAtendimentoDetalhePecaRepository _relatorioAtendimentoDetalhePecaInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public RelatorioAtendimentoDetalhePecaController(
            IRelatorioAtendimentoDetalhePecaRepository relatorioAtendimentoDetalhePecaInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _relatorioAtendimentoDetalhePecaInterface = relatorioAtendimentoDetalhePecaInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpPost]
        public RelatorioAtendimentoDetalhePeca Post([FromBody] RelatorioAtendimentoDetalhePeca detalhePeca)
        {
            detalhePeca.CodRATDetalhePeca = _sequenciaInterface
                .ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO_DETALHE_PECA);

            _relatorioAtendimentoDetalhePecaInterface.Criar(detalhePeca);
            return detalhePeca;
        }

        [HttpDelete("{codRATDetalhePeca}")]
        public void Delete(int codRATDetalhePeca)
        {
            _relatorioAtendimentoDetalhePecaInterface.Deletar(codRATDetalhePeca);
        }
    }
}
