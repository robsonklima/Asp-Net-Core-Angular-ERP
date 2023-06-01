using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Empresa")]
    public partial class Empresa
    {
        [Key]
        public int CodEmpresa { get; set; }
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(20)]
        public string NumBanco { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(20)]
        public string Bairro { get; set; }
        [StringLength(30)]
        public string Cidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(2)]
        public string SiglaUf { get; set; }
        [Column("CEP")]
        [StringLength(20)]
        public string Cep { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Site { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(20)]
        public string InscricaoEstadual { get; set; }
        [StringLength(20)]
        public string Telefone1 { get; set; }
        [StringLength(20)]
        public string Telefone2 { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(1000)]
        public string ObsCliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
    }
}
