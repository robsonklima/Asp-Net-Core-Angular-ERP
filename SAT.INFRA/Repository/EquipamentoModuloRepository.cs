using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class EquipamentoModuloRepository : IEquipamentoModuloRepository
    {
        private readonly AppDbContext _context;

        public EquipamentoModuloRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool ExisteEquipamentoModulo(EquipamentoModulo equipamentoModulo)
        {
            return _context.EquipamentoModulo.FirstOrDefault(a => a.CodECausa == equipamentoModulo.CodECausa && a.CodEquip == equipamentoModulo.CodEquip) != null;
        }

        public void Atualizar(EquipamentoModulo equipamentoModulo)
        {
            _context.ChangeTracker.Clear();
            EquipamentoModulo linhaTabela = _context.EquipamentoModulo.FirstOrDefault(a => a.CodECausa == equipamentoModulo.CodECausa && a.CodEquip == equipamentoModulo.CodEquip);

            if (linhaTabela != null)
            {
                equipamentoModulo.CodConfigEquipModulos = linhaTabela.CodConfigEquipModulos;
                try
                {
                    _context.Entry(linhaTabela).CurrentValues.SetValues(equipamentoModulo);
                    _context.Entry(linhaTabela).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(EquipamentoModulo equipamentoModulo)
        {
            try
            {
                _context.Add(equipamentoModulo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            EquipamentoModulo a = _context.EquipamentoModulo.SingleOrDefault();

            if (a != null)
            {
                try
                {
                    _context.EquipamentoModulo.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public EquipamentoModulo ObterPorCodigo(int codigo)
        {
            return _context.EquipamentoModulo
                .Include(i => i.Equipamento)
                    .ThenInclude(i => i.TipoEquipamento)
                .Include(i => i.Causa).Where(ca => ca.Causa.IndAtivo == 1)
                .SingleOrDefault(s => s.CodEquip == codigo);
        }

        public PagedList<EquipamentoModulo> ObterPorParametros(EquipamentoModuloParameters parameters)
        {
            var equipamentoModulo = _context.EquipamentoModulo
                .Include(i => i.Equipamento)
                    .ThenInclude(i => i.TipoEquipamento)
                .Include(i => i.Causa).Where(ca => ca.Causa.IndAtivo == 1)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                 equipamentoModulo = equipamentoModulo.Where(
                //     c =>
                //     c.Equipamento.NomeEquip.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                //     c.Causa.CodECausa.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                //     c.Equipamento.DescEquip.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                    em =>
                    em.Equipamento.NomeEquip.Contains(parameters.Filter)||
                    em.Causa.CodECausa.Contains(parameters.Filter) ||
                    em.Equipamento.DescEquip.Contains(parameters.Filter)

                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodECausa))
            {
                equipamentoModulo = equipamentoModulo.Where(w => w.CodECausa == parameters.CodECausa);
            }

            // if (parameters.CodEquips.HasValue)
            // {
            //     equipamentoModulo = equipamentoModulo.Where(w => w.CodEquip == parameters.CodEquip);
            // }

             if (!string.IsNullOrWhiteSpace(parameters.CodTipoEquips))
            {
                int[] cods = parameters.CodTipoEquips.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                equipamentoModulo = equipamentoModulo.Where(dc => cods.Contains(dc.Equipamento.CodTipoEquip));
            }

             if (!string.IsNullOrWhiteSpace(parameters.CodEquips))
            {
                int[] cods = parameters.CodEquips.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                equipamentoModulo = equipamentoModulo.Where(dc => cods.Contains(dc.Equipamento.CodEquip));
            }

            if (parameters.IndAtivo.HasValue)
                equipamentoModulo = equipamentoModulo.Where(e => e.IndAtivo == parameters.IndAtivo);

            return PagedList<EquipamentoModulo>.ToPagedList(equipamentoModulo, parameters.PageNumber, parameters.PageSize);
        }
    }
}
