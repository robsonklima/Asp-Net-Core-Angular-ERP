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
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
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
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codLocalEnvioNFFaturamento, int codPosto, int codContrato)
        {
            LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado = _context
                                                                                    .LocalEnvioNFFaturamentoVinculado
                                                                                        .FirstOrDefault(f => f.CodContrato == codContrato && 
                                                                                                            f.CodLocalEnvioNFFaturamento == codLocalEnvioNFFaturamento && 
                                                                                                            f.CodPosto == codPosto);

            if (localEnvioNFFaturamentoVinculado != null)
            {
                _context.LocalEnvioNFFaturamentoVinculado.Remove(localEnvioNFFaturamentoVinculado);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
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
                                    .Include(l => l.LocalAtendimento)
                                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                locais = locais.Where(
                    l =>
                    l.CodPosto.ToString().Contains(parameters.Filter)
                );

            if (parameters.CodContrato.HasValue)
                locais = locais.Where(
                    l =>
                    l.CodContrato == parameters.CodContrato.Value);

            if (parameters.CodLocalEnvioNFFaturamento.HasValue)
                locais = locais.Where(
                    l =>
                    l.CodLocalEnvioNFFaturamento == parameters.CodLocalEnvioNFFaturamento.Value);


            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                locais = locais.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<LocalEnvioNFFaturamentoVinculado>.ToPagedList(locais, parameters.PageNumber, parameters.PageSize);
        }
    }
}
