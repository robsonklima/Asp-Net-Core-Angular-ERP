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
    public partial class InstalacaoMotivoMultaRepository : IInstalacaoMotivoMultaRepository
    {
        private readonly AppDbContext _context;
        private readonly ISequenciaRepository _sequenciaRepository;

        public InstalacaoMotivoMultaRepository(AppDbContext context, ISequenciaRepository sequenciaRepository)
        {
            _context = context;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(InstalacaoMotivoMulta instalMotivoMulta)
        {
            try
            {
                _context.ChangeTracker.Clear();
                InstalacaoMotivoMulta ir = _context.InstalacaoMotivoMulta.FirstOrDefault(i => i.CodInstalMotivoMulta == instalMotivoMulta.CodInstalMotivoMulta);

                if (ir != null)
                {
                    _context.Entry(ir).CurrentValues.SetValues(instalMotivoMulta);
                    _context.Entry(ir).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Criar(InstalacaoMotivoMulta instalMotivoMulta)
        {
            instalMotivoMulta.CodInstalMotivoMulta = this._sequenciaRepository.ObterContador("InstalMotivoMulta");
            _context.Add(instalMotivoMulta);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public InstalacaoMotivoMulta ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoMotivoMulta.FirstOrDefault(f => f.CodInstalMotivoMulta == codigo);
        }

        public PagedList<InstalacaoMotivoMulta> ObterPorParametros(InstalacaoMotivoMultaParameters parameters)
        {
            var query = _context.InstalacaoMotivoMulta
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodInstalMotivoMulta.ToString().Contains(parameters.Filter) ||
                    p.NomeMotivoMulta.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.DescMotivoMulta.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodInstalMotivoMulta != null)
            {
                query = query.Where(l => l.CodInstalMotivoMulta == parameters.CodInstalMotivoMulta);
            }

            if (parameters.IndAtivo != null)
            {
                query = query.Where(a => a.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoMotivoMulta>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
