using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
using SAT.MODELS.Views;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class BancadaLaboratorioRepository : IBancadaLaboratorioRepository
    {
        private readonly AppDbContext _context;

        public BancadaLaboratorioRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(BancadaLaboratorio BancadaLaboratorio)
        {
            _context.ChangeTracker.Clear();
            BancadaLaboratorio c = _context.BancadaLaboratorio.FirstOrDefault(c => c.NumBancada == BancadaLaboratorio.NumBancada);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(BancadaLaboratorio);
                _context.SaveChanges();
            }
        }

        public void Criar(BancadaLaboratorio BancadaLaboratorio)
        {
            _context.Add(BancadaLaboratorio);
            _context.SaveChanges();
        }

        public void Deletar(int num)
        {
            BancadaLaboratorio c = _context.BancadaLaboratorio.FirstOrDefault(c => c.NumBancada == num);

            if (c != null)
            {
                _context.BancadaLaboratorio.Remove(c);
                _context.SaveChanges();
            }
        }

        public BancadaLaboratorio ObterPorCodigo(int codigo)
        {
            return _context.BancadaLaboratorio
                .Include(b => b.UsuarioCadastro)
                .Include(b => b.Usuario)
                .FirstOrDefault(c => c.NumBancada == codigo);
        }

        public PagedList<BancadaLaboratorio> ObterPorParametros(BancadaLaboratorioParameters parameters)
        {
            IQueryable<BancadaLaboratorio> query = _context.BancadaLaboratorio
                .Include(b => b.UsuarioCadastro)
                .Include(b => b.Usuario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                query = query.Where(
                    s =>
                    s.NumBancada.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );

            if (parameters.IndUsuarioAtivo.HasValue)
                query = query.Where(b => b.Usuario.IndAtivo == parameters.IndUsuarioAtivo);

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<BancadaLaboratorio>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada()
        {
            return _context.ViewLaboratorioTecnicoBancada.ToList();
        }
    }
}
