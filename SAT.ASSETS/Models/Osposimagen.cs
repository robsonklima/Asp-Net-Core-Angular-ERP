using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSPOSImagens")]
    public partial class Osposimagen
    {
        [Key]
        [Column("CodOSPOSImagens")]
        public int CodOsposimagens { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [StringLength(300)]
        public string Imagem { get; set; }
        [StringLength(200)]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Required]
        [StringLength(500)]
        public string Caminho { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.Osposimagens))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.Osposimagens))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
    }
}
