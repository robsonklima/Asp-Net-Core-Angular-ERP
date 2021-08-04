using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Equipamento
    {
        [Key]
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        [ForeignKey("CodGrupoEquip")]
        public GrupoEquipamento GrupoEquipamento { get; set; }
        public int CodTipoEquip { get; set; }
        [ForeignKey("CodTipoEquip")]
        public TipoEquipamento TipoEquipamento { get; set; }
        public string CodEEquip { get; set; }
        public string NomeEquip { get; set; }
        public string DescEquip { get; set; }
    }
}
