using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalPleitoInstal
    {
        [Key]
        public int CodInstalacao { get; set; }
        public int CodInstalPleito { get; set; }
        public int? CodEquipContrato { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}