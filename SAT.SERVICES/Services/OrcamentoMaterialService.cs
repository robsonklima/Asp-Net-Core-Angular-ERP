using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoMaterialService : IOrcamentoMaterialService
    {
        private readonly IOrcamentoMaterialRepository _orcamentoMatRepo;

        public OrcamentoMaterialService(IOrcamentoMaterialRepository orcamentoMatRepo)
        {
            _orcamentoMatRepo = orcamentoMatRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentoMaterialParameters parameters)
        {
            var orcamentos = _orcamentoMatRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = orcamentos,
                TotalCount = orcamentos.TotalCount,
                CurrentPage = orcamentos.CurrentPage,
                PageSize = orcamentos.PageSize,
                TotalPages = orcamentos.TotalPages,
                HasNext = orcamentos.HasNext,
                HasPrevious = orcamentos.HasPrevious
            };

            return lista;
        }

        public OrcamentoMaterial Criar(OrcamentoMaterial orcamentoMaterial)
        {
            _orcamentoMatRepo.Criar(orcamentoMaterial);
            return orcamentoMaterial;
        }

        public void Deletar(int codigo)
        {
            _orcamentoMatRepo.Deletar(codigo);
        }

        public OrcamentoMaterial Atualizar(OrcamentoMaterial orcamentoMaterial)
        {
            _orcamentoMatRepo.Atualizar(orcamentoMaterial);
            return orcamentoMaterial;
        }

        public OrcamentoMaterial ObterPorCodigo(int codigo)
        {
            return _orcamentoMatRepo.ObterPorCodigo(codigo);
        }
    }
}
