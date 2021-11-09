using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalRessalva
    {
        [Key]
        public int CodInstalRessalva { get; set; }
        public int CodInstalacao { get; set; }
        public string CodUsuarioCad { get; set; }
        public int CodInstalMotivoRes { get; set; }
        public string Comentario { get; set; }
        public DateTime DataHoraCad { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public byte IndAtivo { get; set; }
        public byte IndJustificativa { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }

    }
}