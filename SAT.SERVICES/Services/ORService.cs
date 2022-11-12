using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORService : IORService
    {
        private readonly IORRepository _ORRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public ORService(
            IORRepository ORRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _ORRepo = ORRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(ORParameters parameters)
        {
            var ORes = _ORRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ORes,
                TotalCount = ORes.TotalCount,
                CurrentPage = ORes.CurrentPage,
                PageSize = ORes.PageSize,
                TotalPages = ORes.TotalPages,
                HasNext = ORes.HasNext,
                HasPrevious = ORes.HasPrevious
            };

            return lista;
        }

        public OR Criar(OR or)
        {
            _ORRepo.Criar(or);

            return or;
        }

        public void Deletar(int codigo)
        {
            _ORRepo.Deletar(codigo);
        }

        public void Atualizar(OR or)
        {
            _ORRepo.Atualizar(or);
        }

        public OR ObterPorCodigo(int codigo)
        {
            return _ORRepo.ObterPorCodigo(codigo);
        }
    }
}
