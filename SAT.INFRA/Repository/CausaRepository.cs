
using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using SAT.INFRA.Interfaces;

namespace SAT.INFRA.Repository
{
    public class CausaRepository : ICausaRepository
    {
        private readonly AppDbContext _context;

        public CausaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Causa causa)
        {
            _context.ChangeTracker.Clear();
            Causa c = _context.Causa.FirstOrDefault(c => c.CodCausa == causa.CodCausa);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(causa);
                _context.SaveChanges();
            }
        }

        public void Criar(Causa causa)
        {
            _context.Add(causa);
            _context.SaveChanges();
        }

        public void Deletar(int codCausa)
        {
            Causa c = _context.Causa.FirstOrDefault(c => c.CodCausa == codCausa);

            if (c != null)
            {
                _context.Causa.Remove(c);
                _context.SaveChanges();
            }
        }

        public Causa ObterPorCodigo(int codigo)
        {
            return _context.Causa.FirstOrDefault(c => c.CodCausa == codigo);
        }

        public PagedList<Causa> ObterPorParametros(CausaParameters parameters)
        {
            var causas = _context.Causa.AsQueryable();

            if (parameters.Filter != null)
            {
                causas = causas.Where(
                            s =>
                            s.CodCausa.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            s.NomeCausa.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            s.CodECausa.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodCausa.HasValue)
            {
                causas = causas.Where(c => c.CodCausa == parameters.CodCausa);
            }

            if (parameters.IndAtivo.HasValue)
            {
                causas = causas.Where(c => c.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.ApenasModulos.HasValue)
            {
                causas = causas.Where(c => c.CodECausa.EndsWith("000"));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodECausa))
            {
                string[] cods = parameters.CodECausa.Split(",").Select(a => a.Trim()).Distinct().ToArray();
                causas = causas.Where(dc => cods.Contains(dc.CodECausa));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                causas = causas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Causa>.ToPagedList(causas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
