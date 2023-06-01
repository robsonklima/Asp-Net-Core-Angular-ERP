using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoVeloh3")]
    public partial class TipoVeloh3
    {
        public TipoVeloh3()
        {
            ProducaoVeloh3s = new HashSet<ProducaoVeloh3>();
        }

        [Key]
        public int CodTipoVeloh3 { get; set; }
        [Required]
        [Column("TipoVeloh3")]
        [StringLength(50)]
        public string TipoVeloh31 { get; set; }

        [InverseProperty(nameof(ProducaoVeloh3.CodTipoVeloh3Navigation))]
        public virtual ICollection<ProducaoVeloh3> ProducaoVeloh3s { get; set; }
    }
}
