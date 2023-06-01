using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORCheckListItem")]
    public partial class OrcheckListItem
    {
        [Key]
        [Column("CodORCheckListItem")]
        public int CodOrcheckListItem { get; set; }
        [StringLength(200)]
        public string CodMagnus { get; set; }
        [StringLength(200)]
        public string Descricao { get; set; }
        [StringLength(200)]
        public string Nivel { get; set; }
        [StringLength(200)]
        public string Acao { get; set; }
        [StringLength(200)]
        public string Parametro { get; set; }
        [StringLength(500)]
        public string Realizacao { get; set; }
        [Column("PN_MEI")]
        [StringLength(50)]
        public string PnMei { get; set; }
        [Column("CodORChecklist")]
        public int? CodOrchecklist { get; set; }
        public int? PassoObrigatorio { get; set; }
    }
}
