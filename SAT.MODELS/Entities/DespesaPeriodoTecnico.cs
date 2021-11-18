using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class DespesaPeriodoTecnico
    {
        public int CodDespesaPeriodoTecnico { get; set; }
        public int CodDespesaPeriodo { get; set; }
        public int CodTecnico { get; set; }
        public int CodDespesaPeriodoTecnicoStatus { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioCredito { get; set; }
        public DateTime? DataHoraCredito { get; set; }
        public string CodUsuarioCreditoCancelado { get; set; }
        public DateTime? DataHoraCreditoCancelado { get; set; }
        public byte? IndCredito { get; set; }
        public string CodUsuarioVerificacao { get; set; }
        public DateTime? DataHoraVerificacao { get; set; }
        public byte? IndVerificacao { get; set; }
        public string CodUsuarioVerificacaoCancelado { get; set; }
        public DateTime? DataHoraVerificacaoCancelado { get; set; }
        public byte? IndCompensacao { get; set; }
        public DateTime? DataHoraCompensacao { get; set; }
        public string CodUsuarioCompensacao { get; set; }
        public virtual DespesaProtocoloPeriodoTecnico DespesaProtocoloPeriodoTecnico { get; set; }
        public List<Despesa> Despesas { get; set; }
        public DespesaPeriodoTecnicoStatus DespesaPeriodoTecnicoStatus { get; set; }
        public Tecnico Tecnico { get; set; }
        public virtual DespesaPeriodo DespesaPeriodo { get; set; }
        public TicketLogPedidoCredito TicketLogPedidoCredito { get; set; }
    }
}