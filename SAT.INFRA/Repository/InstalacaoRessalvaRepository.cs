using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class InstalacaoRessalvaRepository : IInstalacaoRessalvaRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoRessalvaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoRessalva instalRessalva)
        {
            throw new System.NotImplementedException();
        }

        public void Criar(InstalacaoRessalva instalRessalva)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public InstalacaoRessalva ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoRessalva.FirstOrDefault(f => f.CodInstalRessalva == codigo);
        }

        public PagedList<InstalacaoRessalva> ObterPorParametros(InstalacaoRessalvaParameters parameters)
        {
            var query = _context.InstalacaoRessalva
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodInstalRessalva.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.CodInstalacao != null)
            {
                query = query.Where(l => l.CodInstalacao == parameters.CodInstalacao);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoRessalva>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
