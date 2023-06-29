using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class SATFeriadoService : ISATFeriadoService
    {
        private readonly ISATFeriadoRepository _feriadoRepo;

        public SATFeriadoService(ISATFeriadoRepository feriadoRepo)
        {
            _feriadoRepo = feriadoRepo;
        }

        public ListViewModel ObterPorParametros(SATFeriadoParameters parameters)
        {
            var feriados = _feriadoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = feriados,
                TotalCount = feriados.TotalCount,
                CurrentPage = feriados.CurrentPage,
                PageSize = feriados.PageSize,
                TotalPages = feriados.TotalPages,
                HasNext = feriados.HasNext,
                HasPrevious = feriados.HasPrevious
            };

            return lista;
        }

        public SATFeriado Criar(SATFeriado SATFeriado)
        {
            _feriadoRepo.Criar(SATFeriado);
            return SATFeriado;
        }

        public void Deletar(int codigo)
        {
            _feriadoRepo.Deletar(codigo);
        }

        public void Atualizar(SATFeriado SATFeriado)
        {
            _feriadoRepo.Atualizar(SATFeriado);
        }

        public SATFeriado ObterPorCodigo(int codigo)
        {
            return _feriadoRepo.ObterPorCodigo(codigo);
        }
    }
}
