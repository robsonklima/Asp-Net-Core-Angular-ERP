using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class GrupoEquipamento
    {
        [Key]
        public int? CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [ForeignKey("CodTipoEquip")]
        public TipoEquipamento TipoEquipamento { get; set; }
        public string CodEGrupoEquip { get; set; }
        public string NomeGrupoEquip { get; set; }
    }
}
