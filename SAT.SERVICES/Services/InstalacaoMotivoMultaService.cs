using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoMotivoMultaService : IInstalacaoMotivoMultaService
    {
        private readonly IInstalacaoMotivoMultaRepository _instalacaoMotivoMultaRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public InstalacaoMotivoMultaService(
            IInstalacaoMotivoMultaRepository instalacaoMotivoMultaRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _instalacaoMotivoMultaRepo = instalacaoMotivoMultaRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public InstalacaoMotivoMulta ObterPorCodigo(int codigo)
        {
            return _instalacaoMotivoMultaRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(InstalacaoMotivoMultaParameters parameters)
        {
            var instalacoes = _instalacaoMotivoMultaRepo.ObterPorParametros(parameters);

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

        public InstalacaoMotivoMulta Criar(InstalacaoMotivoMulta instalacaoMotivoMulta)
        {
            instalacaoMotivoMulta.CodInstalMotivoMulta = _sequenciaRepo.ObterContador("InstalMotivoMulta");
            _instalacaoMotivoMultaRepo.Criar(instalacaoMotivoMulta);
            return instalacaoMotivoMulta;
        }

        public void Deletar(int codigo)
        {
            _instalacaoMotivoMultaRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoMotivoMulta instalacaoMotivoMulta)
        {
            _instalacaoMotivoMultaRepo.Atualizar(instalacaoMotivoMulta);
        }
    }
}
