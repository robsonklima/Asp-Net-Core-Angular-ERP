using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingIndiceDeAtendimento
    {
        public int CodTipoEquip { get; set; }
        public int CodCliente { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        public int CodTecnico { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int AtendimentosMes { get; set; }
        public int AtendimentosAno { get; set; }
        public int HorasMes { get; set; }
        public int HorasAno { get; set; }
    }
}
