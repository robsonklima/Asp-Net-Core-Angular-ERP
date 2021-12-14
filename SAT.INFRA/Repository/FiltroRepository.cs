using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class FiltroRepository : IFiltroRepository
    {
        private readonly AppDbContext _context;

        public FiltroRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(FiltroUsuario filtroUsuario)
        {
            FiltroUsuario data = _context.FiltroUsuario.SingleOrDefault(a => a.CodFiltroUsuario == filtroUsuario.CodFiltroUsuario);

            if (data != null)
            {
                try
                {
                    _context.Entry(data).CurrentValues.SetValues(filtroUsuario);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(FiltroUsuario filtroUsuario)
        {
            try
            {
                _context.Add(filtroUsuario);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            FiltroUsuario data = _context.FiltroUsuario.SingleOrDefault(a => a.CodFiltroUsuario == codigo);

            if (data != null)
            {
                try
                {
                    _context.FiltroUsuario.Remove(data);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public FiltroUsuario ObterPorCodigo(int codigo)
        {
            return _context.FiltroUsuario.SingleOrDefault(a => a.CodFiltroUsuario == codigo);
        }

        public PagedList<FiltroUsuario> ObterPorParametros(AcaoParameters parameters)
        {
            var acoes = _context.FiltroUsuario.AsQueryable();

            //if (parameters.Filter != null)
            //{
            //    acoes = acoes.Where(
            //        c =>
            //        c.NomeAcao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
            //        c.CodEAcao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
            //        c.CodAcao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
            //    );
            //}

            //if (parameters.CodAcao != null)
            //{
            //    acoes = acoes.Where(a => a.CodAcao == parameters.CodAcao);
            //}

            //if (parameters.IndAtivo != null)
            //{
            //    acoes = acoes.Where(a => a.IndAtivo == parameters.IndAtivo);
            //}

            //if (parameters.SortActive != null && parameters.SortDirection != null)
            //{
            //    acoes = acoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            //}

            return PagedList<FiltroUsuario>.ToPagedList(acoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
