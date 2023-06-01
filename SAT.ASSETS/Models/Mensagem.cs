using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Mensagem")]
    public partial class Mensagem
    {
        public Mensagem()
        {
            MensagemUsuarios = new HashSet<MensagemUsuario>();
        }

        [Key]
        public int CodMensagem { get; set; }
        [Required]
        [StringLength(100)]
        public string Assunto { get; set; }
        [Required]
        [Column("Mensagem", TypeName = "text")]
        public string Mensagem1 { get; set; }
        public byte IndVisto { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(MensagemUsuario.CodMensagemNavigation))]
        public virtual ICollection<MensagemUsuario> MensagemUsuarios { get; set; }
    }
}
