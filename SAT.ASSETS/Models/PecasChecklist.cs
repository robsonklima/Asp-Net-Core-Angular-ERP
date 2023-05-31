using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PecasChecklist")]
    public partial class PecasChecklist
    {
        [Key]
        public int CodPecasChecklist { get; set; }
        [Column("CodChecklistChamadoSTN")]
        public int CodChecklistChamadoStn { get; set; }
        public int CodPeca { get; set; }
        public int Quantidade { get; set; }
        public int IndTroca { get; set; }
    }
}
