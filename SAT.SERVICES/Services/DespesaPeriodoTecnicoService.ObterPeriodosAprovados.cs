using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        public ListViewModel ObterPeriodosAprovados(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesas = _despesaPeriodoTecnicoRepo.ObterPorParametros(new DespesaPeriodoTecnicoParameters
            {
                FilterType = DespesaPeriodoTecnicoFilterEnum.FILTER_PERIODOS_APROVADOS,
                PageSize = parameters.PageSize,
                PageNumber = parameters.PageNumber
            });

            var lista = new ListViewModel
            {
                Items = despesas,
                TotalCount = despesas.TotalCount,
                CurrentPage = despesas.CurrentPage,
                PageSize = despesas.PageSize,
                TotalPages = despesas.TotalPages,
                HasNext = despesas.HasNext,
                HasPrevious = despesas.HasPrevious
            };

            return lista;
        }

    }
}