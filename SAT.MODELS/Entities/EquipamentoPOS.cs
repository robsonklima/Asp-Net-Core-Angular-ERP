using System;

namespace SAT.MODELS.Entities
{
    public class EquipamentoPOS
    {
        public int CodEquipamentoPOS { get; set; }
        public string NumeroSerie { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        public DateTime DataProducao { get; set; }
        public int OpPqm { get; set; }
        public int IdOpSerie { get; set; }
        public int CodStatusEquipamentoPOS { get; set; }
        public int CodTipoMidia { get; set; }
        public string CodEquipPqm { get; set; }
        public string NumeroSeriePqm { get; set; }
        public string NumeroLogico { get; set; }
    }
}