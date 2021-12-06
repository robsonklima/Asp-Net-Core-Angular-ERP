using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoMotivoResService : IInstalacaoMotivoResService
    {
        private readonly IInstalacaoMotivoResRepository _instalacaoMotivoResRepo;

        public InstalacaoMotivoResService(IInstalacaoMotivoResRepository instalRessalvaRepo)
        {
            _instalacaoMotivoResRepo = instalRessalvaRepo;
        }

        public ListViewModel ObterPorParametros(InstalacaoMotivoResParameters parameters)
        {
            var instalacaoMotivoRes = _instalacaoMotivoResRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = instalacaoMotivoRes,
                TotalCount = instalacaoMotivoRes.TotalCount,
                CurrentPage = instalacaoMotivoRes.CurrentPage,
                PageSize = instalacaoMotivoRes.PageSize,
                TotalPages = instalacaoMotivoRes.TotalPages,
                HasNext = instalacaoMotivoRes.HasNext,
                HasPrevious = instalacaoMotivoRes.HasPrevious
            };
        }

        public InstalacaoMotivoRes Criar(InstalacaoMotivoRes instalRessalva)
        {
            _instalacaoMotivoResRepo.Criar(instalRessalva);
            return instalRessalva;
        }

        public void Deletar(int codigo)
        {
            _instalacaoMotivoResRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoMotivoRes instalRessalva)
        {
            _instalacaoMotivoResRepo.Atualizar(instalRessalva);
        }

        public InstalacaoMotivoRes ObterPorCodigo(int codigo)
        {
            return _instalacaoMotivoResRepo.ObterPorCodigo(codigo);
        }
    }
}
