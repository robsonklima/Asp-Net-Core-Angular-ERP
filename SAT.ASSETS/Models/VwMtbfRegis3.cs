using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwMtbfRegis3
    {
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(20)]
        public string NroContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Required]
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Required]
        [Column("DCPosto")]
        [StringLength(2)]
        public string Dcposto { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Required]
        [StringLength(1)]
        public string Ind { get; set; }
        public int? QtdDia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtivacao { get; set; }
        [Column("CodECausa")]
        public int? CodEcausa { get; set; }
        [Column("CodECausaAbrev")]
        public int? CodEcausaAbrev { get; set; }
    }
}
