using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TipoEquipamentoRepository : ITipoEquipamentoRepository
    {
        private readonly AppDbContext _context;

        public TipoEquipamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(TipoEquipamento tipoEquipamento)
        {
            _context.ChangeTracker.Clear();
            TipoEquipamento t = _context.TipoEquipamento.SingleOrDefault(t => t.CodTipoEquip == tipoEquipamento.CodTipoEquip);

            if (t != null)
            {
                try
                {
                    _context.Entry(t).CurrentValues.SetValues(tipoEquipamento);
                    _context.Entry(t).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(TipoEquipamento tipoEquipamento)
        {
            try
            {
                _context.Add(tipoEquipamento);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            var tipoEquipamento = _context.TipoEquipamento.SingleOrDefault(t => t.CodTipoEquip == codigo);

            if (tipoEquipamento != null)
            {
                _context.TipoEquipamento.Remove(tipoEquipamento);

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

        public TipoEquipamento ObterPorCodigo(int codigo)
        {
            return _context.TipoEquipamento.SingleOrDefault(t => t.CodTipoEquip == codigo);
        }

        public PagedList<TipoEquipamento> ObterPorParametros(TipoEquipamentoParameters parameters)
        {
            var tipos = _context.TipoEquipamento.AsQueryable();

            if (parameters.Filter != null)
            {
                tipos = tipos.Where(
                            t =>
                            t.NomeTipoEquip.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodTipoEquip.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodTipoEquip != null)
            {
                tipos = tipos.Where(t => t.CodTipoEquip == parameters.CodTipoEquip);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tipos = tipos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<TipoEquipamento>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
