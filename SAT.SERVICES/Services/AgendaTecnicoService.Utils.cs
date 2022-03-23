using System;
using SAT.MODELS.Entities.Constants;
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
                    return Constants.COR_AZUL;
                case StatusServicoEnum.PECA_FALTANTE:
                    return Constants.COR_ROSA;
                case StatusServicoEnum.PECAS_PENDENTES:
                    return Constants.COR_ROSA;
                case StatusServicoEnum.PECA_EM_TRANSITO:
                    return Constants.COR_ROSA;
                case StatusServicoEnum.PARCIAL:
                    return Constants.COR_VERDE_CLARO;
                case StatusServicoEnum.CANCELADO:
                    return Constants.COR_TERRA;
                default:
                    return Constants.COR_VERMELHO;
            }
        }
        private string GetTypeColor(AgendaTecnicoTypeEnum type)
        {
            switch (type)
            {
                case AgendaTecnicoTypeEnum.OS:
                    return Constants.COR_VERDE_ESCURO;
                case AgendaTecnicoTypeEnum.PONTO:
                    return Constants.COR_CINZA;
                case AgendaTecnicoTypeEnum.INTERVALO:
                    return Constants.COR_CINZA;
                default:
                    return Constants.COR_CINZA;
            }
        }
        private string AgendamentoColor => Constants.COR_ROXO;
        private string IntervaloTitle => "INTERVALO";
        private string FimExpedienteTitle => "FIM DO EXPEDIENTE";
        private string PontoTitle => "PONTO";
        private bool isIntervalo(DateTime time) => time >= this.InicioIntervalo(time) && time <= this.FimIntervalo(time);
        private DateTime InicioExpediente(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(8, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(8, 00, 0));
        private DateTime FimExpediente(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(18, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(18, 00, 0));
        private DateTime InicioIntervalo(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(12, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(12, 00, 0));
        private DateTime FimIntervalo(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(13, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(13, 00, 0));
    }
}