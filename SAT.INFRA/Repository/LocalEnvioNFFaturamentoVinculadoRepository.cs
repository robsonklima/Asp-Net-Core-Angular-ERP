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
    public class LocalEnvioNFFaturamentoVinculadoRepository : ILocalEnvioNFFaturamentoVinculadoRepository
    {
        private readonly AppDbContext _context;

        public LocalEnvioNFFaturamentoVinculadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado)
        {
            _context.ChangeTracker.Clear();
            LocalEnvioNFFaturamentoVinculado local = _context.LocalEnvioNFFaturamentoVinculado.SingleOrDefault(l => l.CodPosto == localEnvioNFFaturamentoVinculado.CodPosto);
            
            if (local != null)
            {
                try
                {
                    _context.Entry(local).CurrentValues.SetValues(localEnvioNFFaturamentoVinculado);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Criar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado)
        {
            try
            {
                _context.Add(localEnvioNFFaturamentoVinculado);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Deletar(int codigo)
        {
            var localEnvioNFFaturamentoVinculado = _context.LocalEnvioNFFaturamentoVinculado.SingleOrDefault(l => l.CodPosto == codigo);

            if (localEnvioNFFaturamentoVinculado != null)
            {
                _context.LocalEnvioNFFaturamentoVinculado.Remove(localEnvioNFFaturamentoVinculado);

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

        public LocalEnvioNFFaturamentoVinculado ObterPorCodigo(int codLocalEnvioNFFaturamento, int codPosto, int codContrato)
        {
            return _context.LocalEnvioNFFaturamentoVinculado
                .FirstOrDefault(f => f.CodContrato == codContrato && f.CodLocalEnvioNFFaturamento == codLocalEnvioNFFaturamento && f.CodPosto == codPosto);
        }

        public PagedList<LocalEnvioNFFaturamentoVinculado> ObterPorParametros(LocalEnvioNFFaturamentoVinculadoParameters parameters)
        {
            var locais = _context.LocalEnvioNFFaturamentoVinculado
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                locais = locais.Where(
                    l =>
                    l.CodPosto.ToString().Contains(parameters.Filter)
                );


            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                locais = locais.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<LocalEnvioNFFaturamentoVinculado>.ToPagedList(locais, parameters.PageNumber, parameters.PageSize);
        }
    }
}
