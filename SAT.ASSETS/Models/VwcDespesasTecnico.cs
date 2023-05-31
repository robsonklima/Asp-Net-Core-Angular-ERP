using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDespesasTecnico
    {
        public int CodTecnico { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [StringLength(4000)]
        public string Data { get; set; }
        [StringLength(4000)]
        public string DataFim { get; set; }
        public int? KmPercorrido { get; set; }
        [StringLength(8000)]
        public string DespesaOutras { get; set; }
        [StringLength(8000)]
        public string DespesaCombustivel { get; set; }
        [StringLength(8000)]
        public string DespesaTotal { get; set; }
        [StringLength(50)]
        public string CodUsuarioCredito { get; set; }
        [StringLength(40)]
        public string CodUsuarioCompensacao { get; set; }
    }
}
