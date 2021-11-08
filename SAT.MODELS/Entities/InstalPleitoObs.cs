using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalPleitoObs
    {
        [Key]
        public int CodInstalPleitoObs { get; set; }
        public int CodInstalPleito { get; set; }
        public string Observacao { get; set; }
        public string CodUsuarioCad { get; set; }
        public byte IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}