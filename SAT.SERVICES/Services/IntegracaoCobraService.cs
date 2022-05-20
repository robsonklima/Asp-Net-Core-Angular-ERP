using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoCobraService : IIntegracaoCobraService
    {
        private readonly IIntegracaoCobraRepository _integracaoCobraRepo;

        public IntegracaoCobraService(IIntegracaoCobraRepository IntegracaoCobraRepo)
        {
            _integracaoCobraRepo = IntegracaoCobraRepo;
        }

        public void Atualizar(IntegracaoCobra IntegracaoCobra)
        {
            _integracaoCobraRepo.Atualizar(IntegracaoCobra);
        }

        public IntegracaoCobra Criar(IntegracaoCobra IntegracaoCobra)
        {
            _integracaoCobraRepo.Criar(IntegracaoCobra);

            return IntegracaoCobra;
        }

        public void Deletar(int codigo)
        {
            _integracaoCobraRepo.Deletar(codigo);
        }

        public IntegracaoCobra ObterPorCodigo(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public ListViewModel ObterPorParametros(IntegracaoCobraParameters parameters)
        {
            var integracaoCobras = _integracaoCobraRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = integracaoCobras,
                TotalCount = integracaoCobras.TotalCount,
                CurrentPage = integracaoCobras.CurrentPage,
                PageSize = integracaoCobras.PageSize,
                TotalPages = integracaoCobras.TotalPages,
                HasNext = integracaoCobras.HasNext,
                HasPrevious = integracaoCobras.HasPrevious
            };

            return lista;
        }
    }
}