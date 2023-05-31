using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwRelatorioGenericoEquipamentoContrato
    {
        public int CodEquipContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(153)]
        public string Contrato { get; set; }
        [StringLength(50)]
        public string NomeTipoContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(10)]
        public string Agencia { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicGarantia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimGarantia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtivacao { get; set; }
        public byte IndAtivo { get; set; }
        public byte? IndGarantia { get; set; }
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Column("NomePAT")]
        [StringLength(50)]
        public string NomePat { get; set; }
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
        [Column(TypeName = "money")]
        public decimal ValorReceita { get; set; }
        [StringLength(20)]
        public string CodMagnus { get; set; }
    }
}
