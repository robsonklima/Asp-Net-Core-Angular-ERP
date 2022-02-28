using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using SAT.MODELS.Entities.Constants;
using System;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Repository
{
    public class AcordoNivelServicoRepository : IAcordoNivelServicoRepository
    {
        private readonly AppDbContext _context;

        public AcordoNivelServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(AcordoNivelServico acordoNivelServico)
        {
            _context.ChangeTracker.Clear();
            AcordoNivelServico ans = _context.AcordoNivelServico.SingleOrDefault(a => a.CodSLA == acordoNivelServico.CodSLA);

            if (ans != null)
            {
                try
                {
                    _context.Entry(ans).CurrentValues.SetValues(acordoNivelServico);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(AcordoNivelServico acordoNivelServico)
        {
            try
            {
                _context.Add(acordoNivelServico);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            AcordoNivelServico ans = _context.AcordoNivelServico.SingleOrDefault(a => a.CodSLA == codigo);

            if (ans != null)
            {
                _context.AcordoNivelServico.Remove(ans);

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

        public AcordoNivelServico ObterPorCodigo(int codigo)
        {
            return _context.AcordoNivelServico.SingleOrDefault(a => a.CodSLA == codigo);
        }

        public PagedList<AcordoNivelServico> ObterPorParametros(AcordoNivelServicoParameters parameters)
        {
            var slas = _context.AcordoNivelServico.AsQueryable();

            if (parameters.Filter != null)
            {
                slas = slas.Where(
                            s =>
                            s.NomeSLA.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            s.CodSLA.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodSLA != null)
            {
                slas = slas.Where(s => s.CodSLA == parameters.CodSLA);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                slas = slas.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<AcordoNivelServico>.ToPagedList(slas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
