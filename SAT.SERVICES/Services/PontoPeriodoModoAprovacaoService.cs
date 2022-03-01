using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoPeriodoModoAprovacaoService : IPontoPeriodoModoAprovacaoService
    {
        private readonly IPontoPeriodoModoAprovacaoRepository _pontoPeriodoModoAprovacaoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public PontoPeriodoModoAprovacaoService(IPontoPeriodoModoAprovacaoRepository pontoPeriodoModoAprovacaoRepo, ISequenciaRepository seqRepo)
        {
            _pontoPeriodoModoAprovacaoRepo = pontoPeriodoModoAprovacaoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(PontoPeriodoModoAprovacaoParameters parameters)
        {
            var data = _pontoPeriodoModoAprovacaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return lista;
        }

        public PontoPeriodoModoAprovacao Criar(PontoPeriodoModoAprovacao pontoPeriodoModoAprovacao)
        {
            pontoPeriodoModoAprovacao.CodPontoPeriodoModoAprovacao = _seqRepo.ObterContador("PontoPeriodoModoAprovacao");
            _pontoPeriodoModoAprovacaoRepo.Criar(pontoPeriodoModoAprovacao);
            return pontoPeriodoModoAprovacao;
        }

        public void Deletar(int codigo)
        {
            _pontoPeriodoModoAprovacaoRepo.Deletar(codigo);
        }

        public void Atualizar(PontoPeriodoModoAprovacao pontoPeriodoModoAprovacao)
        {
            _pontoPeriodoModoAprovacaoRepo.Atualizar(pontoPeriodoModoAprovacao);
        }

        public PontoPeriodoModoAprovacao ObterPorCodigo(int codigo)
        {
            return _pontoPeriodoModoAprovacaoRepo.ObterPorCodigo(codigo);
        }
    }
}
