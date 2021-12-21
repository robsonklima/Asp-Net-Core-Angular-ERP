using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public partial class RegiaoAutorizadaRepository : IRegiaoAutorizadaRepository
    {
        public IQueryable<RegiaoAutorizada> AplicarFiltros(IQueryable<RegiaoAutorizada> query, RegiaoAutorizadaParameters parameters)
        {
            if (parameters.CodRegiao != null)
            {
                query = query.Where(r => r.CodRegiao == parameters.CodRegiao);
            }

            if (parameters.CodAutorizada != null)
            {
                query = query.Where(r => r.CodAutorizada == parameters.CodAutorizada);
            }


            if (parameters.IndAtivo != null)
            {
                query = query.Where(r => r.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(",").Select(s => int.Parse(s.Trim())).ToArray();
                query = query.Where(ag => filiais.Contains(ag.Filial.CodFilial));
            }

            if (parameters.Filter != null)
            {
                query = query.Where(
                    ra => ra.CodAutorizada.ToString().Equals(parameters.Filter) ||
                          ra.Autorizada.NomeFantasia.Contains(parameters.Filter) ||
                          ra.Regiao.NomeRegiao.Contains(parameters.Filter) ||
                          ra.Filial.NomeFilial.Contains(parameters.Filter)
                );
            }

            return query;
        }
    }
}