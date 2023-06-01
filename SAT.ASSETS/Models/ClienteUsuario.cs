using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ClienteUsuario")]
    public partial class ClienteUsuario
    {
        [Key]
        public int CodCliente { get; set; }
        [Key]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.ClienteUsuarios))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.ClienteUsuarios))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
