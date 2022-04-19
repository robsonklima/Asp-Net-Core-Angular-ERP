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
    public class PlantaoTecnicoRegiaoRepository : IPlantaoTecnicoRegiaoRepository
    {
        private readonly AppDbContext _context;

        public PlantaoTecnicoRegiaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PlantaoTecnicoRegiao regiao)
        {
            _context.ChangeTracker.Clear();
            PlantaoTecnicoRegiao per = _context.PlantaoTecnicoRegiao.SingleOrDefault(p => p.CodPlantaoTecnicoRegiao == regiao.CodPlantaoTecnicoRegiao);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(regiao);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(PlantaoTecnicoRegiao regiao)
        {
            try
            {
                _context.Add(regiao);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            PlantaoTecnicoRegiao per = _context.PlantaoTecnicoRegiao.SingleOrDefault(p => p.CodPlantaoTecnicoRegiao == codigo);

            if (per != null)
            {
                _context.PlantaoTecnicoRegiao.Remove(per);

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

        public PlantaoTecnicoRegiao ObterPorCodigo(int codigo)
        {
            return _context.PlantaoTecnicoRegiao.SingleOrDefault(p => p.CodPlantaoTecnicoRegiao == codigo);
        }

        public PagedList<PlantaoTecnicoRegiao> ObterPorParametros(PlantaoTecnicoRegiaoParameters parameters)
        {
            var perfis = _context.PlantaoTecnicoRegiao
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodPlantaoTecnicoRegiao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<PlantaoTecnicoRegiao>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
