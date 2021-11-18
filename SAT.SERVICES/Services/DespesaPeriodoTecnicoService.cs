using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        private readonly IDespesaPeriodoRepository _despesaPeriodoRepo;
        private readonly IDespesaAdiantamentoPeriodoRepository _despesaAdiantamentoPeriodoRepo;
        private readonly IDespesaPeriodoTecnicoRepository _despesaPeriodoTecnicoRepo;
        private readonly IDespesaAdiantamentoPeriodoService _despesaAdiantamentoPeriodoService;

        public DespesaPeriodoTecnicoService(
            IDespesaPeriodoTecnicoRepository despesaPeriodoTecnicoRepo,
            IDespesaAdiantamentoPeriodoRepository despesaAdiantamentoPeriodoRepo,
            IDespesaPeriodoRepository despesaPeriodoRepo,
            IDespesaAdiantamentoPeriodoService despesaAdiantamentoPeriodoService
            )
        {
            _despesaPeriodoTecnicoRepo = despesaPeriodoTecnicoRepo;
            _despesaAdiantamentoPeriodoRepo = despesaAdiantamentoPeriodoRepo;
            _despesaPeriodoRepo = despesaPeriodoRepo;
            _despesaAdiantamentoPeriodoService = despesaAdiantamentoPeriodoService;
        }

        public void Atualizar(DespesaPeriodoTecnico despesa)
        {
            throw new System.NotImplementedException();
        }

        public DespesaPeriodoTecnico Criar(DespesaPeriodoTecnico despesa)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public DespesaPeriodoTecnico ObterPorCodigo(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public ListViewModel ObterPorParametros(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesas = _despesaPeriodoTecnicoRepo.ObterPorParametros(parameters);

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