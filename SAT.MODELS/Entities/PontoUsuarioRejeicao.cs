using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioRejeicao
    {
        [Key]
        public int CodPontoUsuarioRejeicao { get; set; }
        public int? CodRetorno { get; set; }
        public int? CodPontoPeriodo { get; set; }
        public string ChaveSeguranca { get; set; }
        public string ImeiCriptografado { get; set; }
        public string CodUsuario { get; set; }
        public DateTime? DataHoraRegistro { get; set; }
        public DateTime? DataHoraEnvio { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
        public byte? IndAprovado { get; set; }
    }
}