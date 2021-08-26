using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoIntervencaoService : ITipoIntervencaoService
    {
        private readonly ITipoIntervencaoRepository _tiposIntervencaoRepo;

        public TipoIntervencaoService(ITipoIntervencaoRepository tiposIntervencaoRepo)
        {
            _tiposIntervencaoRepo = tiposIntervencaoRepo;
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

        public TipoIntervencao Criar(TipoIntervencao tiposIntervencao)
        {
            _tiposIntervencaoRepo.Criar(tiposIntervencao);
            return tiposIntervencao;
        }

        public void Deletar(int codigo)
        {
            _tiposIntervencaoRepo.Deletar(codigo);
        }

        public void Atualizar(TipoIntervencao tiposIntervencao)
        {
            _tiposIntervencaoRepo.Atualizar(tiposIntervencao);
        }

        public TipoIntervencao ObterPorCodigo(int codigo)
        {
            return _tiposIntervencaoRepo.ObterPorCodigo(codigo);
        }
    }
}
