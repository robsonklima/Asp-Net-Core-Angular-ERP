using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PontoUsuarioDataRepository : IPontoUsuarioDataRepository
    {
        private readonly AppDbContext _context;

        public PontoUsuarioDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PontoUsuarioData pontoUsuarioData)
        {
            PontoUsuarioData per = _context.PontoUsuarioData.SingleOrDefault(p => p.CodPontoUsuarioData == pontoUsuarioData.CodPontoUsuarioData);

            if (per != null)
            {
                _context.Entry(per).CurrentValues.SetValues(pontoUsuarioData);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(PontoUsuarioData pontoUsuarioData)
        {
            try
            {
                _context.Add(pontoUsuarioData);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            PontoUsuarioData per = _context.PontoUsuarioData.SingleOrDefault(p => p.CodPontoUsuarioData == codigo);

            if (per != null)
            {
                _context.PontoUsuarioData.Remove(per);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public PontoUsuarioData ObterPorCodigo(int codigo)
        {
            return _context.PontoUsuarioData.SingleOrDefault(p => p.CodPontoUsuarioData == codigo);
        }

        public PagedList<PontoUsuarioData> ObterPorParametros(PontoUsuarioDataParameters parameters)
        {
            var query = _context.PontoUsuarioData
                .Include(pd => pd.PontoUsuarioDataStatusAcesso)
                .Include(pd => pd.PontoUsuarioDataStatus)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodPontoUsuarioData
                        .ToString()
                        .Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodUsuario != null) {
                query = query.Where(p => p.CodUsuario == parameters.CodUsuario);
            }

            if (parameters.CodPontoPeriodo != null) {
                query = query.Where(p => p.CodPontoPeriodo == parameters.CodPontoPeriodo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<PontoUsuarioData>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
