using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("HistPecaSTN")]
    public partial class HistPecaStn
    {
        public int? CodPeca { get; set; }
        public byte? ListaBackup { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DtObsoleto { get; set; }
        [Column("UtilizadoDSS")]
        public byte? UtilizadoDss { get; set; }
        public byte? ItemLogix { get; set; }
        public int? HierarquiaPesquisa { get; set; }
        public double? IndiceDeTroca { get; set; }
        public byte? KitTecnico { get; set; }
        [Column("QTDPecaKitTecnico")]
        public int? QtdpecaKitTecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataIntegracaoLogix { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtualizacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
