using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class SatTaskTipoService : ISatTaskTipoService
    {
        private readonly ISatTaskTipoRepository _SatTaskTipoRepo;

        public SatTaskTipoService(ISatTaskTipoRepository SatTaskTipoRepo, ISequenciaRepository seqRepo)
        {
            _SatTaskTipoRepo = SatTaskTipoRepo;
        }

        public ListViewModel ObterPorParametros(SatTaskTipoParameters parameters)
        {
            var perfis = _SatTaskTipoRepo.ObterPorParametros(parameters);

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

        public SatTaskTipo Criar(SatTaskTipo SatTaskTipo)
        {
            _SatTaskTipoRepo.Criar(SatTaskTipo);
            return SatTaskTipo;
        }

        public void Deletar(int codigo)
        {
            _SatTaskTipoRepo.Deletar(codigo);
        }

        public void Atualizar(SatTaskTipo SatTaskTipo)
        {
            _SatTaskTipoRepo.Atualizar(SatTaskTipo);
        }

        public SatTaskTipo ObterPorCodigo(int codigo)
        {
            return _SatTaskTipoRepo.ObterPorCodigo(codigo);
        }
    }
}
