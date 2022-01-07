using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class MediaAtendimentoTecnicoRepository : IMediaAtendimentoTecnicoRepository
    {
        public void AtualizaMediaTecnico()
        {
            var inicioMedia = DateTime.Now.AddMonths(-3).Date;
            var fimMedia = DateTime.Now.Date;

            var tecnicos = this._context.Tecnico
            .Where(i => i.IndAtivo == 1 && i.CodTecnico.GetValueOrDefault(0) != 0 && i.IndFerias != 1)
            .Take(20)
            .ToArray();

            foreach (var tecnico in tecnicos)
            {
                var codTecnico =
                    tecnico.CodTecnico.GetValueOrDefault(0);

                if (codTecnico == 0)
                    return;

                var rats =
                    this._context.RelatorioAtendimento
                        .Where(i => i.CodTecnico == tecnico.CodTecnico &&
                            i.DataHoraSolucao >= inicioMedia && i.DataHoraSolucao <= fimMedia)
                        .ToArray();

                var codRats =
                    rats.Select(i => i.CodRAT);

                var ordensServicoPorIntervencao =
                    this._context.OrdemServico
                    .Where(i => i.RelatoriosAtendimento.Any(i => codRats.Contains(i.CodRAT)))
                    .Distinct()
                    .AsEnumerable()
                    .GroupBy(i => i.CodTipoIntervencao);

                foreach (var ordensServico in ordensServicoPorIntervencao)
                {
                    if (ordensServico.Any())
                    {
                        var codOS = ordensServico.FirstOrDefault().CodOS;
                        var codTipoIntervencao = ordensServico.Key;
                        var ratsDaOS = rats.Where(i => i.CodOS == codOS);

                        if (ratsDaOS.Any())
                        {
                            var minutosEmAtendimento =
                                ratsDaOS.Sum(i => (i.DataHoraSolucao - i.DataHoraInicio).TotalMinutes) / ratsDaOS.Count();

                            this.AtualizarOuCriar(new MediaAtendimentoTecnico
                            {
                                CodTecnico = codTecnico,
                                CodTipoIntervencao = codTipoIntervencao,
                                MediaEmMinutos = minutosEmAtendimento,
                                CodUsuarioManut = Constants.AGENDADOR_NOME,
                                DataHoraManut = DateTime.Now
                            });
                        }
                    }
                }
            }
        }
    }
}