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
    public class PlantaoTecnicoClienteRepository : IPlantaoTecnicoClienteRepository
    {
        private readonly AppDbContext _context;

        public PlantaoTecnicoClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PlantaoTecnicoCliente cliente)
        {
            _context.ChangeTracker.Clear();
            PlantaoTecnicoCliente per = _context.PlantaoTecnicoCliente.SingleOrDefault(p => p.CodPlantaoTecnicoCliente == cliente.CodPlantaoTecnicoCliente);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(cliente);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(PlantaoTecnicoCliente cliente)
        {
            try
            {
                _context.Add(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            PlantaoTecnicoCliente per = _context.PlantaoTecnicoCliente.SingleOrDefault(p => p.CodPlantaoTecnicoCliente == codigo);

            if (per != null)
            {
                _context.PlantaoTecnicoCliente.Remove(per);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public PlantaoTecnicoCliente ObterPorCodigo(int codigo)
        {
            return _context.PlantaoTecnicoCliente.SingleOrDefault(p => p.CodPlantaoTecnicoCliente == codigo);
        }

        public PagedList<PlantaoTecnicoCliente> ObterPorParametros(PlantaoTecnicoClienteParameters parameters)
        {
            var perfis = _context.PlantaoTecnicoCliente
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodPlantaoTecnicoCliente.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<PlantaoTecnicoCliente>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
