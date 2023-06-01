using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Temp_RelElefante")]
    public partial class TempRelElefante
    {
        public int? CodEquipContrato { get; set; }
        [Column("LOCAL")]
        [StringLength(255)]
        public string Local { get; set; }
        [Column("SERIE")]
        [StringLength(255)]
        public string Serie { get; set; }
        [Column("ATM")]
        [StringLength(255)]
        public string Atm { get; set; }
        [StringLength(255)]
        public string Chamado { get; set; }
        [Column("Modelo Aceitador")]
        [StringLength(255)]
        public string ModeloAceitador { get; set; }
        [Column("DATA", TypeName = "datetime")]
        public DateTime? Data { get; set; }
    }
}
