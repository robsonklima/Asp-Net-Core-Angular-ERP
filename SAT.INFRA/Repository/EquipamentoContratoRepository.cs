using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
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
            _context.ChangeTracker.Clear();
            EquipamentoContrato equip = _context.EquipamentoContrato.SingleOrDefault(e => e.CodEquipContrato == equipamentoContrato.CodEquipContrato);

            if (equip != null)
            {
                try
                {
                    _context.Entry(equip).CurrentValues.SetValues(equipamentoContrato);
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
                   .ThenInclude(e => e.TipoContrato)
                .Include(e => e.Equipamento)
                    .ThenInclude(e => e.Equivalencia)
                .Include(e => e.ContratoEquipamento)
                .Include(e => e.GrupoEquipamento)
                .Include(e => e.RegiaoAutorizada)
                .Include(e => e.RegiaoAutorizada.Filial)
                .Include(e => e.RegiaoAutorizada.Autorizada)
                .Include(e => e.RegiaoAutorizada.Regiao)
                .Include(e => e.TipoEquipamento)
                .Include(e => e.ContratoServico)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                equips = equips.Where(e =>
                    e.NumSerie.Contains(parameters.Filter) ||
                    e.CodEquipContrato.ToString().Contains(parameters.Filter));

            if (parameters.CodEquipContrato.HasValue)
                equips = equips.Where(e => e.CodEquipContrato == parameters.CodEquipContrato);

            if (parameters.CodPosto.HasValue)
                equips = equips.Where(e => e.CodPosto == parameters.CodPosto);

            if (parameters.CodContrato.HasValue)
                equips = equips.Where(e => e.CodContrato == parameters.CodContrato);

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                equips = equips.Where(os => cods.Contains(os.CodCliente));
            }

            if (!string.IsNullOrEmpty(parameters.NumSerie))
                equips = equips.Where(e => e.NumSerie == parameters.NumSerie);

            if (parameters.CodFilial.HasValue)
                equips = equips.Where(e => e.LocalAtendimento.CodFilial == parameters.CodFilial);

            if (parameters.IndAtivo.HasValue)
                equips = equips.Where(e => e.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(',').Select(f => f.Trim());
                equips = equips.Where(e => filiais.Any(p => p == e.CodFilial.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodEquipamentos))
            {
                var codigos = parameters.CodEquipamentos.Split(',').Select(f => f.Trim());
                equips = equips.Where(e => codigos.Any(p => p == e.CodEquip.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                equips = equips.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<EquipamentoContrato>.ToPagedList(equips, parameters.PageNumber, parameters.PageSize);
        }
    }
}
