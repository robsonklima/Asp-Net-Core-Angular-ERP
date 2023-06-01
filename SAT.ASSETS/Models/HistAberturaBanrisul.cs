using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistAberturaBanrisul")]
    public partial class HistAberturaBanrisul
    {
        [Key]
        public int CodHistAberturaBanrisul { get; set; }
        [StringLength(100)]
        public string Tipo { get; set; }
        [StringLength(100)]
        public string AbertoEm { get; set; }
        [Column("OSCliente")]
        [StringLength(100)]
        public string Oscliente { get; set; }
        [StringLength(100)]
        public string Agencia { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [StringLength(100)]
        public string Cidade { get; set; }
        [StringLength(100)]
        public string Uf { get; set; }
        [StringLength(100)]
        public string Cep { get; set; }
        [StringLength(100)]
        public string Modelo { get; set; }
        [StringLength(100)]
        public string NumeroSerie { get; set; }
        [StringLength(100)]
        public string Rede { get; set; }
        [StringLength(100)]
        public string Estabelecimento { get; set; }
        [StringLength(100)]
        public string Terminal { get; set; }
        [StringLength(100)]
        public string Descricao { get; set; }
        [StringLength(100)]
        public string CnpjCpf { get; set; }
        [StringLength(100)]
        public string RazaoSocial { get; set; }
        [StringLength(100)]
        public string Conta { get; set; }
        [StringLength(100)]
        public string NomeFantasia { get; set; }
        [StringLength(100)]
        public string Reabertura { get; set; }
        [StringLength(100)]
        public string Prioridade { get; set; }
        [StringLength(100)]
        public string InscricaoEstadual { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
    }
}
