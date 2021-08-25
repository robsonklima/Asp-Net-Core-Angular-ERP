using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using System.Linq;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioAtendimentoController : ControllerBase
    {
        private readonly IRelatorioAtendimentoRepository _raInterface;
        private readonly ISequenciaRepository _sequenciaInterface;
        private readonly IRelatorioAtendimentoDetalheRepository _raDetalheInterface;
        private readonly IRelatorioAtendimentoDetalhePecaRepository _raDetalhePecaInterface;

        public RelatorioAtendimentoController(
            IRelatorioAtendimentoRepository raInterface,
            ISequenciaRepository sequenciaInterface,
            IRelatorioAtendimentoDetalheRepository raDetalheInterface,
            IRelatorioAtendimentoDetalhePecaRepository raDetalhePecaInterface
        )
        {
            _raInterface = raInterface;
            _sequenciaInterface = sequenciaInterface;
            _raDetalheInterface = raDetalheInterface;
            _raDetalhePecaInterface = raDetalhePecaInterface;

        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RelatorioAtendimentoParameters parameters)
        {
            var relatoriosAtendimento = _raInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = relatoriosAtendimento,
                TotalCount = relatoriosAtendimento.TotalCount,
                CurrentPage = relatoriosAtendimento.CurrentPage,
                PageSize = relatoriosAtendimento.PageSize,
                TotalPages = relatoriosAtendimento.TotalPages,
                HasNext = relatoriosAtendimento.HasNext,
                HasPrevious = relatoriosAtendimento.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codRAT}")]
        public RelatorioAtendimento Get(int codRAT)
        {
            return _raInterface.ObterPorCodigo(codRAT);
        }

        [HttpPost]
        public RelatorioAtendimento Post([FromBody] RelatorioAtendimento relatorioAtendimento)
        {
            relatorioAtendimento.CodRAT = _sequenciaInterface.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO);
            relatorioAtendimento.RelatorioAtendimentoDetalhes = null;
            _raInterface.Criar(relatorioAtendimento);
            return relatorioAtendimento;
        }

        [HttpPut]
        public void Put([FromBody] RelatorioAtendimento relatorioAtendimento)
        {
            relatorioAtendimento.RelatorioAtendimentoDetalhes = null;
            _raInterface.Atualizar(relatorioAtendimento);
        }

        [HttpDelete("{codRAT}")]
        public void Delete(int codRAT)
        {
            _raInterface.Deletar(codRAT);
        }
    }
}
