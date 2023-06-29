using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class SATFeriadosService : ISATFeriadosService
    {
        private readonly ISATFeriadosRepository _satFeriadosRepo;

        public SATFeriadosService(ISATFeriadosRepository satFeriadosRepo)
        {
            _satFeriadosRepo = satFeriadosRepo;
        }

        public ListViewModel ObterPorParametros(SATFeriadosParameters parameters)
        {
            var satFeriadoss = _satFeriadosRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = satFeriadoss,
                TotalCount = satFeriadoss.TotalCount,
                CurrentPage = satFeriadoss.CurrentPage,
                PageSize = satFeriadoss.PageSize,
                TotalPages = satFeriadoss.TotalPages,
                HasNext = satFeriadoss.HasNext,
                HasPrevious = satFeriadoss.HasPrevious
            };

            return lista;
        }

        public SATFeriados Criar(SATFeriados satFeriados)
        {
            _satFeriadosRepo.Criar(satFeriados);
            return satFeriados;
        }

        public void Deletar(int codigo)
        {
            _satFeriadosRepo.Deletar(codigo);
        }

        public void Atualizar(SATFeriados satFeriados)
        {
            _satFeriadosRepo.Atualizar(satFeriados);
        }

        public SATFeriados ObterPorCodigo(int codigo)
        {
            return _satFeriadosRepo.ObterPorCodigo(codigo);
        }
    }
}
