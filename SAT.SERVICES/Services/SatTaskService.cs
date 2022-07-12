using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class SatTaskService : ISatTaskService
    {
        private readonly ISatTaskRepository _SatTaskRepo;
        private readonly ISequenciaRepository _seqRepo;

        public SatTaskService(ISatTaskRepository SatTaskRepo, ISequenciaRepository seqRepo)
        {
            _SatTaskRepo = SatTaskRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(SatTaskParameters parameters)
        {
            var SatTasks = _SatTaskRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = SatTasks,
                TotalCount = SatTasks.TotalCount,
                CurrentPage = SatTasks.CurrentPage,
                PageSize = SatTasks.PageSize,
                TotalPages = SatTasks.TotalPages,
                HasNext = SatTasks.HasNext,
                HasPrevious = SatTasks.HasPrevious
            };

            return lista;
        }

        public SatTask Criar(SatTask SatTask)
        {
            _SatTaskRepo.Criar(SatTask);
            return SatTask;
        }

        public void Deletar(int codigo)
        {
            _SatTaskRepo.Deletar(codigo);
        }

        public void Atualizar(SatTask SatTask)
        {
            _SatTaskRepo.Atualizar(SatTask);
        }

        public SatTask ObterPorCodigo(int codigo)
        {
            return _SatTaskRepo.ObterPorCodigo(codigo);
        }
    }
}
