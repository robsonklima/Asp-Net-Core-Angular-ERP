using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoPagtoInstalService : IInstalacaoPagtoInstalService
    {
        private readonly IInstalacaoPagtoInstalRepository _instalacaoPagtoInstalRepo;

        public InstalacaoPagtoInstalService(
            IInstalacaoPagtoInstalRepository instalacaoPagtoInstalRepo
        )
        {
            _instalacaoPagtoInstalRepo = instalacaoPagtoInstalRepo;
        }

        public InstalacaoPagtoInstal ObterPorCodigo(int codInstalacao, int codInstalPagto, int codInstalTipoParcela)
        {
            return _instalacaoPagtoInstalRepo.ObterPorCodigo(codInstalacao, codInstalPagto, codInstalTipoParcela);
        }

        public ListViewModel ObterPorParametros(InstalacaoPagtoInstalParameters parameters)
        {
            var instalacoes = _instalacaoPagtoInstalRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = instalacoes,
                TotalCount = instalacoes.TotalCount,
                CurrentPage = instalacoes.CurrentPage,
                PageSize = instalacoes.PageSize,
                TotalPages = instalacoes.TotalPages,
                HasNext = instalacoes.HasNext,
                HasPrevious = instalacoes.HasPrevious
            };
        }

        public InstalacaoPagtoInstal Criar(InstalacaoPagtoInstal instalacaoPagtoInstal)
        {
            _instalacaoPagtoInstalRepo.Criar(instalacaoPagtoInstal);
            return instalacaoPagtoInstal;
        }

        public void Deletar(int codInstalacao, int codInstalPagto, int codInstalTipoParcela)
        {
            _instalacaoPagtoInstalRepo.Deletar(codInstalacao, codInstalPagto, codInstalTipoParcela);
        }

        public void Atualizar(InstalacaoPagtoInstal instalacaoPagtoInstal)
        {
            _instalacaoPagtoInstalRepo.Atualizar(instalacaoPagtoInstal);
        }
    }
}
