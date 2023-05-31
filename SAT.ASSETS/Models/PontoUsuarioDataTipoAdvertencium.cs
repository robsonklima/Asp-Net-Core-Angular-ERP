using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PontoUsuarioDataTipoAdvertencium
    {
        public PontoUsuarioDataTipoAdvertencium()
        {
            PontoUsuarioDataAdvertencia = new HashSet<PontoUsuarioDataAdvertencium>();
        }

        [Key]
        public int CodPontoUsuarioDataTipoAdvertencia { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(PontoUsuarioDataAdvertencium.CodPontoUsuarioDataTipoAdvertenciaNavigation))]
        public virtual ICollection<PontoUsuarioDataAdvertencium> PontoUsuarioDataAdvertencia { get; set; }
    }
}
