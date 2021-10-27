using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        private readonly IDespesaPeriodoTecnicoRepository _despesaPeriodoTecnicoRepo;

        public DespesaPeriodoTecnicoService(IDespesaPeriodoTecnicoRepository despesaPeriodoTecnicoRepo)
        {
            _despesaPeriodoTecnicoRepo = despesaPeriodoTecnicoRepo;
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
            var despesasPeriodoTecnico = _despesaPeriodoTecnicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesasPeriodoTecnico,
                TotalCount = despesasPeriodoTecnico.TotalCount,
                CurrentPage = despesasPeriodoTecnico.CurrentPage,
                PageSize = despesasPeriodoTecnico.PageSize,
                TotalPages = despesasPeriodoTecnico.TotalPages,
                HasNext = despesasPeriodoTecnico.HasNext,
                HasPrevious = despesasPeriodoTecnico.HasPrevious
            };

            return lista;
        }
    }
}