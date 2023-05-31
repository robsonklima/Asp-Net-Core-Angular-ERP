using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("RegistroReconfiguracao", Schema = "abertura")]
    public partial class RegistroReconfiguracao
    {
        public int Id { get; set; }
        public int IdArquivo { get; set; }
        [Required]
        [StringLength(1)]
        public string TipoRegistro { get; set; }
        [Required]
        [StringLength(4)]
        public string AgenciaEstabelecimento { get; set; }
        [Required]
        [StringLength(10)]
        public string ContaCorrenteEstabelecimento { get; set; }
        [Required]
        [StringLength(11)]
        public string Rede { get; set; }
        [Required]
        [StringLength(15)]
        public string Estabelecimento { get; set; }
        [Required]
        [StringLength(8)]
        public string Terminal { get; set; }
        [Required]
        [StringLength(1)]
        public string PessoaFiscal { get; set; }
        [Required]
        [StringLength(14)]
        public string CnpjCpf { get; set; }
        [StringLength(10)]
        public string InscricaoEstadual { get; set; }
        [Required]
        [StringLength(35)]
        public string RazaoSocial { get; set; }
        [Required]
        [StringLength(40)]
        public string NomeFantasia { get; set; }
        [Required]
        [StringLength(35)]
        public string Endereco { get; set; }
        [Required]
        [StringLength(20)]
        public string Bairro { get; set; }
        [Required]
        [StringLength(8)]
        public string Cep { get; set; }
        [Required]
        [StringLength(25)]
        public string Cidade { get; set; }
        [Required]
        [StringLength(2)]
        public string Uf { get; set; }
        [Required]
        [StringLength(25)]
        public string NomeContato { get; set; }
        [Required]
        [StringLength(15)]
        public string TelefoneContato { get; set; }
        [Required]
        [StringLength(1)]
        public string ModoAquisicao { get; set; }
        [Required]
        [StringLength(1)]
        public string Prioridade { get; set; }
        [Required]
        [StringLength(14)]
        public string CnpjCpfOrigem { get; set; }
        [Required]
        [StringLength(11)]
        public string RedeOrigem { get; set; }
        [Required]
        [StringLength(15)]
        public string EstabelecimentoOrigem { get; set; }
        [Required]
        [StringLength(8)]
        public string TerminalOrigem { get; set; }
        [StringLength(20)]
        public string NumeroSerie { get; set; }
        [StringLength(3)]
        public string TipoTerminal { get; set; }
        [StringLength(5)]
        public string HorarioEstabelecimentoTurno1 { get; set; }
        [StringLength(5)]
        public string HorarioEstabelecimentoTurno2 { get; set; }
        [Required]
        [StringLength(1)]
        public string Reabertura { get; set; }
        [StringLength(50)]
        public string Observacao { get; set; }
        [StringLength(2)]
        public string CodigoArea { get; set; }
        [StringLength(8)]
        public string NumeroCelular { get; set; }
        [StringLength(2)]
        public string CodigoAreaOrigem { get; set; }
        [StringLength(8)]
        public string NumeroCelularOrigem { get; set; }
        [StringLength(12)]
        public string MacAddress { get; set; }
        [Required]
        [StringLength(22)]
        public string Filler { get; set; }
        [Required]
        [StringLength(9)]
        public string SeqRegistro { get; set; }
        public int? CodigoOrdemServico { get; set; }
        [Required]
        [StringLength(40)]
        public string Checksum { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string UsuarioCadastro { get; set; }
    }
}
