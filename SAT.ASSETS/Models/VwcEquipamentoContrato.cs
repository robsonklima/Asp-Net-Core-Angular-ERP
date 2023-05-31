using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcEquipamentoContrato
    {
        public int CodEquipContrato { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        [Column("CodSLA")]
        public int CodSla { get; set; }
        [StringLength(87)]
        public string AgenciaNumSerie { get; set; }
        [Column("AgenciaSB")]
        [StringLength(11)]
        public string AgenciaSb { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [StringLength(140)]
        public string ClienteAgenciaNumSerie { get; set; }
        public int CodCliente { get; set; }
        public int CodRegiao { get; set; }
        public int CodAutorizada { get; set; }
        public int CodFilial { get; set; }
        public byte? IndGarantia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicGarantia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimGarantia { get; set; }
        public byte IndReceita { get; set; }
        public byte IndRepasse { get; set; }
        [Column(TypeName = "money")]
        public decimal ValorReceita { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorReceitaProRata { get; set; }
        [Column(TypeName = "money")]
        public decimal ValorDespesa { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesaProRata { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesaInstalacao { get; set; }
        public byte IndInstalacao { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtivacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataDesativacao { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(50)]
        public string Lote { get; set; }
        [Required]
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [Required]
        [StringLength(3)]
        public string Retrofit { get; set; }
        [Required]
        [StringLength(3)]
        public string Retrofit2 { get; set; }
        [Required]
        [StringLength(3)]
        public string Retrofit3 { get; set; }
        [Required]
        [Column("MTBF4")]
        [StringLength(3)]
        public string Mtbf4 { get; set; }
    }
}
