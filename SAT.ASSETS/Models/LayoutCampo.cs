using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LayoutCampo")]
    public partial class LayoutCampo
    {
        [Key]
        public int CodLayoutCampo { get; set; }
        public int CodLayout { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeLayoutCampo { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeColuna { get; set; }
        public int PosicaoInicio { get; set; }
        public int TamanhoCampo { get; set; }
        public int Linha { get; set; }

        [ForeignKey(nameof(CodLayout))]
        [InverseProperty(nameof(Layout.LayoutCampos))]
        public virtual Layout CodLayoutNavigation { get; set; }
    }
}
