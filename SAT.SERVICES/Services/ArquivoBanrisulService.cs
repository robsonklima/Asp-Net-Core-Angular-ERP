using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ArquivoBanrisulService : IArquivoBanrisulService
    {
        private readonly IArquivoBanrisulRepository _arquivoBanrisulRepo;

        public ArquivoBanrisulService(
            IArquivoBanrisulRepository arquivoBanrisulRepo
        )
        {
            _arquivoBanrisulRepo = arquivoBanrisulRepo;
        }

        public void Atualizar(ArquivoBanrisul arquivo)
        {
            _arquivoBanrisulRepo.Atualizar(arquivo);
        }

        public void Criar(ArquivoBanrisul arquivo)
        {
            _arquivoBanrisulRepo.Criar(arquivo);
        }

        public void Deletar(int codigo)
        {
            _arquivoBanrisulRepo.Deletar(codigo);
        }

        public ArquivoBanrisul ObterPorCodigo(int codigo)
        {
            return _arquivoBanrisulRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(ArquivoBanrisulParameters parameters)
        {
            var arquivos = _arquivoBanrisulRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = arquivos,
                TotalCount = arquivos.TotalCount,
                CurrentPage = arquivos.CurrentPage,
                PageSize = arquivos.PageSize,
                TotalPages = arquivos.TotalPages,
                HasNext = arquivos.HasNext,
                HasPrevious = arquivos.HasPrevious
            };

            return lista;
        }
    }
}
