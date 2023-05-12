using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OperadoraTelefoniaService : IOperadoraTelefoniaService
    {
        private readonly IOperadoraTelefoniaRepository _motivoRepo;

        public OperadoraTelefoniaService(IOperadoraTelefoniaRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        public ListViewModel ObterPorParametros(OperadoraTelefoniaParameters parameters)
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

        public OperadoraTelefonia Criar(OperadoraTelefonia op)
        {
            _motivoRepo.Criar(op);

            return op;
        }

        public OperadoraTelefonia Deletar(int codigo)
        {
            return _motivoRepo.Deletar(codigo);
        }

        public OperadoraTelefonia Atualizar(OperadoraTelefonia op)
        {
            return _motivoRepo.Atualizar(op);
        }

        public OperadoraTelefonia ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
