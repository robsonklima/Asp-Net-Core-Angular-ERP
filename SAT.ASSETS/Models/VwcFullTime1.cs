using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcFullTime1
    {
        [Column("FILIAL")]
        [StringLength(50)]
        public string Filial { get; set; }
        [Column("JANELA_1")]
        public int? Janela1 { get; set; }
        [Column("JANELA_2")]
        public int? Janela2 { get; set; }
        [Column("JANELA_3")]
        public int? Janela3 { get; set; }
    }
}
