using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoSolicitacao
    {
        [Key]
        public int CodDespesaAdiantamentoSolicitacao { get; set; }
        public int CodTecnico { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public decimal SaldoLogix { get; set; }
        public decimal MediaMensal { get; set; }
        public decimal MediaQuinzenal { get; set; }
        public decimal MediaSemanal { get; set; }
        public decimal SaldoAbertoLogixMensal { get; set; }
        public decimal SaldoAbertoLogixQuinzenal { get; set; }
        public decimal SaldoAbertoLogixSemanal { get; set; }
        public decimal RDsEmAbertoMensal { get; set; }
        public decimal RDsEmAbertoQuinzenal { get; set; }
        public decimal RDsEmAbertoSemanal { get; set; }
        public decimal SaldoAdiantamentoSATMensal { get; set; }
        public decimal SaldoAdiantamentoSATQuinzenal { get; set; }
        public decimal SaldoAdiantamentoSATSemanal { get; set; }
        public decimal MaximoParaSolicitarMensal { get; set; }
        public decimal MaximoParaSolicitarQuinzenal { get; set; }
        public decimal MaximoParaSolicitarSemanal { get; set; }
        public decimal ValorAdiantamentoSolicitado { get; set; }
        public string Justificativa { get; set; }
        public string Emails { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}