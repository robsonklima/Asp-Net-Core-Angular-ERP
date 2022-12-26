using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LaudoSituacaoService : ILaudoSituacaoService
    {
        private readonly ILaudoSituacaoRepository _laudoSituacaoRepo;

        public LaudoSituacaoService(ILaudoSituacaoRepository laudoSituacaoRepo)
        {
            _laudoSituacaoRepo = laudoSituacaoRepo;
        }

        public void Atualizar(LaudoSituacao laudoSituacao)
        {
            _laudoSituacaoRepo.Atualizar(laudoSituacao);
        }
        public LaudoSituacao Criar(LaudoSituacao laudoSituacao)
        {
            _laudoSituacaoRepo.Criar(laudoSituacao);

            return laudoSituacao;
        }
        public LaudoSituacao ObterPorCodigo(int codigo) =>
            this._laudoSituacaoRepo.ObterPorCodigo(codigo);

        public ListViewModel ObterPorParametros(LaudoSituacaoParameters parameters)
        {
            var laudos = _laudoSituacaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = laudos,
                TotalCount = laudos.TotalCount,
                CurrentPage = laudos.CurrentPage,
                PageSize = laudos.PageSize,
                TotalPages = laudos.TotalPages,
                HasNext = laudos.HasNext,
                HasPrevious = laudos.HasPrevious
            };

            return lista;
        }
    }
}