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
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
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
            catch (Exception ex)
            {
                throw new Exception($"", ex);
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
            var query = _context.PlantaoTecnico
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
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.Filial)
                .Include(p => p.Tecnico)
                    .ThenInclude(t => t.Regiao)
                .AsQueryable();

            if (parameters.CodTecnico != null)
            {
                query = query.Where(p => p.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.IndAtivo != null)
            {
                query = query.Where(p => p.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.Nome != null)
            {
                query = query.Where(p => p.Tecnico.Nome == parameters.Nome);
            }

            if (parameters.DataPlantaoInicio.HasValue && parameters.DataPlantaoFim.HasValue)
                query = query.Where(p => p.DataPlantao.Date >= parameters.DataPlantaoInicio.Value.Date && p.DataPlantao.Date <= parameters.DataPlantaoFim.Value.Date);

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodPlantaoTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Tecnico.Nome.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Tecnico.Filial.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            return PagedList<PlantaoTecnico>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
