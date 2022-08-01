using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq;
using System;
using SAT.INFRA.Interfaces;
using Microsoft.EntityFrameworkCore;
using SAT.MODELS.Entities.Constants;

namespace SAT.INFRA.Repository
{
    public partial class FilialRepository : IFilialRepository
    {
        public IQueryable<Filial> AplicarFiltroPadrao(IQueryable<Filial> query, FilialParameters parameters)
        {
            if (parameters.Filter != null)
            {
                query = query.Where(
                        f =>
                        f.CodFilial.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                        f.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodFilial != null)
            {
                query = query.Where(f => f.CodFilial == parameters.CodFilial);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(dc => cods.Contains(dc.CodFilial));
            }

            if (parameters.CodFiliais != null)
            {
                string[] paramsSplit = parameters.CodFiliais.Split(',');

                paramsSplit = paramsSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var condicoes = string.Empty;

                for (int i = 0; i < paramsSplit.Length; i++)
                {
                    condicoes += string.Format("CodFilial={0}", paramsSplit[i]);
                    if (i < paramsSplit.Length - 1) condicoes += " Or ";
                }
            }

            if (parameters.IndAtivo != null)
            {
                query = query.Where(f => f.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SiglaUF))
            {
                query.Include(i => i.Cidade)
                        .Include(i => i.Cidade.UnidadeFederativa);

                query = query.Where(f => f.Cidade.UnidadeFederativa.SiglaUF == parameters.SiglaUF);
            }

            query = query.Where(f => f.CodFilial != Constants.PERTO_INDIA);

            return query;
        }
    }
}
