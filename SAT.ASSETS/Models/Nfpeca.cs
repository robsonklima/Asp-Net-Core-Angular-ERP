using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("NFPecas")]
    public partial class Nfpeca
    {
        [Key]
        [Column("CodNFPecas")]
        public int CodNfpecas { get; set; }
        [Column("CodRATDetalhesPecas")]
        public int? CodRatdetalhesPecas { get; set; }
        [Column("CodNF")]
        public int? CodNf { get; set; }
        [StringLength(20)]
        public string CodUsuario { get; set; }
    }
}
