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
    public partial class InstalacaoPleitoRepository : IInstalacaoPleitoRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoPleitoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoPleito instalacaoPleito)
        {
            _context.ChangeTracker.Clear();
            InstalacaoPleito inst = _context.InstalacaoPleito.FirstOrDefault(i => i.CodInstalPleito == instalacaoPleito.CodInstalPleito);

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacaoPleito);
                _context.SaveChanges();
            }
        }

        public void Criar(InstalacaoPleito instalacaoPleito)
        {
            _context.Add(instalacaoPleito);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            InstalacaoPleito inst = _context.InstalacaoPleito.FirstOrDefault(i => i.CodInstalPleito == codigo);

            if (inst != null)
            {
                _context.InstalacaoPleito.Remove(inst);
                _context.SaveChanges();
            }
        }

        public InstalacaoPleito ObterPorCodigo(int codigo)
        {

            var data = _context.InstalacaoPleito.FirstOrDefault(i => i.CodInstalPleito == codigo);

            return data;
        }

        public PagedList<InstalacaoPleito> ObterPorParametros(InstalacaoPleitoParameters parameters)
        {
            var instalacoes = _context.InstalacaoPleito
                .Include(i => i.Contrato)
                .Include(i => i.InstalacaoTipoPleito)
                .Include(i => i.InstalacaoPleitoInstal)
                .AsNoTracking() 
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalacoes = instalacoes.Where(i =>
                    i.CodInstalPleito.ToString().Contains(parameters.Filter) ||
                    i.CodInstalTipoPleito .ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    i.CodContrato.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)                 
                );
            }

            if (parameters.CodContrato.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodInstalPleito.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalPleito == parameters.CodInstalPleito);
            }

            if (parameters.CodInstalTipoPleito.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalTipoPleito == parameters.CodInstalTipoPleito);
            }       

            if (!string.IsNullOrWhiteSpace(parameters.CodContratos))
            {
                int[] cods = parameters.CodContratos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                instalacoes = instalacoes.Where(i => cods.Contains(i.CodContrato));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodInstalTipoPleitos))
            {
                int[] cods = parameters.CodInstalTipoPleitos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                instalacoes = instalacoes.Where(i => cods.Contains(i.InstalacaoTipoPleito.CodInstalTipoPleito));
            }


            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalacoes = instalacoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoPleito>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
