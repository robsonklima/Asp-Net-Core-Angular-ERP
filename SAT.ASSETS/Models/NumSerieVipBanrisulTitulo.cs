using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("NumSerieVipBanrisulTitulo")]
    public partial class NumSerieVipBanrisulTitulo
    {
        public NumSerieVipBanrisulTitulo()
        {
            NumSerieVipBanrisuls = new HashSet<NumSerieVipBanrisul>();
        }

        [Key]
        public int CodNumSerieVipBanrisulTitulo { get; set; }
        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }
        public bool Ativo { get; set; }

        [InverseProperty(nameof(NumSerieVipBanrisul.CodNumSerieVipBanrisulTituloNavigation))]
        public virtual ICollection<NumSerieVipBanrisul> NumSerieVipBanrisuls { get; set; }
    }
}
