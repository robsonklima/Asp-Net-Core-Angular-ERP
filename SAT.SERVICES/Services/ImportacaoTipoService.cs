using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ImportacaoTipoService : IImportacaoTipoService
    {
        private readonly IImportacaoTipoRepository _importacaoTipoRepo;
        
        public ImportacaoTipoService(IImportacaoTipoRepository importacaoTipoRepo)
        {
            _importacaoTipoRepo = importacaoTipoRepo;
        }

        public void Atualizar(ImportacaoTipo importacaoConf)
        {
            _importacaoTipoRepo.Atualizar(importacaoConf);
        }

        public ImportacaoTipo Criar(ImportacaoTipo importacaoConf)
        {
            return _importacaoTipoRepo.Criar(importacaoConf);
        }

        public void Deletar(int codigo)
        {
            _importacaoTipoRepo.Deletar(codigo);
        }

        public ImportacaoTipo ObterPorCodigo(int codigo)
        {
            return _importacaoTipoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(ImportacaoTipoParameters parameters)
        {
            var importacaoConf = _importacaoTipoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = importacaoConf,
                TotalCount = importacaoConf.TotalCount,
                CurrentPage = importacaoConf.CurrentPage,
                PageSize = importacaoConf.PageSize,
                TotalPages = importacaoConf.TotalPages,
                HasNext = importacaoConf.HasNext,
                HasPrevious = importacaoConf.HasPrevious
            };

            return lista;
        }
    }
}