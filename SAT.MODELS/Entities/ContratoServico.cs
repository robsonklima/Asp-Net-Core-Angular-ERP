using System;

namespace SAT.MODELS.Entities
{
    public class ContratoServico
    {
        public int CodContratoServico { get; set; }
        public int? CodContrato { get; set; }
        public int CodServico { get; set; }
        public int? CodSLA { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        public decimal Valor { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioCadastroDel { get; set; }
        public DateTime? DataHoraCadastroDel { get; set; }
        public string CodUsuarioManutencaoDel { get; set; }
        public DateTime? DataHoraManutencaoDel { get; set; }
        public TipoServico TipoServico { get; set; }
    }
}