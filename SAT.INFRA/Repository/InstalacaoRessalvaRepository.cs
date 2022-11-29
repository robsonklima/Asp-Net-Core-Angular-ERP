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
    public partial class InstalacaoRessalvaRepository : IInstalacaoRessalvaRepository
    {
        private readonly AppDbContext _context;
        private readonly ISequenciaRepository _sequenciaRepository;

        public InstalacaoRessalvaRepository(AppDbContext context, ISequenciaRepository sequenciaRepository)
        {
            _context = context;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(InstalacaoRessalva instalRessalva)
        {
            try
            {
                _context.ChangeTracker.Clear();
                InstalacaoRessalva ir = _context.InstalacaoRessalva.FirstOrDefault(i => i.CodInstalRessalva == instalRessalva.CodInstalRessalva);

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

        public void Criar(InstalacaoRessalva instalRessalva)
        {
            instalRessalva.CodInstalRessalva = this._sequenciaRepository.ObterContador("InstalRessalva");
            _context.Add(instalRessalva);
            _context.SaveChanges();
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
                .Include(i => i.InstalacaoMotivoRes)
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
