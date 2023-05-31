using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("BanrisulInterface")]
    public partial class BanrisulInterface
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("IDTipoArquivo")]
        public int IdtipoArquivo { get; set; }
        [Column("IDTipoRegistro")]
        public int IdtipoRegistro { get; set; }
        [Required]
        [StringLength(255)]
        public string NomeCampo { get; set; }
        public int Tamanho { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
        public byte Obrigatorio { get; set; }
        [Required]
        public string Descricao { get; set; }

        [ForeignKey(nameof(IdtipoArquivo))]
        [InverseProperty(nameof(BanrisulTipoArquivo.BanrisulInterfaces))]
        public virtual BanrisulTipoArquivo IdtipoArquivoNavigation { get; set; }
        [ForeignKey(nameof(IdtipoRegistro))]
        [InverseProperty(nameof(BanrisulTipoRegistro.BanrisulInterfaces))]
        public virtual BanrisulTipoRegistro IdtipoRegistroNavigation { get; set; }
    }
}
