using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoMovelTipoHorario")]
    public partial class PontoMovelTipoHorario
    {
        public PontoMovelTipoHorario()
        {
            PontoMovels = new HashSet<PontoMovel>();
        }

        [Key]
        public int CodPontoMovelTipoHorario { get; set; }
        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }

        [InverseProperty(nameof(PontoMovel.CodPontoMovelTipoHorarioNavigation))]
        public virtual ICollection<PontoMovel> PontoMovels { get; set; }
    }
}
