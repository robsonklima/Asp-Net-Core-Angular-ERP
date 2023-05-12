using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MotivoCancelamentoService : IMotivoCancelamentoService
    {
        private readonly IMotivoCancelamentoRepository _motivoRepo;

        public MotivoCancelamentoService(IMotivoCancelamentoRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        public ListViewModel ObterPorParametros(MotivoCancelamentoParameters parameters)
        {
            var regioes = _motivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return lista;
        }

        public MotivoCancelamento Criar(MotivoCancelamento op)
        {
            _motivoRepo.Criar(op);

            return op;
        }

        public MotivoCancelamento Deletar(int codigo)
        {
            return _motivoRepo.Deletar(codigo);
        }

        public MotivoCancelamento Atualizar(MotivoCancelamento op)
        {
            return _motivoRepo.Atualizar(op);
        }

        public MotivoCancelamento ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
