using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class MonitoramentoService : IMonitoramentoService
    {
        public Monitoramento[] ObterPorServicos(MonitoramentoParameters parameters)
        {
            List<LogAlerta> logAlertas =
                this._logAlertRepository.ObterPorQuery(new LogAlertaParameters { }).ToList();

            return (from a in logAlertas
                    select new Monitoramento
                    {
                        Nome = a.Item,
                        DataProcessamento = (DateTime)a.DataHoraProcessamento,
                        Mensagem = a.Mensagem
                    }).OrderByDescending(s => s.DataProcessamento).ToArray();
        }
    }
}