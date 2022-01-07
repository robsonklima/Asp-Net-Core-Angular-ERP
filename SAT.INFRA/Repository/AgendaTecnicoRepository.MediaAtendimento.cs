using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class AgendaTecnicoRepository : IAgendaTecnicoRepository
    {
        public void AtualizaMediaTecnico()
        {
            var inicioMedia = DateTime.Now.AddMonths(-3).Date;
            var fimMedia = DateTime.Now.Date;

            var tecnicos = this._context.Tecnico
            .Where(i => i.IndAtivo == 1 && i.CodTecnico.GetValueOrDefault(0) != 0 && i.IndFerias != 1)
            .Take(2)
            .ToArray();

            var listaMedias = new List<MediaAtendimentoTecnico>();

            //             foreach (var tecnico in tecnicos)
            //             {
            //                 var ratsPorIntervencao =
            //                     this._context.RelatorioAtendimento
            //                         .Include(i => i.OrdemServico)
            //                         .Where(i => i.CodTecnico == tecnico.CodTecnico &&
            //                             i.DataHoraSolucao >= inicioMedia && i.DataHoraSolucao <= fimMedia)
            //                         .GroupBy(i => i.OrdemServico.CodTipoIntervencao)
            //                         .ToArray();
            // 
            //                 foreach (var rats in ratsPorIntervencao)
            //                 {
            //                     if (rats.Any())
            //                     {
            //                         var codTecnico = rats.FirstOrDefault().CodTecnico.Value;
            //                         var avg = rats.Sum(i => (i.DataHoraSolucao - i.DataHoraInicio).TotalMinutes) / rats.Count();
            // 
            //                         listaMedias.Add(new MediaAtendimentoTecnico
            //                         {
            //                             CodTecnico = codTecnico,
            //                             CodTipoIntervencao = rats.Key,
            //                             CodUsuarioManut = Constants.AGENDADOR_NOME,
            //                             DataHoraManut = DateTime.Now
            //                         });
            //                     }
            //                 }
        }

        //

    }
}