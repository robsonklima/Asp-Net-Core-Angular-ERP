using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistFechamentoBanrisul")]
    public partial class HistFechamentoBanrisul
    {
        [Key]
        public int CodHistFechamentoBanrisul { get; set; }
        [StringLength(200)]
        public string TipoRegistro { get; set; }
        [StringLength(200)]
        public string DataGeracaoArquivo { get; set; }
        [StringLength(200)]
        public string HoraGeracaoArquivo { get; set; }
        [StringLength(200)]
        public string CnpjEmpresaParceira { get; set; }
        [StringLength(200)]
        public string NomeEmpresaParceira { get; set; }
        [StringLength(200)]
        public string Destino { get; set; }
        [StringLength(200)]
        public string DescricaoFinalidade { get; set; }
        [StringLength(200)]
        public string PessoaFiscal { get; set; }
        [StringLength(200)]
        public string CnpjCpf { get; set; }
        [StringLength(200)]
        public string Rede { get; set; }
        [StringLength(200)]
        public string Estabelecimento { get; set; }
        [StringLength(200)]
        public string Terminal { get; set; }
        [StringLength(200)]
        public string NumeroSerie { get; set; }
        [StringLength(200)]
        public string TipoTerminal { get; set; }
        [StringLength(200)]
        public string DataInstalacao { get; set; }
        [StringLength(200)]
        public string Situacao { get; set; }
        [StringLength(200)]
        public string Observacao { get; set; }
        [StringLength(200)]
        public string Horario { get; set; }
        [StringLength(200)]
        public string CodigoArea { get; set; }
        [StringLength(200)]
        public string NumeroCelular { get; set; }
        [StringLength(200)]
        public string MacAddress { get; set; }
        [StringLength(200)]
        public string QtdDetalhe2 { get; set; }
        [StringLength(200)]
        public string QtdDetalhe3 { get; set; }
        [StringLength(200)]
        public string QtdDetalhe4 { get; set; }
        [StringLength(200)]
        public string QtdDetalhe5 { get; set; }
        [StringLength(200)]
        public string QtdDetalhe6 { get; set; }
        [StringLength(200)]
        public string QtdDetalhe7 { get; set; }
        [StringLength(200)]
        public string QtdDetalhe8 { get; set; }
        [StringLength(200)]
        public string QtdDetalhe9 { get; set; }
        [StringLength(200)]
        public string Filler { get; set; }
        [StringLength(200)]
        public string NomeArquivo { get; set; }
        [StringLength(200)]
        public string SeqArquivo { get; set; }
        [StringLength(200)]
        public string SeqRegistro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
    }
}
