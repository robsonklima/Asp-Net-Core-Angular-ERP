using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SAT.INFRA.Repository
{
    public class ORCheckListRepository : IORCheckListRepository
    {
        private readonly AppDbContext _context;

        public ORCheckListRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORCheckList checkList)
        {
            _context.ChangeTracker.Clear();
            ORCheckList c = _context.ORCheckList.FirstOrDefault(c => c.CodORCheckList == checkList.CodORCheckList);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(checkList);
                _context.SaveChanges();
            }
        }

        public void Criar(ORCheckList checkList)
        {
            _context.Add(checkList);
            _context.SaveChanges();
        }

        public void Deletar(int cod)
        {
            ORCheckList c = _context.ORCheckList.FirstOrDefault(c => c.CodORCheckList == cod);

            if (c != null)
            {
                _context.ORCheckList.Remove(c);
                _context.SaveChanges();
            }
        }

        public ORCheckList ObterPorCodigo(int cod)
        {
            return _context.ORCheckList
                .Include(c => c.UsuarioCadastro)
                .FirstOrDefault(c => c.CodORCheckList == cod);
        }

        public PagedList<ORCheckList> ObterPorParametros(ORCheckListParameters parameters)
        {
            var query = _context.ORCheckList
                .Include(c => c.UsuarioCadastro)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                query = query.Where(
                    s =>
                    s.CodORCheckList.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.Descricao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.UsuarioCadastro.NomeUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );

            if (!string.IsNullOrWhiteSpace(parameters.CodUsuariosCad))
            {
                string[] cods = parameters.CodUsuariosCad.Split(",").Select(a => a.Trim()).Distinct().ToArray();
                query = query.Where(q => cods.Contains(q.CodUsuarioCad));
            }

            if (parameters.DataHoraCadInicio != DateTime.MinValue)
                query = query.Where(os => os.DataHoraCad >= parameters.DataHoraCadInicio);

            if (parameters.DataHoraCadFim != DateTime.MinValue)
                query = query.Where(os => os.DataHoraCad <= parameters.DataHoraCadFim);

            if (parameters.CodORCheckList.HasValue)
                query = query.Where(os => os.CodORCheckList == parameters.CodORCheckList);

            if (!string.IsNullOrWhiteSpace(parameters.Descricao))
                query = query.Where(os => os.Descricao.Contains(parameters.Descricao));

            if (parameters.SortActive != null && parameters.SortDirection != null)
                 query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<ORCheckList>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
