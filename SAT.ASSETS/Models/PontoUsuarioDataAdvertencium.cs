using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PontoUsuarioDataAdvertencium
    {
        [Key]
        public int CodPontoUsuarioDataAdvertencia { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int CodPontoUsuarioDataTipoAdvertencia { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioAdvertido { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodPontoUsuarioData))]
        [InverseProperty(nameof(PontoUsuarioDatum.PontoUsuarioDataAdvertencia))]
        public virtual PontoUsuarioDatum CodPontoUsuarioDataNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioDataTipoAdvertencia))]
        [InverseProperty(nameof(PontoUsuarioDataTipoAdvertencium.PontoUsuarioDataAdvertencia))]
        public virtual PontoUsuarioDataTipoAdvertencium CodPontoUsuarioDataTipoAdvertenciaNavigation { get; set; }
    }
}
