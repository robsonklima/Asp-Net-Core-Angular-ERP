using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CidadeFonetica")]
    public partial class CidadeFonetica
    {
        [Key]
        public int Id { get; set; }
        public int CodCidade { get; set; }
        [Required]
        [StringLength(200)]
        public string Fonetica { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.CidadeFonetica))]
        public virtual Cidade CodCidadeNavigation { get; set; }
    }
}
