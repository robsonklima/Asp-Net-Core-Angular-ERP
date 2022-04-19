using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class InstalacaoMotivoResRepository : IInstalacaoMotivoResRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoMotivoResRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoMotivoRes instalacaoMotivoRes)
        {
            throw new System.NotImplementedException();
        }

        public void Criar(InstalacaoMotivoRes instalacaoMotivoRes)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public InstalacaoMotivoRes ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoMotivoRes.FirstOrDefault(f => f.CodInstalMotivoRes == codigo);
        }

        public PagedList<InstalacaoMotivoRes> ObterPorParametros(InstalacaoMotivoResParameters parameters)
        {
            var query = _context.InstalacaoMotivoRes
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodInstalMotivoRes.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.CodInstalMotivoRes != null)
            {
                query = query.Where(l => l.CodInstalMotivoRes == parameters.CodInstalMotivoRes);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoMotivoRes>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
