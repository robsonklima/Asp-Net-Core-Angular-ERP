using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Veloh")]
    public partial class Veloh
    {
        [Column("CPF/CNPJ Estabelecimento")]
        [StringLength(255)]
        public string CpfCnpjEstabelecimento { get; set; }
        [StringLength(255)]
        public string Estabelecimento { get; set; }
        [Column("CEP")]
        [StringLength(255)]
        public string Cep { get; set; }
        [StringLength(255)]
        public string Uf { get; set; }
        [StringLength(255)]
        public string Cidade { get; set; }
        [StringLength(255)]
        public string Bairro { get; set; }
        [StringLength(255)]
        public string Endereço { get; set; }
        public double? Número { get; set; }
        [StringLength(255)]
        public string Complemento { get; set; }
        [StringLength(255)]
        public string Contato { get; set; }
        [Column("Telefone Contato")]
        [StringLength(255)]
        public string TelefoneContato { get; set; }
        [Column("E-mail Contato")]
        [StringLength(255)]
        public string EMailContato { get; set; }
        [StringLength(255)]
        public string Rede { get; set; }
        [StringLength(255)]
        public string Mnemônico { get; set; }
        [StringLength(255)]
        public string Estab { get; set; }
        [StringLength(255)]
        public string Terminal { get; set; }
        [Column("Tipo Terminal")]
        [StringLength(255)]
        public string TipoTerminal { get; set; }
        [StringLength(255)]
        public string Comunicação { get; set; }
        [Column("Modo Aquisição")]
        [StringLength(255)]
        public string ModoAquisição { get; set; }
        [Column("Terminal Habilitado")]
        [StringLength(255)]
        public string TerminalHabilitado { get; set; }
        [Column("Data Intalação", TypeName = "datetime")]
        public DateTime? DataIntalação { get; set; }
        [Column("Data Desabilitação")]
        [StringLength(255)]
        public string DataDesabilitação { get; set; }
        [StringLength(255)]
        public string Versão { get; set; }
    }
}
