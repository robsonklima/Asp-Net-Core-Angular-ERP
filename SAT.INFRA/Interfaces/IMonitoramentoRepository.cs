﻿using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IMonitoramentoRepository
    {
        PagedList<Monitoramento> ObterPorParametros(MonitoramentoParameters parameters);
    }
}