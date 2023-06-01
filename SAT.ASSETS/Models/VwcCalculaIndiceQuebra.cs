using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcCalculaIndiceQuebra
    {
        public int Filial { get; set; }
        public int Regiao { get; set; }
        [StringLength(20)]
        public string Pai { get; set; }
        [Required]
        [StringLength(24)]
        public string MagnusPeca { get; set; }
        [Required]
        [StringLength(80)]
        public string DescricaoPeca { get; set; }
        [Column("QTDPeca")]
        public int? Qtdpeca { get; set; }
    }
}
