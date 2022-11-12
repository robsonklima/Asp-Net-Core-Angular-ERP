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
    public class ItemXORCheckListRepository : IItemXORCheckListRepository
    {
        private readonly AppDbContext _context;

        public ItemXORCheckListRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ItemXORCheckList itemXORCheckList)
        {
            _context.ChangeTracker.Clear();
            ItemXORCheckList i = _context.ItemXORCheckList.FirstOrDefault(
                             i => i.CodItemChecklist == itemXORCheckList.CodItemChecklist);

            try
            {
                if (i != null)
                {
                    _context.Entry(i).CurrentValues.SetValues(itemXORCheckList);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Criar(ItemXORCheckList itemXORCheckList)
        {
            _context.Add(itemXORCheckList);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            ItemXORCheckList i = _context.ItemXORCheckList.FirstOrDefault(
                             i => i.CodItemChecklist == codigo);

            if (i != null)
            {
                _context.ItemXORCheckList.Remove(i);
                _context.SaveChanges();
            }
        }

        public ItemXORCheckList ObterPorCodigo(int codigo)
        {
            return _context.ItemXORCheckList
                .Include(i => i.ORItem)            
                .Include(i => i.ORCheckList)
                .Include(i => i.ORCheckListItem)
                .FirstOrDefault(i => i.CodItemChecklist == codigo);
        }

        public PagedList<ItemXORCheckList> ObterPorParametros(ItemXORCheckListParameters parameters)
        {
            var itemXORCheckLists = _context.ItemXORCheckList
                .Include(c => c.ORItem)            
                .Include(c => c.ORCheckList)
                .Include(i => i.ORCheckListItem)
                .AsQueryable();

            if (parameters.CodItemChecklist.HasValue)
            {
                itemXORCheckLists = itemXORCheckLists.Where(c => c.CodItemChecklist == parameters.CodItemChecklist);
            }  

            if (parameters.CodORItem.HasValue)
            {
                itemXORCheckLists = itemXORCheckLists.Where(c => c.CodORItem == parameters.CodORItem);
            }              

            if (parameters.CodORCheckList != null)
            {
                itemXORCheckLists = itemXORCheckLists.Where(c => c.CodORCheckList == parameters.CodORCheckList);
            }          

            if (parameters.IndAtivo != null)
            {
                itemXORCheckLists = itemXORCheckLists.Where(c => c.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                itemXORCheckLists = itemXORCheckLists.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ItemXORCheckList>.ToPagedList(itemXORCheckLists, parameters.PageNumber, parameters.PageSize);
        }
    }
}
