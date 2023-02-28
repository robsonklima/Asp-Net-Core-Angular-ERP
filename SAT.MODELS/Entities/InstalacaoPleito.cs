using System;
using System.Collections.Generic;
namespace SAT.MODELS.Entities
{
    public class InstalacaoPleito
    {
        public int CodInstalPleito { get; set; }
        public int? CodContrato { get; set; }
        public int? CodInstalTipoPleito { get; set; }
        public string NomePleito { get; set; }
        public string DescPleito { get; set; }
        public DateTime? DataEnvio { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public Contrato Contrato { get; set; }
        public InstalacaoTipoPleito InstalacaoTipoPleito { get; set; }
        public List<InstalacaoPleitoInstal> InstalacoesPleitoInstal { get; set; }
    }
}