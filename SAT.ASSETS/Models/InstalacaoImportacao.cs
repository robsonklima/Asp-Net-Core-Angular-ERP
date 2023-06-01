using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalacaoImportacao")]
    public partial class InstalacaoImportacao
    {
        [Key]
        public int CodInstalacaoImportacao { get; set; }
        public int CodInstalacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Datainstalacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendada { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public int? BorderosInstalados { get; set; }
        public double? ValoresBorderos { get; set; }
        [StringLength(50)]
        public string StatusDocumentos { get; set; }
        public double? ValorEquipamento { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [StringLength(3)]
        public string Frustrado { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
