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
    public class GrupoEquipamentoRepository : IGrupoEquipamentoRepository
    {
        private readonly AppDbContext _context;

        public GrupoEquipamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(GrupoEquipamento grupoEquipamento)
        {
            GrupoEquipamento grupo = _context.GrupoEquipamento.SingleOrDefault(g => g.CodGrupoEquip == grupoEquipamento.CodGrupoEquip);
            if (grupo != null)
            {
                _context.Entry(grupo).CurrentValues.SetValues(grupoEquipamento);

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

        public void Criar(GrupoEquipamento grupoEquipamento)
        {
            try
            {
                _context.Add(grupoEquipamento);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            GrupoEquipamento grupoEquipamento = _context.GrupoEquipamento.SingleOrDefault(g => g.CodGrupoEquip == codigo);

            if (grupoEquipamento != null)
            {
                _context.GrupoEquipamento.Remove(grupoEquipamento);

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

        public GrupoEquipamento ObterPorCodigo(int codigo)
        {
            return _context.GrupoEquipamento
                .Include(g => g.TipoEquipamento)
                .FirstOrDefault(g => g.CodGrupoEquip == codigo);
        }

        public PagedList<GrupoEquipamento> ObterPorParametros(GrupoEquipamentoParameters parameters)
        {
            var grupos = _context.GrupoEquipamento
                .Include(g => g.TipoEquipamento)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                grupos = grupos.Where(
                            g =>
                            g.CodGrupoEquip.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            g.CodEGrupoEquip.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            g.NomeGrupoEquip.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodGrupoEquip != null)
            {
                grupos = grupos.Where(g => g.CodGrupoEquip == parameters.CodGrupoEquip);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                grupos = grupos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<GrupoEquipamento>.ToPagedList(grupos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
