using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoLoteService : IInstalacaoLoteService
    {
        private readonly IInstalacaoLoteRepository _instalLoteRepo;

        public InstalacaoLoteService(IInstalacaoLoteRepository instalLoteRepo)
        {
            _instalLoteRepo = instalLoteRepo;
        }

        public ListViewModel ObterPorParametros(InstalacaoLoteParameters parameters)
        {
            var instalLotes = _instalLoteRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = instalLotes,
                TotalCount = instalLotes.TotalCount,
                CurrentPage = instalLotes.CurrentPage,
                PageSize = instalLotes.PageSize,
                TotalPages = instalLotes.TotalPages,
                HasNext = instalLotes.HasNext,
                HasPrevious = instalLotes.HasPrevious
            };
        }

        public InstalacaoLote Criar(InstalacaoLote instalLote)
        {
            _instalLoteRepo.Criar(instalLote);
            return instalLote;
        }

        public void Deletar(int codigo)
        {
            _instalLoteRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoLote instalLote)
        {
            _instalLoteRepo.Atualizar(instalLote);
        }

        public InstalacaoLote ObterPorCodigo(int codigo)
        {
            return _instalLoteRepo.ObterPorCodigo(codigo);
        }
    }
}
