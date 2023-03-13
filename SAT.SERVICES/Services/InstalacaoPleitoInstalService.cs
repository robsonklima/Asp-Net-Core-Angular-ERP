using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoPleitoInstalService : IInstalacaoPleitoInstalService
    {
        private readonly IInstalacaoPleitoInstalRepository _instalacaoPleitoInstalRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public InstalacaoPleitoInstalService(
            IInstalacaoPleitoInstalRepository instalacaoPleitoInstalRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _instalacaoPleitoInstalRepo = instalacaoPleitoInstalRepo;
            _sequenciaRepo = sequenciaRepo;
        }
        public InstalacaoPleitoInstal ObterPorCodigo(int codInstalacao, int codInstalPleito)
        {
            return _instalacaoPleitoInstalRepo.ObterPorCodigo(codInstalacao, codInstalPleito);
        }

        public ListViewModel ObterPorParametros(InstalacaoPleitoInstalParameters parameters)
        {
            var instalacoes = _instalacaoPleitoInstalRepo.ObterPorParametros(parameters);

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

        public InstalacaoPleitoInstal Criar(InstalacaoPleitoInstal instalacaoPleitoInstal)
        {
            _instalacaoPleitoInstalRepo.Criar(instalacaoPleitoInstal);
            return instalacaoPleitoInstal;
        }

        public void Deletar(int codInstalacao,int codInstalPleito)
        {
            _instalacaoPleitoInstalRepo.Deletar(codInstalacao, codInstalPleito);
        }

        public void Atualizar(InstalacaoPleitoInstal instalacaoPleitoInstal)
        {
            _instalacaoPleitoInstalRepo.Atualizar(instalacaoPleitoInstal);
        }
    }
}
