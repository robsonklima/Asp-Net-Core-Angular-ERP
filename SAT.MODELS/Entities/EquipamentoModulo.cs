using System;

namespace SAT.MODELS.Entities
{
    public class EquipamentoModulo
    {
        public int CodConfigEquipModulos { get; set; }
        public int CodEquip { get; set; }
        public string CodECausa { get; set; }
        public string CodUsuarioCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? IndAtivo { get; set; }
        public Equipamento Equipamento { get; set; }
        public Causa Causa { get; set; }
    }
}
