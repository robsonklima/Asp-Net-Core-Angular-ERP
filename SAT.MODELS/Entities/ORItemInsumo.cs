using System;

namespace SAT.MODELS.Entities {
    public class ORItemInsumo
    {
        public int CodORItemInsumo { get; set; }
        public int? CodORItem { get; set; }
        public DateTime? DataHoraOritem { get; set; }
        public int? CodOR { get; set; }
        public int? CodPeca { get; set; }
        public int? CodStatus { get; set; }
        public int? Quantidade { get; set; }
        public string NumSerie { get; set; }
        public int? CodTipoOR { get; set; }
        public int? CodOS { get; set; }
        public int? CodCliente { get; set; }
        public string CodTecnico { get; set; }
        public string DefeitoRelatado { get; set; }
        public string RelatoSolucao { get; set; }
        public string CodDefeito { get; set; }
        public int? CodAcao { get; set; }
        public int? CodSolucao { get; set; }
        public byte? IndConfLog { get; set; }
        public byte? IndConfLab { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public int? CodStatusPendente { get; set; }
        public int? IndLiberacao { get; set; }
        public Peca Peca { get; set; }
        public ORStatus ORStatus { get; set; }

    }
}