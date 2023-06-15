using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class SetorRepository : ISetorRepository
    {
        private readonly AppDbContext _context;

        public SetorRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Setor setor)
        {
            _context.ChangeTracker.Clear();
            Setor per = _context.Setor.SingleOrDefault(p => p.CodSetor == setor.CodSetor);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(setor);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(Setor setor)
        {
            try
            {
                _context.Add(setor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            Setor per = _context.Setor.SingleOrDefault(p => p.CodSetor == codigo);

            if (per != null)
            {
                _context.Setor.Remove(per);

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

        public Setor ObterPorCodigo(int codigo)
        {
            return _context.Setor.SingleOrDefault(p => p.CodSetor == codigo);
        }

        public PagedList<Setor> ObterPorParametros(SetorParameters parameters)
        {
            var perfis = _context.Setor
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodSetor.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.NomeSetor.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)

                );
            }

            if (parameters.CodSetor != null)
            {
                perfis = perfis.Where(p => p.CodSetor == parameters.CodSetor);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Setor>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
