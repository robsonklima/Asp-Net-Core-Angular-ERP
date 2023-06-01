using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MensagemUsuario")]
    public partial class MensagemUsuario
    {
        [Key]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Key]
        public int CodMensagem { get; set; }
        public byte IndVisto { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodMensagem))]
        [InverseProperty(nameof(Mensagem.MensagemUsuarios))]
        public virtual Mensagem CodMensagemNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.MensagemUsuarios))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
