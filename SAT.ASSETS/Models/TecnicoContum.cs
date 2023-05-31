using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TecnicoContum
    {
        [Key]
        public int CodTecnicoConta { get; set; }
        public int CodTecnico { get; set; }
        [Required]
        [StringLength(3)]
        public string NumBanco { get; set; }
        [Required]
        [StringLength(10)]
        public string NumAgencia { get; set; }
        [Required]
        [StringLength(20)]
        public string NumConta { get; set; }
        public byte IndPadrao { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.TecnicoConta))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
    }
}
