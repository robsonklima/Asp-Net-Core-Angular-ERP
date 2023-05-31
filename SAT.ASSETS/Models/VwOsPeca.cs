using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwOsPeca
    {
        public int CodPeca { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [StringLength(20)]
        public string CodMagnus { get; set; }
        public int? CodPecaFamilia { get; set; }
        public int? CodPecaSubstituicao { get; set; }
        public int CodPecaStatus { get; set; }
        public int? CodTraducao { get; set; }
        [Required]
        [StringLength(80)]
        public string NomePeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValCusto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValCustoDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValCustoEuro { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPecaDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaEuro { get; set; }
        [Column("ValIPI", TypeName = "decimal(10, 2)")]
        public decimal ValIpi { get; set; }
        public int QtdMinimaVenda { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaAssistencia { get; set; }
        [Column("ValIPIAssistencia", TypeName = "decimal(10, 2)")]
        public decimal? ValIpiassistencia { get; set; }
        [Column("Descr_Ingles")]
        [StringLength(80)]
        public string DescrIngles { get; set; }
        public int IndObrigRastreabilidade { get; set; }
        public int IndValorFixo { get; set; }
        public int Quantidade { get; set; }
    }
}
