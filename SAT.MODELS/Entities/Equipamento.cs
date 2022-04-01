namespace SAT.MODELS.Entities
{
    public class Equipamento
    {
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        public string CodEEquip { get; set; }
        public string NomeEquip { get; set; }
        public string DescEquip { get; set; }
        public Equivalencia Equivalencia { get; set; }
        public GrupoEquipamento GrupoEquipamento { get; set; }
        public TipoEquipamento TipoEquipamento { get; set; }
    }
}
