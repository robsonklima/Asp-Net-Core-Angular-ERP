using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class EquipamentoPOSRepository : IEquipamentoPOSRepository
    {
        private readonly AppDbContext _context;
        public EquipamentoPOSRepository(AppDbContext context)
        {
            this._context = context;
        }

        public EquipamentoPOS Atualizar(EquipamentoPOS e)
        {
            _context.ChangeTracker.Clear();
            EquipamentoPOS t = _context.EquipamentoPOS.FirstOrDefault(t => t.CodEquipamentoPOS == e.CodEquipamentoPOS);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(e);
                _context.SaveChanges();
            }

            return t;
        }

        public EquipamentoPOS Criar(EquipamentoPOS e)
        {
            _context.Add(e);
            _context.SaveChanges();
            return e;
        }

        public EquipamentoPOS Deletar(int codEquipamentoPOS)
        {
            EquipamentoPOS t = _context.EquipamentoPOS.FirstOrDefault(t => t.CodEquipamentoPOS == codEquipamentoPOS);

            if (t != null)
            {
                _context.EquipamentoPOS.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public EquipamentoPOS ObterPorCodigo(int codigo)
        {
            return _context.EquipamentoPOS
                .FirstOrDefault(t => t.CodEquipamentoPOS == codigo);
        }

        public PagedList<EquipamentoPOS> ObterPorParametros(EquipamentoPOSParameters parameters)
        {
            var query = _context.EquipamentoPOS.AsQueryable();

            if (parameters.CodEquip.HasValue)
                query = query.Where(q => q.CodEquip == parameters.CodEquip);

            if (!string.IsNullOrWhiteSpace(parameters.NumSerie))
                query = query.Where(q => q.NumeroSerie == parameters.NumSerie);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<EquipamentoPOS>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
