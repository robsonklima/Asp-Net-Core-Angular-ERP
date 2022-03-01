using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaAdiantamentoPeriodoService : IDespesaAdiantamentoPeriodoService
    {
        private readonly IDespesaAdiantamentoPeriodoRepository _despesaAdiantamentoPeriodoRepo;
        private readonly IDespesaAdiantamentoRepository _despesaAdiantamentoRepo;
        private readonly IDespesaPeriodoTecnicoRepository _despesaPeriodoTecnicoRepo;
        private readonly ITecnicoRepository _tecnicoRepo;


        public DespesaAdiantamentoPeriodoService(
            ITecnicoRepository tecnicoRepo,
            IDespesaAdiantamentoPeriodoRepository despesaAdiantamentoPeriodoRepo,
            IDespesaPeriodoTecnicoRepository despesaPeriodoTecnicoRepo,
            IDespesaAdiantamentoRepository despesaAdiantamentoRepo
            )
        {
            _despesaAdiantamentoPeriodoRepo = despesaAdiantamentoPeriodoRepo;
            _despesaPeriodoTecnicoRepo = despesaPeriodoTecnicoRepo;
            _tecnicoRepo = tecnicoRepo;
            _despesaAdiantamentoRepo = despesaAdiantamentoRepo;
        }

        public void Atualizar(DespesaAdiantamentoPeriodo despesa)
        {
            _despesaAdiantamentoPeriodoRepo.Atualizar(despesa);
        }

        public DespesaAdiantamentoPeriodo Criar(DespesaAdiantamentoPeriodo despesa)
        {
            _despesaAdiantamentoPeriodoRepo.Criar(despesa);

            return despesa;
        }

        public void Deletar(int codigo)
        {
            _despesaAdiantamentoPeriodoRepo.Deletar(codigo);
        }

        public DespesaAdiantamentoPeriodo ObterPorCodigo(int codigo)
        {
            return _despesaAdiantamentoPeriodoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaAdiantamentoPeriodoParameters parameters)
        {
            var despesaAdiantamentoPeriodo = _despesaAdiantamentoPeriodoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesaAdiantamentoPeriodo,
                TotalCount = despesaAdiantamentoPeriodo.TotalCount,
                CurrentPage = despesaAdiantamentoPeriodo.CurrentPage,
                PageSize = despesaAdiantamentoPeriodo.PageSize,
                TotalPages = despesaAdiantamentoPeriodo.TotalPages,
                HasNext = despesaAdiantamentoPeriodo.HasNext,
                HasPrevious = despesaAdiantamentoPeriodo.HasPrevious
            };

            return lista;
        }
    }
}