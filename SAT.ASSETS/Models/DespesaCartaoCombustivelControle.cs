using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DespesaCartaoCombustivelControle")]
    public partial class DespesaCartaoCombustivelControle
    {
        [StringLength(50)]
        public string Numero { get; set; }
        [StringLength(50)]
        public string Placa { get; set; }
        [StringLength(150)]
        public string Tecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicio { get; set; }
        [StringLength(10)]
        public string Lote { get; set; }
    }
}
