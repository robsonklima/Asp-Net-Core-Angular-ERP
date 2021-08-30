using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoIntervencaoService : ITipoIntervencaoService
    {
        private readonly ITipoIntervencaoRepository _tiposIntervencaoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public TipoIntervencaoService(ITipoIntervencaoRepository tiposIntervencaoRepo, ISequenciaRepository seqRepo)
        {
            _tiposIntervencaoRepo = tiposIntervencaoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(TipoIntervencaoParameters parameters)
        {
            var TiposIntervencao = _tiposIntervencaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = TiposIntervencao,
                TotalCount = TiposIntervencao.TotalCount,
                CurrentPage = TiposIntervencao.CurrentPage,
                PageSize = TiposIntervencao.PageSize,
                TotalPages = TiposIntervencao.TotalPages,
                HasNext = TiposIntervencao.HasNext,
                HasPrevious = TiposIntervencao.HasPrevious
            };

            return lista;
        }

        public TipoIntervencao Criar(TipoIntervencao tipoIntervencao)
        {
            tipoIntervencao.CodTipoIntervencao = _seqRepo.ObterContador("TipoIntervencao");
            _tiposIntervencaoRepo.Criar(tipoIntervencao);
            return tipoIntervencao;
        }

        public void Deletar(int codigo)
        {
            _tiposIntervencaoRepo.Deletar(codigo);
        }

        public void Atualizar(TipoIntervencao tipoIntervencao)
        {
            _tiposIntervencaoRepo.Atualizar(tipoIntervencao);
        }

        public TipoIntervencao ObterPorCodigo(int codigo)
        {
            return _tiposIntervencaoRepo.ObterPorCodigo(codigo);
        }
    }
}
