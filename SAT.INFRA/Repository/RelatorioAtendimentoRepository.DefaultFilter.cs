using SAT.MODELS.Entities;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class RelatorioAtendimentoRepository : IRelatorioAtendimentoRepository
    {
        public IQueryable<RelatorioAtendimento> AplicarFiltroPadrao(IQueryable<RelatorioAtendimento> relatorios, RelatorioAtendimentoParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                relatorios = relatorios.Where(
                    r =>
                    r.CodRAT.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    r.NumRAT.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );

            if (parameters.CodRAT.HasValue)
                relatorios = relatorios.Where(r => r.CodRAT == parameters.CodRAT);

            if (parameters.DataInicio.HasValue)
                relatorios = relatorios.Where(r => r.DataHoraInicio.Date >= parameters.DataInicio.Value.Date);

            if (parameters.DataSolucao.HasValue)
                relatorios = relatorios.Where(r => r.DataHoraSolucao <= parameters.DataSolucao.Value);

            if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            {
                var tecnicos = parameters.CodTecnicos.Split(",").Select(a => a.Trim());
                relatorios = relatorios.Where(r => tecnicos.Any(p => p == r.CodTecnico.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                relatorios = relatorios.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return relatorios;
        }
    }
}