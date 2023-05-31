using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ChecklistPreventivaLayoutItem")]
    public partial class ChecklistPreventivaLayoutItem
    {
        public int CodChecklistPreventivaLayoutItem { get; set; }
        public int? CodChecklistPreventivaLayoutPeriferico { get; set; }
        public int? ObrigatorioObs { get; set; }
        public string Descricao { get; set; }
        public int? IndAtivo { get; set; }
    }
}
