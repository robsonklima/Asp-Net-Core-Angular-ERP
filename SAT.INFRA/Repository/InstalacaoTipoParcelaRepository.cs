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
    public partial class InstalacaoTipoParcelaRepository : IInstalacaoTipoParcelaRepository
    {
        private readonly AppDbContext _context;
        private readonly ISequenciaRepository _sequenciaRepository;

        public InstalacaoTipoParcelaRepository(AppDbContext context, ISequenciaRepository sequenciaRepository)
        {
            _context = context;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(InstalacaoTipoParcela instalTipoParcela)
        {
            try
            {
                _context.ChangeTracker.Clear();
                InstalacaoTipoParcela ir = _context.InstalacaoTipoParcela.FirstOrDefault(i => i.CodInstalTipoParcela == instalTipoParcela.CodInstalTipoParcela);

                if (ir != null)
                {
                    _context.Entry(ir).CurrentValues.SetValues(instalTipoParcela);
                    _context.Entry(ir).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Criar(InstalacaoTipoParcela instalTipoParcela)
        {
            instalTipoParcela.CodInstalTipoParcela = this._sequenciaRepository.ObterContador("InstalTipoParcela");
            _context.Add(instalTipoParcela);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public InstalacaoTipoParcela ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoTipoParcela.FirstOrDefault(f => f.CodInstalTipoParcela == codigo);
        }

        public PagedList<InstalacaoTipoParcela> ObterPorParametros(InstalacaoTipoParcelaParameters parameters)
        {
            var query = _context.InstalacaoTipoParcela
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodInstalTipoParcela.ToString().Contains(parameters.Filter) ||
                    p.NomeTipoParcela.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodInstalTipoParcela != null)
            {
                query = query.Where(l => l.CodInstalTipoParcela == parameters.CodInstalTipoParcela);
            }

            if (parameters.IndAtivo != null)
            {
                query = query.Where(a => a.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoTipoParcela>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
