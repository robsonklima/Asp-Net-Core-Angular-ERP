using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AguardandoClientePOS")]
    public partial class AguardandoClientePo
    {
        [Key]
        [Column("CodAguardandoClientePOS")]
        public int CodAguardandoClientePos { get; set; }
        [Required]
        [StringLength(5000)]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        public bool Ativo { get; set; }
        [Required]
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }

        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.AguardandoClientePos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
    }
}
