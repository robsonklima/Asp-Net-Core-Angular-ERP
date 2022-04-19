using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PontoUsuarioDataDivergenciaRepository : IPontoUsuarioDataDivergenciaRepository
    {
        private readonly AppDbContext _context;

        public PontoUsuarioDataDivergenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia)
        {
            _context.ChangeTracker.Clear();
            PontoUsuarioDataDivergencia per = _context.PontoUsuarioDataDivergencia.SingleOrDefault(p => p.CodPontoUsuarioDataDivergencia == pontoUsuarioDataDivergencia.CodPontoUsuarioDataDivergencia);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(pontoUsuarioDataDivergencia);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia)
        {
            // try
            // {
                _context.Add(pontoUsuarioDataDivergencia);
                _context.SaveChanges();
            // }
            // catch (DbUpdateException)
            // {
            //     throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            // }
        }

        public void Deletar(int codigo)
        {
            PontoUsuarioDataDivergencia per = _context.PontoUsuarioDataDivergencia.SingleOrDefault(p => p.CodPontoUsuarioDataDivergencia == codigo);

            if (per != null)
            {
                _context.PontoUsuarioDataDivergencia.Remove(per);

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

        public PontoUsuarioDataDivergencia ObterPorCodigo(int codigo)
        {
            return _context.PontoUsuarioDataDivergencia.SingleOrDefault(p => p.CodPontoUsuarioDataDivergencia == codigo);
        }

        public PagedList<PontoUsuarioDataDivergencia> ObterPorParametros(PontoUsuarioDataDivergenciaParameters parameters)
        {
            var query = _context.PontoUsuarioDataDivergencia
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodPontoUsuarioDataDivergencia.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodPontoUsuarioData != null)
            {
                query = query.Where(p => p.CodPontoUsuarioData == parameters.CodPontoUsuarioData);
            }

            if (parameters.DivergenciaValidada != null)
            {
                query = query.Where(p => p.DivergenciaValidada == parameters.DivergenciaValidada);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            var a = query.ToQueryString();

            return PagedList<PontoUsuarioDataDivergencia>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
