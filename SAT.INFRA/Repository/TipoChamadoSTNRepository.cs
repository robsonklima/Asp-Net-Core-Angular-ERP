using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.Views;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class TipoChamadoSTNRepository : ITipoChamadoSTNRepository
    {
        private readonly AppDbContext _context;

        public TipoChamadoSTNRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(TipoChamadoSTN tipoChamadoSTN)
        {
            _context.ChangeTracker.Clear();
            TipoChamadoSTN aud = _context.TipoChamadoSTN
                .FirstOrDefault(aud => aud.CodTipoChamadoSTN == tipoChamadoSTN.CodTipoChamadoSTN);
            try
            {
                if (aud != null)
                {
                    _context.Entry(aud).CurrentValues.SetValues(tipoChamadoSTN);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(TipoChamadoSTN tipoChamadoSTN)
        {
            try
            {
                _context.Add(tipoChamadoSTN);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codTipoChamadoSTN)
        {
            TipoChamadoSTN aud = _context.TipoChamadoSTN
                .FirstOrDefault(aud => aud.CodTipoChamadoSTN == codTipoChamadoSTN);

            if (aud != null)
            {
                _context.TipoChamadoSTN.Remove(aud);
                _context.SaveChanges();
            }
        }

        public TipoChamadoSTN ObterPorCodigo(int codTipoChamadoSTN)
        {
            try
            {
                return _context.TipoChamadoSTN
                    .SingleOrDefault(aud => aud.CodTipoChamadoSTN == codTipoChamadoSTN);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a tipoChamadoSTN {ex.Message}");
            }
        }

        public PagedList<TipoChamadoSTN> ObterPorParametros(TipoChamadoSTNParameters parameters)
        {
            var tipoChamadoSTNs = _context.TipoChamadoSTN
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                tipoChamadoSTNs = tipoChamadoSTNs.Where(a =>
                    a.CodTipoChamadoSTN.ToString().Contains(parameters.Filter));
            }

            if (parameters.CodTipoChamadoSTN != null)
            {
                tipoChamadoSTNs = tipoChamadoSTNs.Where(a => a.CodTipoChamadoSTN == parameters.CodTipoChamadoSTN);
            };

            if (parameters.IndAtivo != null)
            {
                tipoChamadoSTNs = tipoChamadoSTNs.Where(a => a.IndAtivo == parameters.IndAtivo);
            };

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tipoChamadoSTNs = tipoChamadoSTNs.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<TipoChamadoSTN>.ToPagedList(tipoChamadoSTNs, parameters.PageNumber, parameters.PageSize);
        }
        
    }
}
