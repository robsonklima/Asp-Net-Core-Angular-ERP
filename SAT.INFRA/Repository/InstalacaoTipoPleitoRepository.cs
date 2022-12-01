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
    public partial class InstalacaoTipoPleitoRepository : IInstalacaoTipoPleitoRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoTipoPleitoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoTipoPleito instalacaoTipoPleito)
        {
            _context.ChangeTracker.Clear();
            InstalacaoTipoPleito inst = _context.InstalacaoTipoPleito.FirstOrDefault(i => i.CodInstalTipoPleito == instalacaoTipoPleito.CodInstalTipoPleito);

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacaoTipoPleito);
                _context.SaveChanges();
            }
        }

        public void Criar(InstalacaoTipoPleito instalacaoTipoPleito)
        {
            _context.Add(instalacaoTipoPleito);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            InstalacaoTipoPleito inst = _context.InstalacaoTipoPleito.FirstOrDefault(i => i.CodInstalTipoPleito == codigo);

            if (inst != null)
            {
                _context.InstalacaoTipoPleito.Remove(inst);
                _context.SaveChanges();
            }
        }

        public InstalacaoTipoPleito ObterPorCodigo(int codigo)
        {

            var data = _context.InstalacaoTipoPleito.FirstOrDefault(i => i.CodInstalTipoPleito == codigo);

            return data;
        }

        public PagedList<InstalacaoTipoPleito> ObterPorParametros(InstalacaoTipoPleitoParameters parameters)
        {
            var instalacoes = _context.InstalacaoTipoPleito
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalacoes = instalacoes.Where(p =>
                    p.CodInstalTipoPleito.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.CodInstalTipoPleito.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalTipoPleito == parameters.CodInstalTipoPleito);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalacoes = instalacoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoTipoPleito>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
