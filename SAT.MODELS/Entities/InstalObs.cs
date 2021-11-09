using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalObs
    {
        [Key]
        public int CodInstalObs { get; set; }
        public int CodInstalacao { get; set; }
        public string Observacao { get; set; }
        public string CodUsuarioCad { get; set; }
        public int CodInstalRefObs { get; set; }
        public byte IndJustificativa { get; set; }
        public byte IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}