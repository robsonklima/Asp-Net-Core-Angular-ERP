using System;

namespace SAT.MODELS.Entities
{
    public class ContratoEquipamento
    {
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public TipoEquipamento TipoEquipamento { get; set; }
        public int CodGrupoEquip { get; set; }
        public GrupoEquipamento GrupoEquipamento { get; set; }
        public int CodEquip { get; set; }
        public Equipamento Equipamento { get; set; }
        public int QtdEquip { get; set; }
        public decimal VlrUnitario { get; set; }
        public int CodTipoGarantia { get; set; }
        public DateTime? DataRecDM { get; set; }
        public decimal? VlrInstalacao { get; set; }
        public byte? IndGarPriSem { get; set; }
        public byte? IndGarSegSem { get; set; }
        public byte? IndGarTerSem { get; set; }
        public byte? IndGarQuaSem { get; set; }
        public byte? IndGarPriQui { get; set; }
        public byte? IndGarSegQui { get; set; }
        public int QtdDiaGarantia { get; set; }
        public int QtdLimDiaEnt { get; set; }
        public int? QtdLimDiaIns { get; set; }
        public int? CodContratoEquipDataGar { get; set; }
        public int CodContratoEquipDataEnt { get; set; }
        public int? CodContratoEquipDataIns { get; set; }
        public DateTime? DataGar { get; set; }
        public decimal? PercIPI { get; set; }
        public decimal PercValorEnt { get; set; }
        public decimal PercValorIns { get; set; }
        public DateTime? DataInicioMTBF { get; set; }
        public DateTime? DataFimMTBF { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodMagnus { get; set; }
    }
}
