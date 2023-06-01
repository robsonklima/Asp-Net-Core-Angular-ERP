﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("FaturamentoSicrediOS")]
    public partial class FaturamentoSicrediO
    {
        [Column("CodFaturamentoSicrediOS")]
        public int CodFaturamentoSicrediOs { get; set; }
        public int CodFaturamentoSicredi { get; set; }
        public int CodCooperativaSicredi { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodigoUA")]
        [StringLength(10)]
        public string CodigoUa { get; set; }
        [Required]
        [StringLength(50)]
        public string CodCooperativa { get; set; }
        [StringLength(250)]
        public string NomeReduzidoCooperativa { get; set; }
        [StringLength(50)]
        public string CnpjCooperativa { get; set; }
        [Required]
        [StringLength(250)]
        public string RazaoSocialCooperativa { get; set; }
        [StringLength(100)]
        public string NomeCooperativa { get; set; }
        [Column("NumeroOS")]
        [StringLength(50)]
        public string NumeroOs { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [StringLength(20)]
        public string NumeroLogico { get; set; }
        [StringLength(15)]
        public string CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Required]
        [Column("GrupoEStados")]
        [StringLength(6)]
        public string GrupoEstados { get; set; }
        [StringLength(100)]
        public string NomeRepresentante { get; set; }
        [Column("NumeroNF")]
        [StringLength(50)]
        public string NumeroNf { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInstalacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusaoBanrisul { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorContratoApartir13Mes { get; set; }
        public int? DiasApartir13Mes { get; set; }
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? ValorApartir13Mes { get; set; }
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? ValorCobrarApartir13Mes { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorContrato { get; set; }
        public int? Dias { get; set; }
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? Valor { get; set; }
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? ValorCobrar { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorTotal { get; set; }
        [Required]
        [Column("servico")]
        [StringLength(14)]
        public string Servico { get; set; }
        [Required]
        [Column("cnpjperto")]
        [StringLength(18)]
        public string Cnpjperto { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorRessarcir { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorDescontado { get; set; }
        public int? CodRepresentante { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorRepresentante1 { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorRepresentante2 { get; set; }
        public int? Ano { get; set; }
        public int? Meses { get; set; }
        public int? DiasInstala { get; set; }
        public int? MesesTotal { get; set; }
        public double? MesesFloat { get; set; }

        [ForeignKey(nameof(CodCooperativaSicredi))]
        public virtual CooperativaSicredi CodCooperativaSicrediNavigation { get; set; }
        [ForeignKey(nameof(CodFaturamentoSicredi))]
        public virtual FaturamentoSicredi CodFaturamentoSicrediNavigation { get; set; }
        [ForeignKey(nameof(CodOs))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodRepresentante))]
        public virtual Representante CodRepresentanteNavigation { get; set; }
    }
}
