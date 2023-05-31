using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class HistEquipamento
    {
        public int CodHistEquipamentos { get; set; }
        [StringLength(50)]
        public string CodUsuario { get; set; }
        [StringLength(50)]
        public string Objeto { get; set; }
        [StringLength(200)]
        public string Motivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHora { get; set; }
        [StringLength(20)]
        public string CodObjeto { get; set; }
        [StringLength(50)]
        public string Acao { get; set; }
    }
}
