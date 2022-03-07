using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class UsuarioSeguranca
    {
        [Key]
        public string CodUsuario { get; set; }
        public byte? SenhaBloqueada { get; set; }
        public byte? SenhaExpirada { get; set; }
        public int QuantidadeTentativaLogin { get; set; }
        public string CodUsuarioCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime DataHoraCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public byte? IndAtivo { get; set; }
    }
}