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
    public class ANSRepository : IANSRepository
    {
        private readonly AppDbContext _context;

        public ANSRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ANS ans)
        {
            _context.ChangeTracker.Clear();
            ANS a = _context.ANS
                .FirstOrDefault(a => a.CodANS == ans.CodANS);
            try
            {
                if (a != null)
                {
                    _context.Entry(a).CurrentValues.SetValues(ans);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(ANS ans)
        {
            try
            {
                _context.Add(ans);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codigoAns)
        {
            ANS a = _context.ANS
                .FirstOrDefault(a => a.CodANS == codigoAns);

            if (a != null)
            {
                _context.ANS.Remove(a);
                _context.SaveChanges();
            }
        }

        public ANS ObterPorCodigo(int codigoAns)
        {
            try
            {
                return _context.ANS
                    .SingleOrDefault(a => a.CodANS == codigoAns);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a ANS {ex.Message}");
            }
        }

        public PagedList<ANS> ObterPorParametros(ANSParameters parameters)
        {
            var anss = _context.ANS
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                anss = anss.Where(a =>
                    a.CodANS.ToString().Contains(parameters.Filter));
            }

            if (parameters.CodANS != null)
            {
                anss = anss.Where(a => a.CodANS == parameters.CodANS);
            };

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                anss = anss.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ANS>.ToPagedList(anss, parameters.PageNumber, parameters.PageSize);
        }
    }
}
