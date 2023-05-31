using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OSSemRetornoBB")]
    public partial class OssemRetornoBb
    {
        [Column("OS")]
        [StringLength(255)]
        public string Os { get; set; }
        [Column("UNIV")]
        [StringLength(255)]
        public string Univ { get; set; }
        [Column("SIT")]
        [StringLength(255)]
        public string Sit { get; set; }
        [Column("DTA SOLI", TypeName = "datetime")]
        public DateTime? DtaSoli { get; set; }
        [Column("COD MTVO")]
        [StringLength(255)]
        public string CodMtvo { get; set; }
        [Column("UF")]
        [StringLength(255)]
        public string Uf { get; set; }
        [Column("OBS")]
        [StringLength(255)]
        public string Obs { get; set; }
        [Column("CNTR")]
        [StringLength(255)]
        public string Cntr { get; set; }
    }
}
