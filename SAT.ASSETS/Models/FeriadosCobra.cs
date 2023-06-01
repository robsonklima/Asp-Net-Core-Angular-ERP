using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("FeriadosCobra")]
    public partial class FeriadosCobra
    {
        [Column("UF")]
        [StringLength(255)]
        public string Uf { get; set; }
        [Column("CIDADE")]
        [StringLength(255)]
        public string Cidade { get; set; }
        [Column("MES")]
        [StringLength(255)]
        public string Mes { get; set; }
        [Column("DIA")]
        public double? Dia { get; set; }
        [Column("TIPO")]
        [StringLength(255)]
        public string Tipo { get; set; }
        [Column("COD_MUNICIPIO")]
        [StringLength(255)]
        public string CodMunicipio { get; set; }
    }
}
