using System;

namespace SAT.MODELS.Entities
{
    public class ItemSolucao
    {
        public int CodItemSolucao { get; set; }
        public string CodTecnico { get; set; }
        public int CodORItem { get; set; }
        public int CodSolucao { get; set; }
        public DateTime DataHoraCad { get; set; }
        public Usuario Usuario { get; set; }
        public ORItem ORItem { get; set; }
        public ORSolucao ORSolucao { get; set; }
    }
}