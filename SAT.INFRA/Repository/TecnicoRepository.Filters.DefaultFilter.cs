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
                query = query.Where(t => t.IndAtivo == parameters.IndAtivo && t.Usuario.IndAtivo == parameters.IndAtivo);

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
                var pas = parameters.PAS.Split(",").Select(a => a.Trim());
                query = query.Where(t => pas.Any(p => p == t.RegiaoAutorizada.PA.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodRegioes))
            {
                var regioes = parameters.CodRegioes.Split(",").Select(a => a.Trim());
                query = query.Where(t => regioes.Any(r => r == t.CodRegiao.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(",");
                query = query.Where(t => filiais.Any(a => a == t.CodFilial.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                var tecs = parameters.CodTecnicos.Split(",");
                query = query.Where(t => tecs.Any(a => a == t.CodTecnico.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodStatusServicos))
            {
                var codigosStatusServico = parameters.CodStatusServicos.Split(',').Select(a => a.Trim());
                query = query
                    .Include(t => t.OrdensServico.Where(os => codigosStatusServico.Contains(os.CodStatusServico.ToString())))
                        .ThenInclude(os => os.TipoIntervencao);
            }

            return query;
        }
    }
}
