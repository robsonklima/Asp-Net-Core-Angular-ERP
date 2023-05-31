using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CidadeDeParaCliente")]
    public partial class CidadeDeParaCliente
    {
        [Key]
        public int CodCidadeDeParaCliente { get; set; }
        public int CodCliente { get; set; }
        public int CodCidade { get; set; }
        [Required]
        [StringLength(500)]
        public string NomeCidade { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(2)]
        public string SiglaUf { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.CidadeDeParaClientes))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.CidadeDeParaClientes))]
        public virtual Cliente CodClienteNavigation { get; set; }
    }
}
