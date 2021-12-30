using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        /// <summary>
        /// Roda via Agendamento
        /// </summary>
        public void AtualizaMediaTecnico()
        {
            //             Tecnico[] tecnicos = this._tecnicoRepo.ObterQuery(new TecnicoParameters
            //             {
            //                 Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
            //                 IndAtivo = 1
            //             })
            //             .Where(t => t.CodTecnico.GetValueOrDefault(0) != 0 && t.IndFerias != 1)
            //             .ToArray().Take(2).ToArray();
            // 
            //             var listaMedias = new List<MediaAtendimentoTecnico>();
            // 
            //             foreach (var tecnico in tecnicos)
            //             {
            //                 var ratsPorIntervencao = this._ratRepo.ObterQuery(new RelatorioAtendimentoParameters
            //                 {
            //                     CodTecnicos = tecnico.CodTecnico.ToString(),
            //                     DataInicio = DateTime.Now.AddMonths(-3).Date,
            //                     Include = RelatorioAtendimentoIncludeEnum.RAT_OS
            //                 })
            //                 .ToArray()
            //                 .GroupBy(i => i.OrdemServico.CodTipoIntervencao);
            // 
            //                 foreach (var rats in ratsPorIntervencao)
            //                 {
            //                     if (rats.Any())
            //                     {
            //                         var codTecnico = rats.FirstOrDefault().CodTecnico.Value;
            //                         var avg = rats.Sum(i => (i.DataHoraSolucao - i.DataHoraInicio).TotalMinutes) / rats.Count();
            // 
            //                         var media = new MediaAtendimentoTecnico
            //                         {
            //                             CodTecnico = codTecnico,
            //                             CodTipoIntervencao = rats.Key,
            //                             CodUsuarioManut = Constants.AGENDADOR_NOME,
            //                             DataHoraManut = DateTime.Now
            //                         };
            // 
            //                         listaMedias.Add(media);
            //                     }
            //                 }
            //             }
            // 
            //             this._mediaTecnicoRepo.AtualizarListaAsync(listaMedias);
        }
    }
}