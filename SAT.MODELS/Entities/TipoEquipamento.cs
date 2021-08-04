using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class TipoEquipamento
    {
        [Key]
        public int? CodTipoEquip { get; set; }
        public string CodETipoEquip { get; set; }
        public string NomeTipoEquip { get; set; }
    }
}
