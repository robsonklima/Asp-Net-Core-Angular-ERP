using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoComunicacaoService : ITipoComunicacaoService
    {
        private readonly ITipoComunicacaoRepository _TipoComunicacaoRepo;

        public TipoComunicacaoService(ITipoComunicacaoRepository TipoComunicacaoRepo)
        {
            _TipoComunicacaoRepo = TipoComunicacaoRepo;
        }

        public ListViewModel ObterPorParametros(TipoComunicacaoParameters parameters)
        {
            var TipoComunicacao = _TipoComunicacaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = TipoComunicacao,
                TotalCount = TipoComunicacao.TotalCount,
                CurrentPage = TipoComunicacao.CurrentPage,
                PageSize = TipoComunicacao.PageSize,
                TotalPages = TipoComunicacao.TotalPages,
                HasNext = TipoComunicacao.HasNext,
                HasPrevious = TipoComunicacao.HasPrevious
            };

            return lista;
        }

        public TipoComunicacao Criar(TipoComunicacao TipoComunicacao)
        {
            _TipoComunicacaoRepo.Criar(TipoComunicacao);
            return TipoComunicacao;
        }

        public TipoComunicacao Deletar(int codigo)
        {
            return _TipoComunicacaoRepo.Deletar(codigo);
        }

        public TipoComunicacao Atualizar(TipoComunicacao TipoComunicacao)
        {
            return _TipoComunicacaoRepo.Atualizar(TipoComunicacao);
        }

        public TipoComunicacao ObterPorCodigo(int codigo)
        {
            return _TipoComunicacaoRepo.ObterPorCodigo(codigo);
        }
    }
}