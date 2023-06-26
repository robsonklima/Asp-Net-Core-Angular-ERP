using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class SatTaskProcessoService : ISatTaskProcessoService
    {
        private readonly ISatTaskProcessoRepository _satTaskProcessoRepo;

        public SatTaskProcessoService(ISatTaskProcessoRepository satTaskProcessoRepo)
        {
            _satTaskProcessoRepo = satTaskProcessoRepo;
        }

        public ListViewModel ObterPorParametros(SatTaskProcessoParameters parameters)
        {
            var processos = _satTaskProcessoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = processos,
                TotalCount = processos.TotalCount,
                CurrentPage = processos.CurrentPage,
                PageSize = processos.PageSize,
                TotalPages = processos.TotalPages,
                HasNext = processos.HasNext,
                HasPrevious = processos.HasPrevious
            };

            return lista;
        }

        public SatTaskProcesso Criar(SatTaskProcesso satTaskProcesso)
        {
            _satTaskProcessoRepo.Criar(satTaskProcesso);

            return satTaskProcesso;
        }

        public SatTaskProcesso Deletar(int codigo)
        {
            return _satTaskProcessoRepo.Deletar(codigo);
        }

        public SatTaskProcesso Atualizar(SatTaskProcesso satTaskProcesso)
        {
            _satTaskProcessoRepo.Atualizar(satTaskProcesso);
            
            return satTaskProcesso;
        }

        public SatTaskProcesso ObterPorCodigo(int codigo)
        {
            return _satTaskProcessoRepo.ObterPorCodigo(codigo);
        }
    }
}
