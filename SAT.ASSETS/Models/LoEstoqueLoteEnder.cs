using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoEstoqueLoteEnder")]
    public partial class LoEstoqueLoteEnder
    {
        [Required]
        [Column("cod_empresa")]
        [StringLength(2)]
        public string CodEmpresa { get; set; }
        [Required]
        [Column("cod_item")]
        [StringLength(15)]
        public string CodItem { get; set; }
        [Required]
        [Column("cod_local")]
        [StringLength(10)]
        public string CodLocal { get; set; }
        [Column("num_lote")]
        [StringLength(15)]
        public string NumLote { get; set; }
        [Required]
        [Column("endereco")]
        [StringLength(15)]
        public string Endereco { get; set; }
        [Column("num_volume")]
        public int NumVolume { get; set; }
        [Column("cod_grade_1")]
        [StringLength(15)]
        public string CodGrade1 { get; set; }
        [Column("cod_grade_2")]
        [StringLength(15)]
        public string CodGrade2 { get; set; }
        [Column("cod_grade_3")]
        [StringLength(15)]
        public string CodGrade3 { get; set; }
        [Column("cod_grade_4")]
        [StringLength(15)]
        public string CodGrade4 { get; set; }
        [Column("cod_grade_5")]
        [StringLength(15)]
        public string CodGrade5 { get; set; }
        [Column("dat_hor_producao")]
        [StringLength(50)]
        public string DatHorProducao { get; set; }
        [Column("num_ped_ven", TypeName = "decimal(6, 0)")]
        public decimal NumPedVen { get; set; }
        [Column("num_seq_ped_ven", TypeName = "decimal(5, 0)")]
        public decimal NumSeqPedVen { get; set; }
        [Required]
        [Column("ies_situa_qtd")]
        [StringLength(1)]
        public string IesSituaQtd { get; set; }
        [Column("qtd_saldo", TypeName = "decimal(15, 3)")]
        public decimal QtdSaldo { get; set; }
        [Column("num_transac")]
        public int NumTransac { get; set; }
        [Required]
        [Column("ies_origem_entrada")]
        [StringLength(1)]
        public string IesOrigemEntrada { get; set; }
        [Column("dat_hor_validade")]
        [StringLength(50)]
        public string DatHorValidade { get; set; }
        [Required]
        [Column("num_peca")]
        [StringLength(15)]
        public string NumPeca { get; set; }
        [Required]
        [Column("num_serie")]
        [StringLength(15)]
        public string NumSerie { get; set; }
        [Column("comprimento", TypeName = "decimal(15, 3)")]
        public decimal Comprimento { get; set; }
        [Column("largura", TypeName = "decimal(15, 3)")]
        public decimal Largura { get; set; }
        [Column("altura", TypeName = "decimal(15, 3)")]
        public decimal Altura { get; set; }
        [Column("diametro", TypeName = "decimal(15, 3)")]
        public decimal Diametro { get; set; }
        [Column("dat_hor_reserv_1")]
        [StringLength(50)]
        public string DatHorReserv1 { get; set; }
        [Column("dat_hor_reserv_2")]
        [StringLength(50)]
        public string DatHorReserv2 { get; set; }
        [Column("dat_hor_reserv_3")]
        [StringLength(50)]
        public string DatHorReserv3 { get; set; }
        [Column("qtd_reserv_1", TypeName = "decimal(15, 3)")]
        public decimal QtdReserv1 { get; set; }
        [Column("qtd_reserv_2", TypeName = "decimal(15, 3)")]
        public decimal QtdReserv2 { get; set; }
        [Column("qtd_reserv_3", TypeName = "decimal(15, 3)")]
        public decimal QtdReserv3 { get; set; }
        [Column("num_reserv_1")]
        public int NumReserv1 { get; set; }
        [Column("num_reserv_2")]
        public int NumReserv2 { get; set; }
        [Column("num_reserv_3")]
        public int NumReserv3 { get; set; }
        [Required]
        [Column("tex_reservado")]
        [StringLength(100)]
        public string TexReservado { get; set; }
    }
}
