using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSbancada")]
    public partial class Osbancadum
    {
        public Osbancadum()
        {
            OsbancadaPecas = new HashSet<OsbancadaPeca>();
        }

        [Key]
        [Column("CodOSBancada")]
        public int CodOsbancada { get; set; }
        public int CodClienteBancada { get; set; }
        [Required]
        [Column("NFEntrada")]
        [StringLength(20)]
        public string Nfentrada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataChegada { get; set; }
        [Column("DataNF", TypeName = "datetime")]
        public DateTime? DataNf { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManut { get; set; }
        [StringLength(1000)]
        public string Observacao { get; set; }
        public int? CodFilial { get; set; }
        public int? PrazoEntrega { get; set; }
        [Column("ValorNF", TypeName = "decimal(10, 2)")]
        public decimal? ValorNf { get; set; }

        [InverseProperty(nameof(OsbancadaPeca.CodOsbancadaNavigation))]
        public virtual ICollection<OsbancadaPeca> OsbancadaPecas { get; set; }
    }
}
