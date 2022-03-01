using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class LocalEnvioNFFaturamentoRepository : ILocalEnvioNFFaturamentoRepository
    {
        private readonly AppDbContext _context;

        public LocalEnvioNFFaturamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(LocalEnvioNFFaturamento localEnvioNFFaturamento)
        {
            _context.ChangeTracker.Clear();
            LocalEnvioNFFaturamento local = _context.LocalEnvioNFFaturamento.SingleOrDefault(l => l.CodLocalEnvioNFFaturamento == localEnvioNFFaturamento.CodLocalEnvioNFFaturamento);
            
            if (local != null)
            {
                try
                {
                    _context.Entry(local).CurrentValues.SetValues(localEnvioNFFaturamento);
                    _context.SaveChanges();
                } 
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Criar(LocalEnvioNFFaturamento localEnvioNFFaturamento)
        {
            try
            {
                _context.Add(localEnvioNFFaturamento);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Deletar(int codigo)
        {
            var localEnvioNFFaturamento = _context.LocalEnvioNFFaturamento.SingleOrDefault(l => l.CodLocalEnvioNFFaturamento == codigo);

            if (localEnvioNFFaturamento != null)
            {
                _context.LocalEnvioNFFaturamento.Remove(localEnvioNFFaturamento);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public LocalEnvioNFFaturamento ObterPorCodigo(int codigo)
        {
            return _context.LocalEnvioNFFaturamento
                .Include(l => l.Cliente)
                .Include(l => l.Contrato)
                .Include(l => l.CidadeEnvioNF)
                    .ThenInclude(c => c.UnidadeFederativa)
                .Include(l => l.CidadeFaturamento)
                    .ThenInclude(c => c.UnidadeFederativa)
                .Include(l => l.LocaisVinculados)
                .FirstOrDefault(f => f.CodLocalEnvioNFFaturamento == codigo);
        }

        public PagedList<LocalEnvioNFFaturamento> ObterPorParametros(LocalEnvioNFFaturamentoParameters parameters)
        {
            var locais = _context.LocalEnvioNFFaturamento
                .Include(l => l.Cliente)
                .Include(l => l.Contrato)
                .Include(l => l.CidadeEnvioNF)
                    .ThenInclude(c => c.UnidadeFederativa)
                .Include(l => l.CidadeFaturamento)        
                    .ThenInclude(c => c.UnidadeFederativa)        
                .Include(l => l.LocaisVinculados)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                locais = locais.Where(
                    l =>
                    l.CodLocalEnvioNFFaturamento.ToString().Contains(parameters.Filter)
                );            

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                locais = locais.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<LocalEnvioNFFaturamento>.ToPagedList(locais, parameters.PageNumber, parameters.PageSize);
        }
    }
}