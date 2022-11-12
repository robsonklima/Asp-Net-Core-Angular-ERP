using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ORSolucaoRepository : IORSolucaoRepository
    {
        private readonly AppDbContext _context;

        public ORSolucaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORSolucao orSolucao)
        {
            _context.ChangeTracker.Clear();
            ORSolucao p = _context.ORSolucao.FirstOrDefault(p => p.CodSolucao == orSolucao.CodSolucao);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(orSolucao);
                _context.SaveChanges();
            }
        }

        public ORSolucao Criar(ORSolucao orSolucao)
        {
            _context.Add(orSolucao);
            _context.SaveChanges();
            return orSolucao;
        }

        public void Deletar(int codigo)
        {
            ORSolucao ORSolucao = _context.ORSolucao.FirstOrDefault(p => p.CodSolucao == codigo);

            if (ORSolucao != null)
            {
                _context.ORSolucao.Remove(ORSolucao);
                _context.SaveChanges();
            }
        }

        public ORSolucao ObterPorCodigo(int codigo)
        {
            return _context.ORSolucao
                .FirstOrDefault(p => p.CodSolucao == codigo);
        }

        public PagedList<ORSolucao> ObterPorParametros(ORSolucaoParameters parameters)
        {
            var query = _context.ORSolucao
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodSolucao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) 
                    ||
                    p.Descricao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodSolucao.HasValue)
            {
                query = query.Where(q => q.CodSolucao == parameters.CodSolucao);
            }

            if(parameters.IndAtivo.HasValue)
            {
                query = query.Where(q => q.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ORSolucao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
