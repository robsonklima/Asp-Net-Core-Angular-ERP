using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("DispBBRegiaoUF")]
    public partial class DispBBRegiaoUF
    {
        [Key]
        public int CodDispBBRegiaoUF { get; set; }
        [Column(TypeName = "nchar")]
        public int CodDispBBRegiao { get; set; }
        public int CodUF { get; set; }
        public int IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }

        [ForeignKey("CodDispBBRegiao")]
        public DispBBRegiao DispBBRegiao { get; set; }
    }
}
