using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        private readonly AppDbContext _context;
        private readonly IDespesaProtocoloPeriodoTecnicoRepository _despesaProtocoloPeriodoTecnicoRepo;

        public DespesaPeriodoTecnicoRepository(AppDbContext context, IDespesaProtocoloPeriodoTecnicoRepository despesaProtocoloPeriodoTecnicoRepo)
        {
            _context = context;
        }

        public void Atualizar(DespesaPeriodoTecnico despesaTecnico)
        {
            throw new NotImplementedException();
        }

        public void Criar(DespesaPeriodoTecnico despesa)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaPeriodoTecnico ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaPeriodoTecnico> ObterPorParametros(DespesaPeriodoTecnicoParameters parameters)
        {
            var query = _context.DespesaPeriodoTecnico.AsNoTracking().AsQueryable();

            query = AplicarIncludes(query);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return PagedList<DespesaPeriodoTecnico>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);

        }
    }
}