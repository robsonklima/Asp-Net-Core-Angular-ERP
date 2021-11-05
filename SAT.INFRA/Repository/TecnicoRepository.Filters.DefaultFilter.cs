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
            if (parameters.Filter != null)
            {
                query = query.Where(
                    t =>
                    t.CodTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Nome.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Regiao.NomeRegiao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Autorizada.NomeFantasia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Filial.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.IndAtivo != null)
            {
                query = query.Where(t => t.IndAtivo == parameters.IndAtivo && t.Usuario.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.IndFerias != null)
            {
                query = query.Where(t => t.IndFerias == parameters.IndFerias);
            }

            if (parameters.CodTecnico != null)
            {
                query = query.Where(t => t.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.CodPerfil != null)
            {
                query = query.Where(t => t.Usuario.CodPerfil == parameters.CodPerfil);
            }

            if (parameters.Nome != null)
            {
                query = query.Where(t => t.Nome == parameters.Nome);
            }

            if (parameters.CodAutorizada != null)
            {
                query = query.Where(t => t.CodAutorizada == parameters.CodAutorizada);
            }

            if (parameters.PA != null)
            {
                query = query.Where(t => t.RegiaoAutorizada.PA == parameters.PA);
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

            if (parameters.CodStatusServicos != null)
            {
                var codigosStatusServico = parameters.CodStatusServicos.Split(',');

                query = query
                    .Include(t => t.OrdensServico.Where(os => codigosStatusServico.Contains(os.CodStatusServico.ToString())))
                        .ThenInclude(os => os.TipoIntervencao);
            }

            return query;
        }
    }
}
