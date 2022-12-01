using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoTipoPleitoService : IInstalacaoTipoPleitoService
    {
        private readonly IInstalacaoTipoPleitoRepository _instalacaoTipoPleitoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public InstalacaoTipoPleitoService(
            IInstalacaoTipoPleitoRepository instalacaoTipoPleitoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _instalacaoTipoPleitoRepo = instalacaoTipoPleitoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public InstalacaoTipoPleito ObterPorCodigo(int codigo)
        {
            return _instalacaoTipoPleitoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(InstalacaoTipoPleitoParameters parameters)
        {
            var instalacoes = _instalacaoTipoPleitoRepo.ObterPorParametros(parameters);

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

        public InstalacaoTipoPleito Criar(InstalacaoTipoPleito instalacaoTipoPleito)
        {
            instalacaoTipoPleito.CodInstalTipoPleito = _sequenciaRepo.ObterContador("InstalacaoTipoPleito");
            _instalacaoTipoPleitoRepo.Criar(instalacaoTipoPleito);
            return instalacaoTipoPleito;
        }

        public void Deletar(int codigo)
        {
            _instalacaoTipoPleitoRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoTipoPleito instalacaoTipoPleito)
        {
            _instalacaoTipoPleitoRepo.Atualizar(instalacaoTipoPleito);
        }
    }
}
