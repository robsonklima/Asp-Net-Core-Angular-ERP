using System;
namespace SAT.MODELS.Entities
{
    public class InstalacaoPleitoInstal
    {
        public int? CodInstalacao { get; set; }
        public int? CodInstalPleito { get; set; }
        public int? CodEquipContrato { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public Instalacao Instalacao { get; set; }
    }
}