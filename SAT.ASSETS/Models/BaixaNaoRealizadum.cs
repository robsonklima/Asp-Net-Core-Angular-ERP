using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class BaixaNaoRealizadum
    {
        [Key]
        public int CodBaixaNaoRealizada { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [StringLength(5000)]
        public string Observacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string UsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.BaixaNaoRealizada))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(UsuarioCadastro))]
        [InverseProperty(nameof(Usuario.BaixaNaoRealizada))]
        public virtual Usuario UsuarioCadastroNavigation { get; set; }
    }
}
