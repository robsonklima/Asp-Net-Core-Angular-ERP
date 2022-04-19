using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PontoUsuarioRepository : IPontoUsuarioRepository
    {
        private readonly AppDbContext _context;

        public PontoUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PontoUsuario pontoUsuario)
        {
            _context.ChangeTracker.Clear();
            PontoUsuario per = _context.PontoUsuario.SingleOrDefault(p => p.CodPontoUsuario == pontoUsuario.CodPontoUsuario);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(pontoUsuario);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public PontoUsuario Criar(PontoUsuario pontoUsuario)
        {
            try
            {
                _context.Add(pontoUsuario);
                _context.SaveChanges();
                return pontoUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            PontoUsuario per = _context.PontoUsuario.SingleOrDefault(p => p.CodPontoUsuario == codigo);

            if (per != null)
            {
                _context.PontoUsuario.Remove(per);

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

        public PontoUsuario ObterPorCodigo(int codigo)
        {
            return _context.PontoUsuario.SingleOrDefault(p => p.CodPontoUsuario == codigo);
        }

        public PagedList<PontoUsuario> ObterPorParametros(PontoUsuarioParameters parameters)
        {
            var query = _context.PontoUsuario
                .Include(p => p.PontoPeriodo)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodPontoUsuario.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodUsuario))
                query = query.Where(p => !string.IsNullOrEmpty(p.CodUsuario) && p.CodUsuario.ToLower() == parameters.CodUsuario.ToLower());

            if (parameters.CodPontoPeriodo.HasValue)
                query = query.Where(p => p.CodPontoPeriodo == parameters.CodPontoPeriodo);

            if (parameters.DataHoraRegistroInicio != DateTime.MinValue && parameters.DataHoraRegistroFim != DateTime.MinValue)
                query = query.Where(p => p.DataHoraRegistro.Date >= parameters.DataHoraRegistroInicio.Date && p.DataHoraRegistro.Date <= parameters.DataHoraRegistroFim.Date);

            if (parameters.DataHoraRegistro.HasValue)
                query = query.Where(p => p.DataHoraRegistro.Date == parameters.DataHoraRegistro.Value.Date);

            if (parameters.IndAtivo.HasValue)
                query = query.Where(p => p.IndAtivo == parameters.IndAtivo.Value);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<PontoUsuario>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
