using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
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
            _context.ChangeTracker.Clear();
            DespesaProtocolo d = _context.DespesaProtocolo.FirstOrDefault(d => d.CodDespesaProtocolo == protocolo.CodDespesaProtocolo);

            if (d != null)
            {
                _context.Entry(d).CurrentValues.SetValues(protocolo);
                _context.SaveChanges();
            }
        }

        public DespesaProtocolo Criar(DespesaProtocolo protocolo)
        {
            try
            {
                _context.Add(protocolo);
                _context.SaveChanges();
                return protocolo;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codDespesaProtocolo)
        {
            DespesaProtocolo dp = _context.DespesaProtocolo
                .FirstOrDefault(dp => dp.CodDespesaProtocolo == codDespesaProtocolo);

            if (dp != null)
            {
                _context.DespesaProtocolo.Remove(dp);
                _context.SaveChanges();
            }
        }

        public DespesaProtocolo ObterPorCodigo(int codigo)
        {
            return _context.DespesaProtocolo
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.Tecnico)
                            .ThenInclude(d => d.Filial)
                .Include(d => d.DespesaProtocoloPeriodoTecnico)
                    .ThenInclude(d => d.DespesaPeriodoTecnico)
                        .ThenInclude(d => d.Tecnico)
                            .ThenInclude(d => d.DespesaCartaoCombustivelTecnico)
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
                            .ThenInclude(d => d.Filial)
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
                .AsQueryable();

            if (parameters.CodFilial != null) {
                protocolos = protocolos.Where(p => p.CodFilial == parameters.CodFilial);
            }

            if (!string.IsNullOrEmpty(parameters.Filter))
                protocolos =
                    protocolos.Where(t => t.CodDespesaProtocolo.ToString().Contains(parameters.Filter) ||
                        t.NomeProtocolo.Contains(parameters.Filter));

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                protocolos = protocolos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<DespesaProtocolo>.ToPagedList(protocolos, parameters.PageNumber, parameters.PageSize);
        }
    }
}