using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoAnexoService : IInstalacaoAnexoService
    {
        private readonly IInstalacaoAnexoRepository _instalacaoAnexoRepo;

        public InstalacaoAnexoService(IInstalacaoAnexoRepository instalacaoAnexoRepo)
        {
            _instalacaoAnexoRepo = instalacaoAnexoRepo;
        }

        public InstalacaoAnexo Criar(InstalacaoAnexo instalacaoAnexo)
        {
            _instalacaoAnexoRepo.Criar(instalacaoAnexo);
            
            // To do Salvar Arquivo

            return instalacaoAnexo;
        }

        public void Deletar(int codigo)
        {
            _instalacaoAnexoRepo.Deletar(codigo);
        }

        public InstalacaoAnexo ObterPorCodigo(int codigo)
        {
            return _instalacaoAnexoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(InstalacaoAnexoParameters parameters)
        {
            var instalacaoAnexos = _instalacaoAnexoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = instalacaoAnexos,
                TotalCount = instalacaoAnexos.TotalCount,
                CurrentPage = instalacaoAnexos.CurrentPage,
                PageSize = instalacaoAnexos.PageSize,
                TotalPages = instalacaoAnexos.TotalPages,
                HasNext = instalacaoAnexos.HasNext,
                HasPrevious = instalacaoAnexos.HasPrevious
            };

            return lista;
        }
    }
}
