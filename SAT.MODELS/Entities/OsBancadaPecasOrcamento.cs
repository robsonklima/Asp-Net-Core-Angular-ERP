using System;

namespace SAT.MODELS.Entities
{
    public class OsBancadaPecasOrcamento
    {

        public int CodOrcamento { get; set; }
        public int CodOsbancada { get; set; }
        public int CodPecaRe5114 { get; set; }
        public byte? IndOrcamentoAprov { get; set; }
        public byte? TipoOrcamentoEscolhido { get; set; }
        public decimal? ValorPreAprovado { get; set; }
        public string Observacao { get; set; }
        public int? NumeroAlteracao { get; set; }
        public int? CodOrcamentoPai { get; set; }
        public string CodUsuarioManut { get; set; }
        public string NumOrdemCompra { get; set; }
        public string MotivoReprov { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? CodOrcamentoQtdPai { get; set; }
        public OSBancadaPecas OSBancadaPecas { get; set; }

    }
}
