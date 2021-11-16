using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class DespesaProtocoloPeriodoTecnicoRepository : IDespesaProtocoloPeriodoTecnicoRepository
    {
        private readonly AppDbContext _context;

        public DespesaProtocoloPeriodoTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaProtocoloPeriodoTecnico despesa)
        {
            throw new NotImplementedException();
        }

        public void Criar(DespesaProtocoloPeriodoTecnico despesa)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaProtocoloPeriodoTecnico ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaProtocoloPeriodoTecnico ObterPorCodigoPeriodoTecnico(int codigo)
        {
            return _context.DespesaProtocoloPeriodoTecnico
            .Include(d => d.DespesaPeriodoTecnico)
            .FirstOrDefault(i => i.CodDespesaPeriodoTecnico == codigo);
        }

        public PagedList<DespesaProtocoloPeriodoTecnico> ObterPorParametros(DespesaProtocoloPeriodoTecnicoParameters parameters)
        {
            var despesasProtocoloPeriodoTecnico = _context.DespesaProtocoloPeriodoTecnico
                .Include(d => d.DespesaPeriodoTecnico)
                .AsQueryable();

            if (parameters.IndAtivo.HasValue)
                despesasProtocoloPeriodoTecnico = despesasProtocoloPeriodoTecnico.Where(i => i.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesasProtocoloPeriodoTecnico =
                    despesasProtocoloPeriodoTecnico.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaProtocoloPeriodoTecnico>.ToPagedList(despesasProtocoloPeriodoTecnico, parameters.PageNumber, parameters.PageSize);
        }
    }
}