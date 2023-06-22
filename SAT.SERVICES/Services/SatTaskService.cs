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

        public SatTaskService(ISatTaskRepository SatTaskRepo, ISequenciaRepository seqRepo)
        {
            _SatTaskRepo = SatTaskRepo;
        }

        public ListViewModel ObterPorParametros(SatTaskParameters parameters)
        {
            var perfis = _SatTaskRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = perfis,
                TotalCount = perfis.TotalCount,
                CurrentPage = perfis.CurrentPage,
                PageSize = perfis.PageSize,
                TotalPages = perfis.TotalPages,
                HasNext = perfis.HasNext,
                HasPrevious = perfis.HasPrevious
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
