using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class InstalacaoStatusRepository : IInstalacaoStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly ISequenciaRepository _sequenciaRepository;

        public InstalacaoStatusRepository(AppDbContext context, ISequenciaRepository sequenciaRepository)
        {
            _context = context;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(InstalacaoStatus instalRessalva)
        {
            try
            {
                _context.ChangeTracker.Clear();
                InstalacaoStatus ir = _context.InstalacaoStatus.FirstOrDefault(i => i.CodInstalStatus == instalRessalva.CodInstalStatus);

                if (ir != null)
                {
                    _context.Entry(ir).CurrentValues.SetValues(instalRessalva);
                    _context.Entry(ir).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Criar(InstalacaoStatus instalRessalva)
        {
            instalRessalva.CodInstalStatus = this._sequenciaRepository.ObterContador("InstalRessalva");
            _context.Add(instalRessalva);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public InstalacaoStatus ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoStatus.FirstOrDefault(f => f.CodInstalStatus == codigo);
        }

        public PagedList<InstalacaoStatus> ObterPorParametros(InstalacaoStatusParameters parameters)
        {
            var query = _context.InstalacaoStatus
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(s =>
                    s.CodInstalStatus.ToString().Contains(parameters.Filter) ||
                    s.NomeInstalStatus.Contains(parameters.Filter)
                );
            }

            if(!string.IsNullOrWhiteSpace(parameters.NomeInstalStatus))
            {
                query = query.Where(s => s.NomeInstalStatus.Contains(parameters.NomeInstalStatus));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoStatus>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
