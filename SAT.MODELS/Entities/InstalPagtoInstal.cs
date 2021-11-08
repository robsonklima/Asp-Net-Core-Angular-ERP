using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalPagtoInstal
    {
        [Key]
        public int CodInstalacao { get; set; }
        public int CodInstalPagto { get; set; }
        public int CodInstalTipoParcela { get; set; }
        public decimal VlrParcela { get; set; }
        public int? CodInstalMotivoMulta { get; set; }
        public decimal? VlrMulta { get; set; }
        public byte IndEndossarMulta { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string Comentario { get; set; }
        public byte? IndImportacao { get; set; }
    }
}