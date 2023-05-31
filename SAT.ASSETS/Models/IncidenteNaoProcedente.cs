using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IncidenteNaoProcedente")]
    public partial class IncidenteNaoProcedente
    {
        [Key]
        public int CodIncidenteNaoProcedente { get; set; }
        public int CodMotivoIncidenteNaoProcedente { get; set; }
        [Required]
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [StringLength(5000)]
        public string Observacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string UsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodMotivoIncidenteNaoProcedente))]
        [InverseProperty(nameof(MotivoIncidenteNaoProcedente.IncidenteNaoProcedentes))]
        public virtual MotivoIncidenteNaoProcedente CodMotivoIncidenteNaoProcedenteNavigation { get; set; }
        [ForeignKey(nameof(UsuarioCadastro))]
        [InverseProperty(nameof(Usuario.IncidenteNaoProcedentes))]
        public virtual Usuario UsuarioCadastroNavigation { get; set; }
    }
}
