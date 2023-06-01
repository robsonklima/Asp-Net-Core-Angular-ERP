using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempOS_Ponto")]
    public partial class TempOsPonto
    {
        [StringLength(3)]
        public string Filial { get; set; }
        [StringLength(20)]
        public string SimCard { get; set; }
        [StringLength(80)]
        public string Nome { get; set; }
        [StringLength(7)]
        public string MesAno { get; set; }
        public int? ChamadosRecebidos { get; set; }
        public int? ChamadosGeral { get; set; }
        public int? Pontos { get; set; }
    }
}
