using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MRPLogixEstoqueService : IMRPLogixEstoqueService
    {
        private readonly IMRPLogixEstoqueRepository _mrpLogixEstoqueRepo;

        public MRPLogixEstoqueService(IMRPLogixEstoqueRepository mrpLogixEstoqueRepo)
        {
            _mrpLogixEstoqueRepo = mrpLogixEstoqueRepo;
        }

        public ListViewModel ObterPorParametros(MRPLogixEstoqueParameters parameters)
        {
            var mrps = _mrpLogixEstoqueRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = mrps,
                TotalCount = mrps.TotalCount,
                CurrentPage = mrps.CurrentPage,
                PageSize = mrps.PageSize,
                TotalPages = mrps.TotalPages,
                HasNext = mrps.HasNext,
                HasPrevious = mrps.HasPrevious
            };
        }

        public MRPLogixEstoque Criar(MRPLogixEstoque mrpLogixEstoque)
        {
            _mrpLogixEstoqueRepo.Criar(mrpLogixEstoque);
            return mrpLogixEstoque;
        }

        public void Deletar(int codigo)
        {
            _mrpLogixEstoqueRepo.Deletar(codigo);
        }

        public void Atualizar(MRPLogixEstoque mrpLogixEstoque)
        {
            _mrpLogixEstoqueRepo.Atualizar(mrpLogixEstoque);
        }

        public MRPLogixEstoque ObterPorCodigo(int codigo)
        {
            return _mrpLogixEstoqueRepo.ObterPorCodigo(codigo);
        }
        public void LimparTabela()
        {
            _mrpLogixEstoqueRepo.LimparTabela();
        }
    }
}
