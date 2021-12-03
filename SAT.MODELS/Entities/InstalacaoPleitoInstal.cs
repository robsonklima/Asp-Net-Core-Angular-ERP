using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalPleitoInstal")]
    public class InstalacaoPleitoInstal
    {
        [Key]
        public int CodInstalacao { get; set; }
        public int CodInstalPleito { get; set; }
        public int? CodEquipContrato { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}