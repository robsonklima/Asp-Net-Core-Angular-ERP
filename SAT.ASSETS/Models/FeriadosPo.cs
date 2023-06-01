using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FeriadosPOS")]
    public partial class FeriadosPo
    {
        [Key]
        [Column("CodFeriadosPOS")]
        public int CodFeriadosPos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFeriado { get; set; }
        [Required]
        [StringLength(500)]
        public string NomeFeriado { get; set; }
        public int? CodCidade { get; set; }
        [Column("CodUF")]
        public int? CodUf { get; set; }
        public int CodPais { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        public int? CodCliente { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.FeriadosPos))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.FeriadosPos))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodPais))]
        [InverseProperty(nameof(Pai.FeriadosPos))]
        public virtual Pai CodPaisNavigation { get; set; }
        [ForeignKey(nameof(CodUf))]
        [InverseProperty(nameof(Uf.FeriadosPos))]
        public virtual Uf CodUfNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.FeriadosPos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
    }
}
