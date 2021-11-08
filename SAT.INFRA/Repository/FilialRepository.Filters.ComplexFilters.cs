using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public partial class FilialRepository : IFilialRepository
    {
        public IQueryable<DashboardTecnicoDisponibilidadeFilialViewModel> AplicarFiltroDashboardDisponibilidadeTecnicos(IQueryable<Filial> query, FilialParameters parameters)
        {
            int[] codFiliais = parameters.CodFiliais.Split(',').Select(i => int.Parse(i)).ToArray();

            return from q in query.Where(s => codFiliais.Contains(s.CodFilial))
                                     .Select(ord => ord.OrdensServico
                                     .Where(os =>
                                       !os.Equipamento.NomeEquip.StartsWith("POS") && /*Remover POS e PERTOS*/
                                       !os.Equipamento.NomeEquip.StartsWith("PERTOS")
                                     ))
                   select new DashboardTecnicoDisponibilidadeFilialViewModel()
                   {
                       CodFilial = q.FirstOrDefault().CodFilial.Value,
                       QtdOSNaoTransferidasCorretivas = q
                       .Count(s => s.CodStatusServico == (int)StatusServicoEnum.ABERTO && s.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA)
                   };
        }
    }
}
