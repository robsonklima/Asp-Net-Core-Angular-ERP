using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Fornecedor")]
    public partial class Fornecedor
    {
        [Key]
        public int CodFornecedor { get; set; }
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [StringLength(50)]
        public string Contato { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Telefone { get; set; }
        [StringLength(50)]
        public string MunicipioSede { get; set; }
        [Column("UF")]
        [StringLength(2)]
        public string Uf { get; set; }
        [Column("UFRealizaServico")]
        [StringLength(200)]
        public string UfrealizaServico { get; set; }
    }
}
