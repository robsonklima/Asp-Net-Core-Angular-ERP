using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Conexao")]
    public partial class Conexao
    {
        public Conexao()
        {
            Relatorios = new HashSet<Relatorio>();
        }

        [Key]
        public int CodConexao { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeConexao { get; set; }
        [StringLength(300)]
        public string StringConexao { get; set; }

        [InverseProperty(nameof(Relatorio.CodConexaoNavigation))]
        public virtual ICollection<Relatorio> Relatorios { get; set; }
    }
}
