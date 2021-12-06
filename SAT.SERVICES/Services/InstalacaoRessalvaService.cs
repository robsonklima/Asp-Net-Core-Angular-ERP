using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoRessalvaService : IInstalacaoRessalvaService
    {
        private readonly IInstalacaoRessalvaRepository _instalRessalvaRepo;

        public InstalacaoRessalvaService(IInstalacaoRessalvaRepository instalRessalvaRepo)
        {
            _instalRessalvaRepo = instalRessalvaRepo;
        }

        public ListViewModel ObterPorParametros(InstalacaoRessalvaParameters parameters)
        {
            var instalRessalvas = _instalRessalvaRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = instalRessalvas,
                TotalCount = instalRessalvas.TotalCount,
                CurrentPage = instalRessalvas.CurrentPage,
                PageSize = instalRessalvas.PageSize,
                TotalPages = instalRessalvas.TotalPages,
                HasNext = instalRessalvas.HasNext,
                HasPrevious = instalRessalvas.HasPrevious
            };
        }

        public InstalacaoRessalva Criar(InstalacaoRessalva instalRessalva)
        {
            _instalRessalvaRepo.Criar(instalRessalva);
            return instalRessalva;
        }

        public void Deletar(int codigo)
        {
            _instalRessalvaRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoRessalva instalRessalva)
        {
            _instalRessalvaRepo.Atualizar(instalRessalva);
        }

        public InstalacaoRessalva ObterPorCodigo(int codigo)
        {
            return _instalRessalvaRepo.ObterPorCodigo(codigo);
        }
    }
}
