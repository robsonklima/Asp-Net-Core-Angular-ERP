using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtPeca
    {
        public int CodPeca { get; set; }
        [Required]
        [StringLength(20)]
        public string CodMagnus { get; set; }
        [Required]
        [StringLength(80)]
        public string NomePeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValCusto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValCustoDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPecaDolar { get; set; }
        [Column("ValIPI", TypeName = "decimal(10, 2)")]
        public decimal ValIpi { get; set; }
        public int QtdMinimaVenda { get; set; }
        public int? CodPecaFamilia { get; set; }
        public int? CodPecaSubstituicao { get; set; }
        public int CodPecaStatus { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}
