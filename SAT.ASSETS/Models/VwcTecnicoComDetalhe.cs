using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcTecnicoComDetalhe
    {
        [Column("NumCREA")]
        [StringLength(30)]
        public string NumCrea { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(20)]
        public string Apelido { get; set; }
        [Column("CPF")]
        [StringLength(20)]
        public string Cpf { get; set; }
        [Column("RG")]
        [StringLength(20)]
        public string Rg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataNascimento { get; set; }
        [Required]
        [StringLength(3)]
        public string IndTecnicoBancada { get; set; }
        [Column("CEP")]
        [StringLength(20)]
        public string Cep { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(50)]
        public string EnderecoComplemento { get; set; }
        [StringLength(60)]
        public string Bairro { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(16)]
        public string FonePerto { get; set; }
        [Required]
        [StringLength(3)]
        public string IndAtivo { get; set; }
        [Required]
        [StringLength(3)]
        public string IndFerias { get; set; }
        [StringLength(50)]
        public string NomePais { get; set; }
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAdmissao { get; set; }
        [StringLength(40)]
        public string SimCardMobile { get; set; }
        [StringLength(16)]
        public string FoneParticular { get; set; }
        [StringLength(40)]
        public string Fone { get; set; }
        [StringLength(3)]
        public string NumBanco { get; set; }
        [StringLength(10)]
        public string NumAgencia { get; set; }
        [StringLength(20)]
        public string NumConta { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        public int? Ano { get; set; }
        [StringLength(8)]
        public string Placa { get; set; }
    }
}
