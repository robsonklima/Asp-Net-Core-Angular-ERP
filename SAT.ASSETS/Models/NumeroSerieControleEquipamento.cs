using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class NumeroSerieControleEquipamento
    {
        [Key]
        public int CodNumeroSerieControleEquipamentos { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroSerie { get; set; }
        public int CodNumeroSerieControleTitulo { get; set; }
        public bool Ativo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodNumeroSerieControleTitulo))]
        [InverseProperty(nameof(NumeroSerieControleTitulo.NumeroSerieControleEquipamentos))]
        public virtual NumeroSerieControleTitulo CodNumeroSerieControleTituloNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.NumeroSerieControleEquipamentos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
    }
}
