using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using SAT.MODELS.Entities.Constants;
using System;

namespace SAT.INFRA.Repository
{
    public class EquipamentoContratoRepository : IEquipamentoContratoRepository
    {
        private AppDbContext _context;

        public EquipamentoContratoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(EquipamentoContrato equipamentoContrato)
        {
            EquipamentoContrato equip = _context.EquipamentoContrato.SingleOrDefault(e => e.CodEquipContrato == equipamentoContrato.CodEquipContrato);
            if (equip != null)
            {
                _context.Entry(equip).CurrentValues.SetValues(equipamentoContrato);

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

        public void Criar(EquipamentoContrato equipamentoContrato)
        {
            try
            {
                _context.Add(equipamentoContrato);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            EquipamentoContrato equip = _context.EquipamentoContrato.SingleOrDefault(e => e.CodEquipContrato == codigo);

            if (equip != null)
            {
                _context.EquipamentoContrato.Remove(equip);

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

        public EquipamentoContrato ObterPorCodigo(int codigo)
        {
            return _context.EquipamentoContrato
                .Include(e => e.LocalAtendimento)
                .Include(e => e.Cliente)
                .Include(e => e.Contrato)
                .Include(e => e.Equipamento)
                .Include(e => e.GrupoEquipamento)
                .Include(e => e.TipoEquipamento)
                .Include(e => e.RegiaoAutorizada)
                .Include(e => e.RegiaoAutorizada.Filial)
                .Include(e => e.RegiaoAutorizada.Autorizada)
                .Include(e => e.RegiaoAutorizada.Regiao)
                .SingleOrDefault(e => e.CodEquipContrato == codigo);
        }

        public PagedList<EquipamentoContrato> ObterPorParametros(EquipamentoContratoParameters parameters)
        {
            var equips = _context.EquipamentoContrato
                .Include(e => e.LocalAtendimento)
                .Include(e => e.Cliente)
                .Include(e => e.Contrato)
                .Include(e => e.Equipamento)
                .Include(e => e.GrupoEquipamento)
                .Include(e => e.RegiaoAutorizada)
                .Include(e => e.RegiaoAutorizada.Filial)
                .Include(e => e.RegiaoAutorizada.Autorizada)
                .Include(e => e.RegiaoAutorizada.Regiao)
                .Include(e => e.TipoEquipamento)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                equips = equips.Where(
                    e =>
                    e.NumSerie.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    e.CodEquipContrato.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodEquipContrato != null)
            {
                equips = equips.Where(e => e.CodEquipContrato == parameters.CodEquipContrato);
            }

            if (parameters.CodPosto != null)
            {
                equips = equips.Where(e => e.CodPosto == parameters.CodPosto);
            }

            if (parameters.CodContrato != null)
            {
                equips = equips.Where(e => e.CodContrato == parameters.CodContrato);
            } 

            if (parameters.CodFilial != null)
            {
                equips = equips.Where(e => e.LocalAtendimento.CodFilial == parameters.CodFilial);
            }

            if (parameters.IndAtivo != null)
            {
                equips = equips.Where(e => e.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.CodFiliais != null)
            {
                var paramsSplit = parameters.CodFiliais.Split(',');
                paramsSplit = paramsSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var condicoes = string.Empty;

                for (int i = 0; i < paramsSplit.Length; i++)
                {
                    condicoes += string.Format("CodFilial={0}", paramsSplit[i]);
                    if (i < paramsSplit.Length - 1) condicoes += " Or ";
                }

                equips = equips.Where(condicoes);
            }

            if (parameters.CodEquipamentos != null)
            {
                var paramsSplit = parameters.CodEquipamentos.Split(',');
                paramsSplit = paramsSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var condicoes = string.Empty;

                for (int i = 0; i < paramsSplit.Length; i++)
                {
                    condicoes += string.Format("CodEquip={0}", paramsSplit[i]);
                    if (i < paramsSplit.Length - 1) condicoes += " Or ";
                }

                equips = equips.Where(condicoes);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                equips = equips.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
                equips = equips.OrderByDescending(e => e.IndAtivo.Equals(1));

            }

            var a = equips.ToQueryString();

            return PagedList<EquipamentoContrato>.ToPagedList(equips, parameters.PageNumber, parameters.PageSize);
        }
    }
}
