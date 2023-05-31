using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FerramentaTecnicoHist")]
    public partial class FerramentaTecnicoHist
    {
        [Key]
        public int CodFerramentaTecnicoHist { get; set; }
        public int? CodFerramentaTecnico { get; set; }
        [StringLength(50)]
        public string CodUsuario { get; set; }
        public int? Selecionado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? Entregue { get; set; }
        [StringLength(10)]
        public string Justificativa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
