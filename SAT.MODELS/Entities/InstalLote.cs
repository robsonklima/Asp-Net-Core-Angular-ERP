using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalLote
    {
        [Key]
        public int CodInstalLote { get; set; }
        public string NomeLote { get; set; }
        public string DescLote { get; set; }
        public DateTime DataRecLote { get; set; }
        public int CodContrato { get; set; }
        public int QtdEquipLote { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}