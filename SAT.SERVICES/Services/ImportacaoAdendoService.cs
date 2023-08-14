using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ImportacaoAdendoService : IImportacaoAdendoService
    {
        private readonly IImportacaoAdendoRepository _ImportacaoAdendoRepo;

        public ImportacaoAdendoService(IImportacaoAdendoRepository ImportacaoAdendoRepo)
        {
            _ImportacaoAdendoRepo = ImportacaoAdendoRepo;
        }

        public ImportacaoAdendo Atualizar(ImportacaoAdendo ImportacaoAdendo)
        {
            return _ImportacaoAdendoRepo.Atualizar(ImportacaoAdendo);
        }

        public ImportacaoAdendo Criar(ImportacaoAdendo ImportacaoAdendo)
        {
            _ImportacaoAdendoRepo.Criar(ImportacaoAdendo);

            return ImportacaoAdendo;
        }

        public ImportacaoAdendo Deletar(int codigo)
        {
            return _ImportacaoAdendoRepo.Deletar(codigo);
        }

        public ImportacaoAdendo ObterPorCodigo(int codigo)
        {
            return _ImportacaoAdendoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(ImportacaoAdendoParameters parameters)
        {
            var data = _ImportacaoAdendoRepo.ObterPorParametros(parameters);

            var model = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return model;
        }
    }
}
