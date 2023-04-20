using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoContaRepository : ITecnicoContaRepository
    {
        private readonly AppDbContext _context;
        public TecnicoContaRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Atualizar(TecnicoConta conta)
        {
            _context.ChangeTracker.Clear();
            TecnicoConta c = _context.TecnicoConta.FirstOrDefault(c => c.CodTecnicoConta == conta.CodTecnicoConta);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(conta);
                _context.SaveChanges();
            }
        }

        public TecnicoConta Criar(TecnicoConta conta)
        {
            _context.Add(conta);
            _context.SaveChanges();
            return conta;
        }

        public void Deletar(int codigo)
        {
            TecnicoConta c = _context.TecnicoConta.FirstOrDefault(t => t.CodTecnico == codigo);

            if (c != null)
            {
                _context.TecnicoConta.Remove(c);
                _context.SaveChanges();
            }
        }

        public TecnicoConta ObterPorCodigo(int codigo)
        {
            return _context.TecnicoConta
                .FirstOrDefault(t => t.CodTecnicoConta == codigo);
        }

        public PagedList<TecnicoConta> ObterPorParametros(TecnicoContaParameters parameters)
        {
            var contas = _context.TecnicoConta.AsNoTracking().AsQueryable();

            if (parameters.CodTecnico != null) {
                contas = contas.Where(c => c.CodTecnico == parameters.CodTecnico);
            }

            return PagedList<TecnicoConta>.ToPagedList(contas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
