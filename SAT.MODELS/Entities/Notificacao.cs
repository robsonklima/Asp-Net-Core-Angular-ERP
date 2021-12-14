using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Notificacao
    {
        [Key]
        public int? CodNotificacao { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
        public byte? Lida { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioManut { get; set; }
        public string Link { get; set; }
        public byte? IndAtivo { get; set; }
        public string CodUsuario { get; set; }
    }
}
