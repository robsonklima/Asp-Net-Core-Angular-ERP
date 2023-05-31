using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TicketLogTransacao")]
    [Index(nameof(ValorTransacao), nameof(DataTransacao), nameof(NumeroCartao), nameof(CnpjEstabelecimento), Name = "UQ_TicketLogTransacao_entries", IsUnique = true)]
    public partial class TicketLogTransacao
    {
        [Key]
        public int CodTicketLogTransacao { get; set; }
        [Column("quilometrosPorLitro")]
        public double QuilometrosPorLitro { get; set; }
        [Column("codigoTransacao")]
        public int CodigoTransacao { get; set; }
        [Required]
        [Column("cor")]
        [StringLength(20)]
        public string Cor { get; set; }
        [Column("valorTransacao")]
        public double ValorTransacao { get; set; }
        [Column("dataTransacao", TypeName = "datetime")]
        public DateTime DataTransacao { get; set; }
        [Column("codigoFamiliaVeiculo")]
        public int CodigoFamiliaVeiculo { get; set; }
        [Required]
        [Column("numeroCartao")]
        [StringLength(50)]
        public string NumeroCartao { get; set; }
        [Required]
        [Column("principal")]
        [StringLength(10)]
        public string Principal { get; set; }
        [Required]
        [Column("veiculoFabricante")]
        [StringLength(50)]
        public string VeiculoFabricante { get; set; }
        [Required]
        [Column("uf")]
        [StringLength(2)]
        public string Uf { get; set; }
        [Required]
        [Column("cnpjEstabelecimento")]
        [StringLength(50)]
        public string CnpjEstabelecimento { get; set; }
        [Required]
        [Column("segmentoVeiculo")]
        [StringLength(100)]
        public string SegmentoVeiculo { get; set; }
        [Required]
        [Column("controlaHodometro")]
        [StringLength(10)]
        public string ControlaHodometro { get; set; }
        [Required]
        [Column("grupoRestricaoVeiculo")]
        [StringLength(100)]
        public string GrupoRestricaoVeiculo { get; set; }
        [Column("litros", TypeName = "numeric(5, 2)")]
        public decimal Litros { get; set; }
        [Column("codigoLiberacaoRestricao")]
        public int CodigoLiberacaoRestricao { get; set; }
        [Required]
        [Column("grupoRestricaoTransacao")]
        [StringLength(125)]
        public string GrupoRestricaoTransacao { get; set; }
        [Required]
        [Column("descricaoSeriePOS")]
        [StringLength(50)]
        public string DescricaoSeriePos { get; set; }
        [Column("quilometrosRodados")]
        public int QuilometrosRodados { get; set; }
        [Required]
        [Column("responsavel")]
        [StringLength(224)]
        public string Responsavel { get; set; }
        [Required]
        [Column("placa")]
        [StringLength(10)]
        public string Placa { get; set; }
        [Column("valorSaldoAnterior")]
        public double ValorSaldoAnterior { get; set; }
        [Column("ano")]
        public int Ano { get; set; }
        [Column("codigoOrdemServico")]
        public int CodigoOrdemServico { get; set; }
        [Required]
        [Column("tipoCombustivel")]
        [StringLength(54)]
        public string TipoCombustivel { get; set; }
        [Required]
        [Column("nomeCidade")]
        [StringLength(120)]
        public string NomeCidade { get; set; }
        [Required]
        [Column("numeroTerminal")]
        [StringLength(50)]
        public string NumeroTerminal { get; set; }
        [Required]
        [Column("codigoServico")]
        [StringLength(50)]
        public string CodigoServico { get; set; }
        [Required]
        [Column("exibeMediaQuilometragem")]
        [StringLength(10)]
        public string ExibeMediaQuilometragem { get; set; }
        [Required]
        [Column("nomeMotorista")]
        [StringLength(124)]
        public string NomeMotorista { get; set; }
        [Required]
        [Column("nomeReduzidoEstabelecimento")]
        [StringLength(140)]
        public string NomeReduzidoEstabelecimento { get; set; }
        [Required]
        [Column("tipoFrota")]
        [StringLength(80)]
        public string TipoFrota { get; set; }
        [Required]
        [Column("veiculoModelo")]
        [StringLength(80)]
        public string VeiculoModelo { get; set; }
        [Required]
        [Column("controleDesempenho")]
        [StringLength(90)]
        public string ControleDesempenho { get; set; }
        [Column("codigoVeiculoCliente")]
        public int CodigoVeiculoCliente { get; set; }
        [Column("codigoTipoCombustivel")]
        public int CodigoTipoCombustivel { get; set; }
        [Column("numeroMatricula")]
        public int NumeroMatricula { get; set; }
        [Column("quilometragemInicial")]
        public bool QuilometragemInicial { get; set; }
        [Column("valorLitro")]
        public double ValorLitro { get; set; }
        [Column("codigoUsuarioCartao")]
        public int CodigoUsuarioCartao { get; set; }
        [Column("codigoEstabelecimento")]
        public int CodigoEstabelecimento { get; set; }
        [Column("quilometragem")]
        public int Quilometragem { get; set; }
        [Required]
        [Column("servico")]
        [StringLength(100)]
        public string Servico { get; set; }
        [Column("codigoServicoOrdemServico")]
        public int CodigoServicoOrdemServico { get; set; }
        [Required]
        [Column("controlaHorimetro")]
        [StringLength(10)]
        public string ControlaHorimetro { get; set; }
        [Column("dataHoraConsulta", TypeName = "datetime")]
        public DateTime DataHoraConsulta { get; set; }
    }
}
