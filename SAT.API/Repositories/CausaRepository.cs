using SAT.API.Context;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using SAT.API.Repositories.Interfaces;

namespace SAT.API.Repositories
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

            if (parameters.CodCausa != null)
            {
                causas = causas.Where(c => c.CodCausa == parameters.CodCausa);
            }

            if (parameters.IndAtivo != null)
            {
                causas = causas.Where(c => c.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                causas = causas.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Causa>.ToPagedList(causas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
