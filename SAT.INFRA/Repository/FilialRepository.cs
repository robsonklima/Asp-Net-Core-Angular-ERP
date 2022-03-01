using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class FilialRepository : IFilialRepository
    {
        private readonly AppDbContext _context;

        public FilialRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Filial filial)
        {
            throw new System.NotImplementedException();
        }

        public void Criar(Filial filial)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public Filial ObterPorCodigo(int codigo)
        {
            return _context.Filial.FirstOrDefault(f => f.CodFilial == codigo);
        }

        public PagedList<Filial> ObterPorParametros(FilialParameters parameters)
        {
            var query = _context.Filial.AsNoTracking().AsQueryable();

            query = AplicarIncludes(query, parameters.Include);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return PagedList<Filial>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
