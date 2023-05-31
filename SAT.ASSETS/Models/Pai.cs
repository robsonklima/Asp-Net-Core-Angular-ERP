using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class Pai
    {
        public Pai()
        {
            FeriadosNacionais = new HashSet<FeriadosNacionai>();
            FeriadosPos = new HashSet<FeriadosPo>();
        }

        [Key]
        public int CodPais { get; set; }
        [Required]
        [StringLength(3)]
        public string SiglaPais { get; set; }
        [Required]
        [StringLength(50)]
        public string NomePais { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.PaiCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.PaiCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(FeriadosNacionai.CodPaisNavigation))]
        public virtual ICollection<FeriadosNacionai> FeriadosNacionais { get; set; }
        [InverseProperty(nameof(FeriadosPo.CodPaisNavigation))]
        public virtual ICollection<FeriadosPo> FeriadosPos { get; set; }
    }
}
