using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcEstoqueTecnico
    {
        [Required]
        [Column("CPF")]
        [StringLength(50)]
        public string Cpf { get; set; }
        [StringLength(50)]
        public string CodMagnus { get; set; }
        [Column("DATAINICIAL_old", TypeName = "datetime")]
        public DateTime? DatainicialOld { get; set; }
        [Column("DATAINICIAL", TypeName = "datetime")]
        public DateTime? Datainicial { get; set; }
        [Column("DATAFINAL_old", TypeName = "datetime")]
        public DateTime? DatafinalOld { get; set; }
        [Column("DATAFINAL", TypeName = "datetime")]
        public DateTime? Datafinal { get; set; }
        [Column("QTDPECA", TypeName = "decimal(38, 2)")]
        public decimal? Qtdpeca { get; set; }
        public int CodTecnico { get; set; }
    }
}
