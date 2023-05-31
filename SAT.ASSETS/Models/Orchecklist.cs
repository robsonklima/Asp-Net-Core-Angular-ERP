using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ORChecklist")]
    public partial class Orchecklist
    {
        [Column("CodORChecklist")]
        public int CodOrchecklist { get; set; }
        [StringLength(300)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodMagnus { get; set; }
        public int CodPeca { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Column("CodORItem")]
        public int? CodOritem { get; set; }
        [Column("CodORCheckListItem")]
        [StringLength(200)]
        public string CodOrcheckListItem { get; set; }
        public int? TempoReparo { get; set; }
    }
}
