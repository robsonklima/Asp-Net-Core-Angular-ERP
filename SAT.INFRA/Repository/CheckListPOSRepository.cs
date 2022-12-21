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
    public class CheckListPOSRepository : ICheckListPOSRepository
    {
        private readonly AppDbContext _context;

        public CheckListPOSRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(CheckListPOS checkListPOS)
        {
            _context.ChangeTracker.Clear();
            CheckListPOS aud = _context.CheckListPOS
                .FirstOrDefault(aud => aud.CodCheckListPOS == checkListPOS.CodCheckListPOS);
            try
            {
                if (aud != null)
                {
                    _context.Entry(aud).CurrentValues.SetValues(checkListPOS);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(CheckListPOS checkListPOS)
        {
            try
            {
                _context.Add(checkListPOS);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codCheckListPOS)
        {
            CheckListPOS aud = _context.CheckListPOS
                .FirstOrDefault(aud => aud.CodCheckListPOS == codCheckListPOS);

            if (aud != null)
            {
                _context.CheckListPOS.Remove(aud);
                _context.SaveChanges();
            }
        }

        public CheckListPOS ObterPorCodigo(int codCheckListPOS)
        {
            try
            {
                return _context.CheckListPOS
                    .Include(c => c.CheckListPOSItens)
                    .SingleOrDefault(aud => aud.CodCheckListPOS == codCheckListPOS);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a checkListPOS {ex.Message}");
            }
        }

        public PagedList<CheckListPOS> ObterPorParametros(CheckListPOSParameters parameters)
        {
            var checkListPOSs = _context.CheckListPOS
                .Include(c => c.CheckListPOSItens)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                checkListPOSs = checkListPOSs.Where(a =>
                    a.CodCheckListPOS.ToString().Contains(parameters.Filter));
            }

            if (parameters.CodOS.HasValue)
                checkListPOSs = checkListPOSs.Where(c => c.CodOS == parameters.CodOS);

            if (parameters.CodRAT.HasValue)
                checkListPOSs = checkListPOSs.Where(c => c.CodRAT == parameters.CodRAT);

            if (!string.IsNullOrWhiteSpace(parameters.CodCheckListPOSItens))
            {
                int[] cods = parameters.CodCheckListPOSItens.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                checkListPOSs = checkListPOSs.Where(dc => cods.Contains(dc.CheckListPOSItens.CodCheckListPOSItens));
            };

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                checkListPOSs = checkListPOSs.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<CheckListPOS>.ToPagedList(checkListPOSs, parameters.PageNumber, parameters.PageSize);
        }

    }
}
