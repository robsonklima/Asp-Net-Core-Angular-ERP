using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TicketLogUsuarioCartaoPlaca")]
    public partial class TicketLogUsuarioCartaoPlaca
    {
        [Key]
        public int CodTicketLogUsuarioCartaoPlaca { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroCartao { get; set; }
        [Required]
        [StringLength(250)]
        public string NomeResponsavel { get; set; }
        [Required]
        [StringLength(12)]
        public string Placa { get; set; }
        [StringLength(150)]
        public string VeiculoCidade { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtivacao { get; set; }
        [StringLength(4)]
        public string Situacao { get; set; }
        [Column("VeiculoUF")]
        [StringLength(3)]
        public string VeiculoUf { get; set; }
        [StringLength(60)]
        public string DescricaoTipoCombustivel { get; set; }
        [StringLength(50)]
        public string DescricaoTipoFrota { get; set; }
        public double? ValorReservado { get; set; }
        public int? CodigoGrupoRestricao { get; set; }
        public double? Saldo { get; set; }
        [StringLength(90)]
        public string DescricaoModeloVeiculo { get; set; }
        [StringLength(8)]
        public string Temporario { get; set; }
        [StringLength(50)]
        public string VeiculoFabricante { get; set; }
        public double? Compras { get; set; }
        [StringLength(1)]
        public string ControlaHodometro { get; set; }
        public double? LimiteAtual { get; set; }
        public int? CodigoUsuarioCartao { get; set; }
        [StringLength(12)]
        public string SituacaoVeiculo { get; set; }
        [StringLength(1)]
        public string TrackOnline { get; set; }
        [StringLength(1)]
        public string ControlaHorimetro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
