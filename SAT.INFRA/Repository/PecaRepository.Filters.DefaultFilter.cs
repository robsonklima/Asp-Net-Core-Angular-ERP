using SAT.MODELS.Entities;
using System.Linq;
using System;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class PecaRepository : IPecaRepository
    {
        public IQueryable<Peca> AplicarFiltroPadrao(IQueryable<Peca> query, PecaParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                query = query.Where(p =>
                    p.CodPeca.ToString().Contains(parameters.Filter) ||
                    p.CodMagnus.Contains(parameters.Filter) ||
                    p.NomePeca.Contains(parameters.Filter)
                );
            }

            if (!string.IsNullOrEmpty(parameters.CodPeca))
            {
                int[] split = parameters.CodPeca.Split(",").Select(s => int.Parse(s)).ToArray();
                query = query.Where(p => split.Any(s => s.Equals(p.CodPeca)));
            }

            if (!string.IsNullOrEmpty(parameters.CodMagnus))
            {
                query = query.Where(p => p.CodPeca == Convert.ToInt32(parameters.CodPeca));
            }

            return query;
        }
    }
}
