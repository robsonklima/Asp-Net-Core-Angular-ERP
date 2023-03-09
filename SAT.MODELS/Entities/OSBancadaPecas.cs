using System;

namespace SAT.MODELS.Entities
{
    public class OSBancadaPecas
    {
        public int CodOsbancada { get; set; }
        public int CodPecaRe5114 { get; set; }
        public int? CodFilialRe5114 { get; set; }
        public byte? IndGarantia { get; set; }
        public string DefeitoRelatado { get; set; }
        public string DefeitoConstatado { get; set; }
        public string Solucao { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataManut { get; set; }
        public string MotivoQgarantia { get; set; }
        public byte? IndPecaLiberada { get; set; }
        public DateTime? DataHoraPecaLiberada { get; set; }
        public decimal? ValorEntrada { get; set; }
        public byte? IndPecaDevolvida { get; set; }
        public int? CodPecaRe5114troca { get; set; }
        public int? NumItemNf { get; set; }
        public string NomeTecnicoRelatante { get; set; }
        public byte? IndImpressao { get; set; }
        public OSBancada OSBancada { get; set; }
        public PecaRE5114 PecaRE5114 { get; set; }
    }
}
