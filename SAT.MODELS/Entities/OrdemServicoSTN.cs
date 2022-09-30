using System;

namespace SAT.MODELS.Entities
{
    public class OrdemServicoSTN
    {
        public int CodAtendimento { get; set; }
        public int CodOS { get; set; }
        public DateTime DataHoraAberturaSTN { get; set; }
        public DateTime? DataHoraFechamentoSTN { get; set; }
        public int CodStatusSTN { get; set; }
        public string CodTipoCausa { get; set; }
        public string CodGrupoCausa { get; set; }
        public string CodDefeito { get; set; }
        public string CodCausa { get; set; }
        public int? CodAcao { get; set; }
        public string CodTecnico { get; set; }
        public string CodUsuarioCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public int? CodOrigemChamadoSTN { get; set; }
        public int? IndAtivo { get; set; }
        public int? NumReincidenciaAoAssumir { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? NumTratativas { get; set; }
        public int? IndEvitaPendencia { get; set; }
        public int? IndPrimeiraLigacao { get; set; }
        public string NomeSolicitante { get; set; }
        public string ObsSistema { get; set; }
    }
}
