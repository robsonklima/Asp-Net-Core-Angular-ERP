using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class ItemDefeito
    {
        public int CodItemDefeito { get; set; }
        public string CodTecnico { get; set; }
        public int CodORItem { get; set; }
        public int CodDefeito { get; set; }
        public DateTime DataHoraCad { get; set; }
        public Usuario Usuario { get; set; }
        public ORItem ORItem { get; set; }
        public ORDefeito ORDefeito { get; set; }
    }
}