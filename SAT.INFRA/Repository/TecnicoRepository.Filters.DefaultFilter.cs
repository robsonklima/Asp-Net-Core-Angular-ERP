using SAT.MODELS.Entities;
using System.Linq;
using System;
using SAT.INFRA.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoRepository : ITecnicoRepository
    {
        public IQueryable<Tecnico> AplicarFiltroPadrao(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                query = query.Where(t =>
                    t.CodTecnico.ToString().Contains(parameters.Filter) ||
                    t.Nome.Contains(parameters.Filter) ||
                    t.Regiao.NomeRegiao.Contains(parameters.Filter) ||
                    t.Autorizada.NomeFantasia.Contains(parameters.Filter) ||
                    t.Filial.NomeFilial.Contains(parameters.Filter));

            if (parameters.IndAtivo.HasValue)
                query = query.Where(t => t.IndAtivo == parameters.IndAtivo && (t.Usuario == null || t.Usuario.IndAtivo == parameters.IndAtivo));

            if (parameters.IndFerias.HasValue)
                query = query.Where(t => t.IndFerias == parameters.IndFerias);

            if (parameters.CodTecnico.HasValue)
                query = query.Where(t => t.CodTecnico == parameters.CodTecnico);

            if (parameters.CodPerfil.HasValue)
                query = query.Where(t => t.Usuario.CodPerfil == parameters.CodPerfil);

            if (!string.IsNullOrEmpty(parameters.Nome))
                query = query.Where(t => t.Nome == parameters.Nome);

            if (parameters.CodAutorizada.HasValue)
                query = query.Where(t => t.CodAutorizada == parameters.CodAutorizada);

            if (!string.IsNullOrEmpty(parameters.PAS))
            {
                int[] pas = parameters.PAS.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(t => pas.Contains(t.IndPA.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodRegioes))
            {
                int[] regioes = parameters.CodRegioes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(t => regioes.Contains(t.CodRegiao.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                int[] filiais = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(t => filiais.Contains(t.CodFilial.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                int[] tecs = parameters.CodTecnicos.Split(",").Select(a => int.Parse(a.Trim())).Where(s => s > 0).Distinct().ToArray();
                query = query.Where(t => t.CodTecnico != null && tecs.Contains(t.CodTecnico.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodStatusServicos))
            {
                int[] codigosStatusServico = parameters.CodStatusServicos.Split(',').Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query
                    .Include(t => t.OrdensServico.Where(os => codigosStatusServico.Contains(os.CodStatusServico)))
                        .ThenInclude(os => os.TipoIntervencao);
            }

            return query;
        }
    }
}
