using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("NumeroSerieControleTitulo")]
    public partial class NumeroSerieControleTitulo
    {
        public NumeroSerieControleTitulo()
        {
            NumeroSerieControleEquipamentos = new HashSet<NumeroSerieControleEquipamento>();
        }

        [Key]
        public int CodNumeroSerieControleTitulo { get; set; }
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        public bool Ativo { get; set; }
        [StringLength(5000)]
        public string Emails { get; set; }

        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.NumeroSerieControleTitulos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [InverseProperty(nameof(NumeroSerieControleEquipamento.CodNumeroSerieControleTituloNavigation))]
        public virtual ICollection<NumeroSerieControleEquipamento> NumeroSerieControleEquipamentos { get; set; }
    }
}
