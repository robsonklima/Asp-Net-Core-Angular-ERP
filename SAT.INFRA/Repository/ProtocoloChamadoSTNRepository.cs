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
    public class ProtocoloChamadoSTNRepository : IProtocoloChamadoSTNRepository
    {
        private readonly AppDbContext _context;

        public ProtocoloChamadoSTNRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ProtocoloChamadoSTN protocoloChamadoSTN)
        {
            _context.ChangeTracker.Clear();
            ProtocoloChamadoSTN p = _context.ProtocoloChamadoSTN
                .FirstOrDefault(p => p.CodProtocoloChamadoSTN == protocoloChamadoSTN.CodProtocoloChamadoSTN);
            try
            {
                if (p != null)
                {
                    _context.Entry(p).CurrentValues.SetValues(protocoloChamadoSTN);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(ProtocoloChamadoSTN protocoloChamadoSTN)
        {
            try
            {
                _context.Add(protocoloChamadoSTN);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codProtocoloChamadoSTN)
        {
            ProtocoloChamadoSTN p = _context.ProtocoloChamadoSTN
                .FirstOrDefault(p => p.CodProtocoloChamadoSTN == codProtocoloChamadoSTN);

            if (p != null)
            {
                _context.ProtocoloChamadoSTN.Remove(p);
                _context.SaveChanges();
            }
        }

        public ProtocoloChamadoSTN ObterPorCodigo(int codProtocoloChamadoSTN)
        {
            try
            {
                return _context.ProtocoloChamadoSTN
                    .Include(p => p.TipoChamadoSTN)
                    .Include(p => p.Usuario)
                    .Include(p => p.CausaImprodutividades)
                    .SingleOrDefault(p => p.CodProtocoloChamadoSTN == codProtocoloChamadoSTN);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a ProtocoloChamadoSTN {ex.Message}");
            }
        }

        public PagedList<ProtocoloChamadoSTN> ObterPorParametros(ProtocoloChamadoSTNParameters parameters)
        {
            var protocoloChamadoSTNs = _context.ProtocoloChamadoSTN
                .Include(p => p.TipoChamadoSTN)
                .Include(p => p.Usuario)
                .Include(p => p.CausaImprodutividades)                
                .AsQueryable();

            if (parameters.CodAtendimento.HasValue)
            {
                protocoloChamadoSTNs = protocoloChamadoSTNs.Where(p => p.CodAtendimento == parameters.CodAtendimento);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                protocoloChamadoSTNs = protocoloChamadoSTNs.Where(a =>
                    a.CodProtocoloChamadoSTN.ToString().Contains(parameters.Filter));
            }
            
            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                protocoloChamadoSTNs = protocoloChamadoSTNs.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            var query = protocoloChamadoSTNs.ToQueryString();

            return PagedList<ProtocoloChamadoSTN>.ToPagedList(protocoloChamadoSTNs, parameters.PageNumber, parameters.PageSize);
        }

    }
}
