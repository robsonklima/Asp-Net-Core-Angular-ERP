﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaItem")]
    public partial class DespesaItem
    {
        [Key]
        public int CodDespesaItem { get; set; }
        public int CodDespesa { get; set; }
        public int CodDespesaTipo { get; set; }
        public int CodDespesaConfiguracao { get; set; }
        public int? SequenciaDespesaKm { get; set; }
        [Column("NumNF")]
        [StringLength(100)]
        public string NumNf { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DespesaValor { get; set; }
        [StringLength(150)]
        public string EnderecoOrigem { get; set; }
        [StringLength(10)]
        public string NumOrigem { get; set; }
        [StringLength(255)]
        public string BairroOrigem { get; set; }
        public int? CodCidadeOrigem { get; set; }
        [StringLength(255)]
        public string EnderecoOrigemWebraska { get; set; }
        [StringLength(10)]
        public string NumOrigemWebraska { get; set; }
        [StringLength(100)]
        public string BairroOrigemWebraska { get; set; }
        [StringLength(50)]
        public string NomeCidadeOrigemWebraska { get; set; }
        [Column("SiglaUFOrigemWebraska")]
        [StringLength(3)]
        public string SiglaUforigemWebraska { get; set; }
        [StringLength(3)]
        public string SiglaPaisOrigemWebraska { get; set; }
        public byte IndResidenciaOrigem { get; set; }
        public byte IndHotelOrigem { get; set; }
        [StringLength(255)]
        public string EnderecoDestino { get; set; }
        [StringLength(10)]
        public string NumDestino { get; set; }
        [StringLength(255)]
        public string BairroDestino { get; set; }
        public int? CodCidadeDestino { get; set; }
        [StringLength(150)]
        public string EnderecoDestinoWebraska { get; set; }
        [StringLength(10)]
        public string NumDestinoWebraska { get; set; }
        [StringLength(50)]
        public string BairroDestinoWebraska { get; set; }
        [StringLength(50)]
        public string NomeCidadeDestinoWebraska { get; set; }
        [Column("SiglaUFDestinoWebraska")]
        [StringLength(3)]
        public string SiglaUfdestinoWebraska { get; set; }
        [StringLength(3)]
        public string SiglaPaisDestinoWebraska { get; set; }
        public byte IndResidenciaDestino { get; set; }
        public byte IndHotelDestino { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? KmPrevisto { get; set; }
        public int? KmPercorrido { get; set; }
        [Column("TentativaKM")]
        [StringLength(200)]
        public string TentativaKm { get; set; }
        [StringLength(500)]
        public string Obs { get; set; }
        [StringLength(500)]
        public string ObsReprovacao { get; set; }
        public int? CodDespesaItemAlerta { get; set; }
        public byte IndWebrascaIndisponivel { get; set; }
        public byte IndReprovado { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(50)]
        public string LatitudeHotel { get; set; }
        [StringLength(50)]
        public string LongitudeHotel { get; set; }

        [ForeignKey(nameof(CodCidadeDestino))]
        [InverseProperty(nameof(Cidade.DespesaItemCodCidadeDestinoNavigations))]
        public virtual Cidade CodCidadeDestinoNavigation { get; set; }
        [ForeignKey(nameof(CodCidadeOrigem))]
        [InverseProperty(nameof(Cidade.DespesaItemCodCidadeOrigemNavigations))]
        public virtual Cidade CodCidadeOrigemNavigation { get; set; }
        [ForeignKey(nameof(CodDespesaItemAlerta))]
        [InverseProperty(nameof(DespesaItemAlertum.DespesaItems))]
        public virtual DespesaItemAlertum CodDespesaItemAlertaNavigation { get; set; }
        [ForeignKey(nameof(CodDespesa))]
        [InverseProperty(nameof(Despesa.DespesaItems))]
        public virtual Despesa CodDespesaNavigation { get; set; }
        [ForeignKey(nameof(CodDespesaTipo))]
        [InverseProperty(nameof(DespesaTipo.DespesaItems))]
        public virtual DespesaTipo CodDespesaTipoNavigation { get; set; }
    }
}