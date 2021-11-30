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
        private readonly IDespesaRepository _despesaRepository;

        public DespesaPeriodoTecnicoService(
            IDespesaPeriodoTecnicoRepository despesaPeriodoTecnicoRepo,
            IDespesaAdiantamentoPeriodoRepository despesaAdiantamentoPeriodoRepo,
            IDespesaPeriodoRepository despesaPeriodoRepo,
            IDespesaRepository despesaRepository,
            IDespesaAdiantamentoPeriodoService despesaAdiantamentoPeriodoService
            )
        {
            _despesaPeriodoTecnicoRepo = despesaPeriodoTecnicoRepo;
            _despesaAdiantamentoPeriodoRepo = despesaAdiantamentoPeriodoRepo;
            _despesaPeriodoRepo = despesaPeriodoRepo;
            _despesaAdiantamentoPeriodoService = despesaAdiantamentoPeriodoService;
            _despesaRepository = despesaRepository;
        }

        public void Atualizar(DespesaPeriodoTecnico despesa)
        {
            _despesaPeriodoTecnicoRepo.Atualizar(despesa);
        }

        public DespesaPeriodoTecnico Criar(DespesaPeriodoTecnico despesa)
        {
            _despesaPeriodoTecnicoRepo.Criar(despesa);
            return despesa;
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public DespesaPeriodoTecnico ObterPorCodigo(int codigo)
        {
            return _despesaPeriodoTecnicoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesas = _despesaPeriodoTecnicoRepo.ObterPorParametros(parameters);
            despesas = ObterPropriedadesCalculadas(despesas, parameters);

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