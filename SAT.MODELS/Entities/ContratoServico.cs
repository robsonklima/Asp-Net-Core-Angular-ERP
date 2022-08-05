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
        public string DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public string DataHoraManut { get; set; }
        public string CodUsuarioCadastro_DEL { get; set; }
        public string DataHoraCadastro_DEL { get; set; }
        public string CodUsuarioManutencao_DEL { get; set; }
        public string DataHoraManutencao_DEL { get; set; }
        public TipoServico TipoServico { get; set; }
        public Contrato Contrato { get; set; }
    }
}