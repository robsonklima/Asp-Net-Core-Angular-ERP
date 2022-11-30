using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoPleitoService : IInstalacaoPleitoService
    {
        private readonly IInstalacaoPleitoRepository _instalacaoPleitoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public InstalacaoPleitoService(
            IInstalacaoPleitoRepository instalacaoPleitoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _instalacaoPleitoRepo = instalacaoPleitoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public InstalacaoPleito ObterPorCodigo(int codigo)
        {
            return _instalacaoPleitoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(InstalacaoPleitoParameters parameters)
        {
            var instalacoes = _instalacaoPleitoRepo.ObterPorParametros(parameters);

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

        public InstalacaoPleito Criar(InstalacaoPleito instalacaoPleito)
        {
            instalacaoPleito.CodInstalPleito = _sequenciaRepo.ObterContador("InstalacaoPleito");
            _instalacaoPleitoRepo.Criar(instalacaoPleito);
            return instalacaoPleito;
        }

        public void Deletar(int codigo)
        {
            _instalacaoPleitoRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoPleito instalacaoPleito)
        {
            _instalacaoPleitoRepo.Atualizar(instalacaoPleito);
        }
    }
}
