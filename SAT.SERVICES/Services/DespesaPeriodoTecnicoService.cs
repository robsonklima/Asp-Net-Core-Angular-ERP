using System;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        private readonly IDespesaPeriodoRepository _despesaPeriodoRepo;
        private readonly IDespesaAdiantamentoPeriodoRepository _despesaAdiantamentoPeriodoRepo;
        private readonly IDespesaPeriodoTecnicoRepository _despesaPeriodoTecnicoRepo;

        public DespesaPeriodoTecnicoService(
            IDespesaPeriodoTecnicoRepository despesaPeriodoTecnicoRepo,
            IDespesaAdiantamentoPeriodoRepository despesaAdiantamentoPeriodoRepo,
            IDespesaPeriodoRepository despesaPeriodoRepo)
        {
            _despesaPeriodoTecnicoRepo = despesaPeriodoTecnicoRepo;
            _despesaAdiantamentoPeriodoRepo = despesaAdiantamentoPeriodoRepo;
            _despesaPeriodoRepo = despesaPeriodoRepo;
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

        public DespesaPeriodoViewModel ObterPorParametros(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesasPeriodoTecnico =
                PagedList<DespesaPeriodoTecnicoViewModel>.ToPagedList(
                    GetDespesaPeriodoViewModel(parameters), parameters.PageNumber, parameters.PageSize);

            var lista = new DespesaPeriodoViewModel
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