using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalPleito
    {
        [Key]
        public int CodInstalPleito { get; set; }
        public int CodContrato { get; set; }
        public int CodInstalTipoPleito { get; set; }
        public string NomePleito { get; set; }
        public string DescPleito { get; set; }
        public DateTime? DataEnvio { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}