using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcFrotaTecnico
    {
        [StringLength(8000)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [StringLength(8000)]
        public string Cartao { get; set; }
        [Required]
        [StringLength(13)]
        public string Modalidade { get; set; }
        [StringLength(4000)]
        public string EntregaVeiculo { get; set; }
        [StringLength(10)]
        public string Lote { get; set; }
        public int PeriodosPendentesAprovacao { get; set; }
        [StringLength(4000)]
        public string PrazoRegularizacaoPeriodos { get; set; }
    }
}
