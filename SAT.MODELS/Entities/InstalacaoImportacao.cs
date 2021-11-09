using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities{
    public class InstalacaoImportacao
    {
        [Key]
        public int CodInstalacaoImportacao { get; set; }
        public int CodInstalacao { get; set; }
        public DateTime? Datainstalacao { get; set; }
        public DateTime? DataAgendada { get; set; }
        public string Status { get; set; }
        public int? BorderosInstalados { get; set; }
        public double? ValoresBorderos { get; set; }
        public string StatusDocumentos { get; set; }
        public double? ValorEquipamento { get; set; }
        public string CodUsuarioCad { get; set; }
        public string Frustrado { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}