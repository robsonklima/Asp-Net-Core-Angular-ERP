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
    public partial class InstalacaoLoteRepository : IInstalacaoLoteRepository
    {
        private readonly AppDbContext _context;
        private readonly ISequenciaRepository _sequenciaRepository;

        public InstalacaoLoteRepository(AppDbContext context, ISequenciaRepository sequenciaRepository)
        {
            _context = context;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(InstalacaoLote instalacaoLote)
        {
            try
            {
                _context.ChangeTracker.Clear();
                InstalacaoLote ir = _context.InstalacaoLote.FirstOrDefault(i => i.CodInstalLote == instalacaoLote.CodInstalLote);

                if (ir != null)
                {
                    _context.Entry(ir).CurrentValues.SetValues(instalacaoLote);
                    _context.Entry(ir).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        public void Criar(InstalacaoLote instalacaoLote)
        {
            instalacaoLote.CodInstalLote = this._sequenciaRepository.ObterContador("InstalLote");
            _context.Add(instalacaoLote);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public InstalacaoLote ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoLote.FirstOrDefault(f => f.CodInstalLote == codigo);
        }

        public PagedList<InstalacaoLote> ObterPorParametros(InstalacaoLoteParameters parameters)
        {
            var query = _context.InstalacaoLote
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodInstalLote.ToString().Contains(parameters.Filter) ||
                    p.NomeLote.Contains(parameters.Filter) ||
                    p.DescLote.Contains(parameters.Filter) ||
                    p.DataRecLote.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.CodInstalLote != null)
            {
                query = query.Where(l => l.CodInstalLote == parameters.CodInstalLote);
            }

            if (parameters.CodContrato != null)
            {
                query = query.Where(l => l.CodContrato == parameters.CodContrato);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoLote>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
