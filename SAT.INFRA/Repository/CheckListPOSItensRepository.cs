using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class CheckListPOSItensRepository : ICheckListPOSItensRepository
    {
        private readonly AppDbContext _context;

        public CheckListPOSItensRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(CheckListPOSItens checkListPOSItens)
        {
            _context.ChangeTracker.Clear();
            CheckListPOSItens aud = _context.CheckListPOSItens
                .FirstOrDefault(aud => aud.CodCheckListPOSItens == checkListPOSItens.CodCheckListPOSItens);
            try
            {
                if (aud != null)
                {
                    _context.Entry(aud).CurrentValues.SetValues(checkListPOSItens);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(CheckListPOSItens checkListPOSItens)
        {
            try
            {
                _context.Add(checkListPOSItens);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codCheckListPOSItens)
        {
            CheckListPOSItens aud = _context.CheckListPOSItens
                .FirstOrDefault(aud => aud.CodCheckListPOSItens == codCheckListPOSItens);

            if (aud != null)
            {
                _context.CheckListPOSItens.Remove(aud);
                _context.SaveChanges();
            }
        }

        public CheckListPOSItens ObterPorCodigo(int codCheckListPOSItens)
        {
            try
            {
                return _context.CheckListPOSItens
                    .Include(c => c.Cliente)
                    .SingleOrDefault(aud => aud.CodCheckListPOSItens == codCheckListPOSItens);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a checkListPOSItens {ex.Message}");
            }
        }

        public PagedList<CheckListPOSItens> ObterPorParametros(CheckListPOSItensParameters parameters)
        {
            var checkListPOSItenss = _context.CheckListPOSItens
                .Include(c => c.Cliente)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                checkListPOSItenss = checkListPOSItenss.Where(a =>
                    a.CodCheckListPOSItens.ToString().Contains(parameters.Filter));
            }

            if (parameters.CodCliente.HasValue)
                checkListPOSItenss = checkListPOSItenss.Where(c => c.CodCliente == parameters.CodCliente);

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                checkListPOSItenss = checkListPOSItenss.Where(dc => cods.Contains(dc.Cliente.CodCliente));
            };

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                checkListPOSItenss = checkListPOSItenss.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<CheckListPOSItens>.ToPagedList(checkListPOSItenss, parameters.PageNumber, parameters.PageSize);
        }

    }
}
