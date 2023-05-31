using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcEnderecosDespesa
    {
        [StringLength(8000)]
        public string Origem { get; set; }
        [StringLength(8000)]
        public string Destino { get; set; }
        [StringLength(8000)]
        public string KmPercorrido { get; set; }
        [StringLength(8000)]
        public string KmPrevisto { get; set; }
        public int CodDespesaItem { get; set; }
        [Required]
        [Column("NumRAT")]
        [StringLength(20)]
        public string NumRat { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int? KmPercorridoTecnico { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? KmPrevistoMaplink { get; set; }
        [StringLength(150)]
        public string EnderecoOrigem { get; set; }
        [StringLength(255)]
        public string BairroOrigem { get; set; }
        [Required]
        [StringLength(10)]
        public string NumeroOrigem { get; set; }
        [StringLength(50)]
        public string CidadeOrigem { get; set; }
        [StringLength(50)]
        public string UfOrigem { get; set; }
        [StringLength(255)]
        public string EnderecoDestino { get; set; }
        [StringLength(255)]
        public string BairroDestino { get; set; }
        [Required]
        [StringLength(10)]
        public string NumeroDestino { get; set; }
        [StringLength(50)]
        public string CidadeDestino { get; set; }
        [StringLength(50)]
        public string UfDestino { get; set; }
        public int CodTecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFim { get; set; }
    }
}
