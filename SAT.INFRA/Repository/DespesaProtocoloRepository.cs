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
    public class DespesaProtocoloRepository : IDespesaProtocoloRepository
    {
        private readonly AppDbContext _context;

        public DespesaProtocoloRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaProtocolo protocolo)
        {
            throw new NotImplementedException();
        }

        public void Criar(DespesaProtocolo protocolo)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaProtocolo ObterPorCodigo(int codigo)
        {
            return _context.DespesaProtocolo
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.Tecnico)
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.DespesaPeriodo)
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.DespesaPeriodoTecnicoStatus)
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.Despesas)
                            .ThenInclude(d => d.DespesaItens)
                .FirstOrDefault(i => i.CodDespesaProtocolo == codigo);
        }

        public PagedList<DespesaProtocolo> ObterPorParametros(DespesaProtocoloParameters parameters)
        {
            var protocolos = _context.DespesaProtocolo
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.Tecnico)
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.DespesaPeriodo)
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.DespesaPeriodoTecnicoStatus)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                protocolos =
                    protocolos.Where(t => t.CodDespesaProtocolo.ToString().Contains(parameters.Filter) ||
                        t.NomeProtocolo.Contains(parameters.Filter));

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                protocolos = protocolos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaProtocolo>.ToPagedList(protocolos, parameters.PageNumber, parameters.PageSize);
        }
    }
}