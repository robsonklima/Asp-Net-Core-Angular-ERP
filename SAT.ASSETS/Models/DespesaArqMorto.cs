using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Despesa_ARQ_MORTO")]
    public partial class DespesaArqMorto
    {
        public int CodDespesa { get; set; }
        public int CodDespesaPeriodo { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        public int CodTecnico { get; set; }
        public int? CodFilial { get; set; }
        [Required]
        [StringLength(10)]
        public string CentroCusto { get; set; }
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
    }
}
