using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PlantaoTecnicoRepository : IPlantaoTecnicoRepository
    {
        private readonly AppDbContext _context;

        public PlantaoTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PlantaoTecnico plantao)
        {
            _context.ChangeTracker.Clear();
            PlantaoTecnico per = _context.PlantaoTecnico.SingleOrDefault(p => p.CodPlantaoTecnico == plantao.CodPlantaoTecnico);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(plantao);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(PlantaoTecnico plantao)
        {
            try
            {
                _context.Add(plantao);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            PlantaoTecnico per = _context.PlantaoTecnico.SingleOrDefault(p => p.CodPlantaoTecnico == codigo);

            if (per != null)
            {
                _context.PlantaoTecnico.Remove(per);
                _context.SaveChanges();
            }
        }

        public PlantaoTecnico ObterPorCodigo(int codigo)
        {
            return _context.PlantaoTecnico
                .Include(p => p.PlantaoRegioes)
                    .ThenInclude(t => t.Regiao)
                .Include(p => p.PlantaoClientes)
                    .ThenInclude(t => t.Cliente)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.Veiculos)
                        .ThenInclude(t => t.Combustivel)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.RegiaoAutorizada)
                        .ThenInclude(r => r.Autorizada)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.RegiaoAutorizada)
                        .ThenInclude(r => r.Regiao)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.Usuario)
                .SingleOrDefault(p => p.CodPlantaoTecnico == codigo);
        }

        public PagedList<PlantaoTecnico> ObterPorParametros(PlantaoTecnicoParameters parameters)
        {
            var perfis = _context.PlantaoTecnico
                .Include(p => p.PlantaoRegioes)
                    .ThenInclude(t => t.Regiao)
                .Include(p => p.PlantaoClientes)
                    .ThenInclude(t => t.Cliente)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.Veiculos)
                        .ThenInclude(t => t.Combustivel)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.RegiaoAutorizada)
                        .ThenInclude(r => r.Autorizada)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.RegiaoAutorizada)
                        .ThenInclude(r => r.Regiao)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.Usuario)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodPlantaoTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Tecnico.Nome.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodTecnico != null)
            {
                perfis = perfis.Where(p => p.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.Nome != null)
            {
                perfis = perfis.Where(p => p.Tecnico.Nome == parameters.Nome);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<PlantaoTecnico>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
