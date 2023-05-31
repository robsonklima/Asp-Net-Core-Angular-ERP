using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDespesasFrotaTecnico
    {
        public int CodTecnico { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [StringLength(50)]
        public string NumeroCartao { get; set; }
        [StringLength(4000)]
        public string Data { get; set; }
        public int? KmPercorrido { get; set; }
        [Required]
        [StringLength(5)]
        public string Autonomia { get; set; }
        [StringLength(8000)]
        public string PrecoLitro { get; set; }
        [StringLength(8000)]
        public string DespesaOutras { get; set; }
        [StringLength(8000)]
        public string DespesaCombustivel { get; set; }
        [StringLength(8000)]
        public string DespesaTotal { get; set; }
    }
}
