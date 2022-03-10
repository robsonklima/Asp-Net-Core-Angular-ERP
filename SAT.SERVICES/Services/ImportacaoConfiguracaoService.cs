using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ImportacaoConfiguracaoService : IImportacaoConfiguracaoService
    {
        private readonly IImportacaoConfiguracaoRepository _importacaoConfServicoRepo;
        
        public ImportacaoConfiguracaoService(IImportacaoConfiguracaoRepository importacaoConfServicoRepo)
        {
            _importacaoConfServicoRepo = importacaoConfServicoRepo;
        }

        public void Atualizar(ImportacaoConfiguracao importacaoConf)
        {
            _importacaoConfServicoRepo.Atualizar(importacaoConf);
        }

        public ImportacaoConfiguracao Criar(ImportacaoConfiguracao importacaoConf)
        {
            return _importacaoConfServicoRepo.Criar(importacaoConf);
        }

        public void Deletar(int codigo)
        {
            _importacaoConfServicoRepo.Deletar(codigo);
        }

        public ImportacaoConfiguracao ObterPorCodigo(int codigo)
        {
            return _importacaoConfServicoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(ImportacaoConfiguracaoParameters parameters)
        {
            var importacaoConf = _importacaoConfServicoRepo.ObterPorParametros(parameters);

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