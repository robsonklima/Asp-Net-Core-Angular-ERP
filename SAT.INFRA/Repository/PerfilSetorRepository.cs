using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class PerfilSetorRepository : IPerfilSetorRepository
    {
        private readonly AppDbContext _context;

        public PerfilSetorRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PerfilSetor perfilSetor)
        {
            _context.ChangeTracker.Clear();
            PerfilSetor per = _context.PerfilSetor.SingleOrDefault(p => p.CodPerfilSetor == perfilSetor.CodPerfilSetor);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(perfilSetor);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(PerfilSetor perfilSetor)
        {
            try
            {
                _context.Add(perfilSetor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            PerfilSetor per = _context.PerfilSetor.SingleOrDefault(p => p.CodPerfilSetor == codigo);

            if (per != null)
            {
                _context.PerfilSetor.Remove(per);

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

        public PerfilSetor ObterPorCodigo(int codigo)
        {
            return _context.PerfilSetor.SingleOrDefault(p => p.CodPerfilSetor == codigo);
        }

        public PagedList<PerfilSetor> ObterPorParametros(PerfilSetorParameters parameters)
        {
            var perfis = _context.PerfilSetor
                .Include(c => c.Setor)
                .Include(c => c.Perfil)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodPerfilSetor.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Perfil.NomePerfil.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodPerfilSetor != null)
            {
                perfis = perfis.Where(p => p.CodPerfilSetor == parameters.CodPerfilSetor);
            }

            if (parameters.CodSetor != null)
            {
                perfis = perfis.Where(p => p.CodSetor == parameters.CodSetor);
            }

            if (parameters.CodPerfil != null)
            {
                perfis = perfis.Where(p => p.CodPerfil == parameters.CodPerfil);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodSetores))
            {
                int[] setores = parameters.CodSetores.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                perfis = perfis.Where(p => setores.Contains(p.CodSetor));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<PerfilSetor>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
