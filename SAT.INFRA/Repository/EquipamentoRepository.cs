using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.MODELS.Entities.Constants;
using System;

namespace SAT.INFRA.Repository
{
    public partial class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly AppDbContext _context;

        public EquipamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Equipamento equipamento)
        {
            _context.ChangeTracker.Clear();
            Equipamento linha = _context.Equipamento.SingleOrDefault(a => a.CodEquip == equipamento.CodEquip);

            if (linha != null)
            {
                try
                {
                    _context.Entry(linha).CurrentValues.SetValues(equipamento);
                    _context.Entry(linha).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(Equipamento equipamento)
        {
            try
            {
                _context.Add(equipamento);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            Equipamento linha = _context.Equipamento.SingleOrDefault(a => a.CodEquip == codigo);

            if (linha != null)
            {
                _context.Equipamento.Remove(linha);

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

        public Equipamento ObterPorCodigo(int codigo)
        {
            return _context.Equipamento.SingleOrDefault(e => e.CodEquip == codigo);
        }

        public PagedList<Equipamento> ObterPorParametros(EquipamentoParameters parameters)
        {
            var equips = _context.Equipamento
                .Include(e => e.TipoEquipamento)
                .Include(e => e.GrupoEquipamento)
                .AsQueryable();

            equips = AplicarFiltros(equips, parameters);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                equips = equips.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<Equipamento>.ToPagedList(equips, parameters.PageNumber, parameters.PageSize);
        }
    }
}