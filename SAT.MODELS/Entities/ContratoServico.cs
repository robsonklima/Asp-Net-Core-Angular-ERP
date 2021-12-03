using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class ContratoServico
    {
        [Key]
        public int CodContratoServico { get; set; }
        public int? CodContrato { get; set; }
        public int CodServico { get; set; }
        public int? CodSLA { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        public decimal Valor { get; set; }
        public string CodUsuarioCad { get; set; }
        public string DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public string DataHoraManut { get; set; }
        public string CodUsuarioCadastroDel { get; set; }
        public string DataHoraCadastroDel { get; set; }
        public string CodUsuarioManutencaoDel { get; set; }
        public string DataHoraManutencaoDel { get; set; }
    }
}