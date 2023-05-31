using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ComponentePOS")]
    public partial class ComponentePo
    {
        [Key]
        [Column("CodComponentePOS")]
        public int CodComponentePos { get; set; }
        [Required]
        [Column("NomeComponentePOS")]
        [StringLength(50)]
        public string NomeComponentePos { get; set; }
        public bool Ativo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataManutencao { get; set; }
        public bool? SomenteImpressao { get; set; }

        [ForeignKey(nameof(CodUsuarioManutencao))]
        [InverseProperty(nameof(Usuario.ComponentePos))]
        public virtual Usuario CodUsuarioManutencaoNavigation { get; set; }
    }
}
