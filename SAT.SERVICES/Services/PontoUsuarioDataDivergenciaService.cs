using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoUsuarioDataDivergenciaService : IPontoUsuarioDataDivergenciaService
    {
        private IPontoUsuarioDataDivergenciaRepository _pontoUsuarioDataDivergenciaRepo { get; }

        public PontoUsuarioDataDivergenciaService(IPontoUsuarioDataDivergenciaRepository pontoUsuarioDataDivergenciaRepo)
        {
            _pontoUsuarioDataDivergenciaRepo = pontoUsuarioDataDivergenciaRepo;
        }

        public ListViewModel ObterPorParametros(PontoUsuarioDataDivergenciaParameters parameters)
        {
            var pontoUsuarioDataDivergencias = _pontoUsuarioDataDivergenciaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = pontoUsuarioDataDivergencias,
                TotalCount = pontoUsuarioDataDivergencias.TotalCount,
                CurrentPage = pontoUsuarioDataDivergencias.CurrentPage,
                PageSize = pontoUsuarioDataDivergencias.PageSize,
                TotalPages = pontoUsuarioDataDivergencias.TotalPages,
                HasNext = pontoUsuarioDataDivergencias.HasNext,
                HasPrevious = pontoUsuarioDataDivergencias.HasPrevious
            };

            return lista;
        }

        public PontoUsuarioDataDivergencia Criar(PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia)
        {
            _pontoUsuarioDataDivergenciaRepo.Criar(pontoUsuarioDataDivergencia);

            return pontoUsuarioDataDivergencia;
        }

        public void Deletar(int codigo)
        {
            _pontoUsuarioDataDivergenciaRepo.Deletar(codigo);
        }

        public void Atualizar(PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia)
        {
            _pontoUsuarioDataDivergenciaRepo.Atualizar(pontoUsuarioDataDivergencia);
        }

        public PontoUsuarioDataDivergencia ObterPorCodigo(int codigo)
        {
            return _pontoUsuarioDataDivergenciaRepo.ObterPorCodigo(codigo);
        }
    }
}
