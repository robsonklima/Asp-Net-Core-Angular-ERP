using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoLocal")]
    public partial class LoLocal
    {
        [Required]
        [Column("cod_empresa")]
        [StringLength(50)]
        public string CodEmpresa { get; set; }
        [Required]
        [Column("cod_local")]
        [StringLength(50)]
        public string CodLocal { get; set; }
        [Required]
        [Column("den_local")]
        [StringLength(50)]
        public string DenLocal { get; set; }
        [Required]
        [Column("num_nivel")]
        [StringLength(50)]
        public string NumNivel { get; set; }
    }
}
