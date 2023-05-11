﻿namespace SAT.MODELS.Entities
{
    public class RelatorioAtendimentoPOS
    {
        public int CodRat { get; set; }
        public int CodRATbanrisul { get; set; }
        public string NumSerieInst { get; set; }
        public string NumSerieRet { get; set; }
        public string Rede { get; set; }
        public string NumeroChipInstalado { get; set; }
        public string NumeroChipRetirado { get; set; }
        public string ObsMotivoComunicacao { get; set; }
        public bool? AtendimentoRealizadoPorTelefone { get; set; }
        public string ObsMotivoCancelamento { get; set; }
        public byte? IndSmartphone { get; set; }

        public int? CodTipoComunicacao { get; set; }
        public int? CodOperadoraTelefoniaChipRetirado { get; set; }
        public int? CodEquipRet { get; set; }
        public Equipamento EquipRet { get; set; }
        public int? CodMotivoComunicacao { get; set; }
        public MotivoComunicacao MotivoComunicacao { get; set; }
        public int? CodOperadoraTelefoniaChipInstalado { get; set; }
        public OperadoraTelefonia OperadoraTelefoniaChipInstalado { get; set; }
        public int? CodEquipInst { get; set; }
        public Equipamento EquipInst { get; set; }
        public int? CodDefeitoPos { get; set; }
        public DefeitoPOS DefeitoPOS { get; set; }
        public int? CodMotivoCancelamento { get; set; }
        public MotivoCancelamento MotivoCancelamento { get; set; }
    }
}