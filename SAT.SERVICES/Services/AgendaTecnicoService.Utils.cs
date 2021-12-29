using System;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {

        private string GetStatusColor(StatusServicoEnum statusServicoEnum)
        {
            switch (statusServicoEnum)
            {
                case StatusServicoEnum.FECHADO:
                    return "#4c4cff";
                case StatusServicoEnum.PECA_FALTANTE:
                    return "#ff4cb7";
                case StatusServicoEnum.PECAS_PENDENTES:
                    return "#ff4cb7";
                case StatusServicoEnum.PECA_EM_TRANSITO:
                    return "#ff4cb7";
                case StatusServicoEnum.PARCIAL:
                    return "#6dbd62";
                case StatusServicoEnum.CANCELADO:
                    return "#964B00";
                default:
                    return "#ff4c4c";
            }
        }
        private string GetTypeColor(AgendaTecnicoTypeEnum type)
        {
            switch (type)
            {
                case AgendaTecnicoTypeEnum.OS:
                    return "#009000";
                case AgendaTecnicoTypeEnum.PONTO:
                    return "#C8C8C8C8";
                case AgendaTecnicoTypeEnum.INTERVALO:
                    return "#C8C8C8C8";
                default:
                    return "#C8C8C8C8";
            }
        }
        private string AgendamentoColor => "#381354";
        private string IntervaloTitle => "INTERVALO";
        private string FimExpedienteTitle => "FIM DO EXPEDIENTE";
        private string PontoTitle => "PONTO";
        private bool isIntervalo(DateTime time) => time >= this.InicioIntervalo(time) && time <= this.FimIntervalo(time);
        private DateTime InicioExpediente(DateTime? referenceTime = null) => referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(8, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(8, 00, 0));
        private DateTime FimExpediente(DateTime? referenceTime = null) => referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(18, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(18, 00, 0));
        private DateTime InicioIntervalo(DateTime? referenceTime = null) => referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(12, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(12, 00, 0));
        private DateTime FimIntervalo(DateTime? referenceTime = null) => referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(13, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(13, 00, 0));
    }
}