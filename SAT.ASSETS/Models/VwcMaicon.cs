using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcMaicon
    {
        [Column("Código da Tarefa")]
        public int CódigoDaTarefa { get; set; }
        [Column("Nome do Usuário")]
        [StringLength(50)]
        public string NomeDoUsuário { get; set; }
        [Column("Data e Hora de cadastro", TypeName = "datetime")]
        public DateTime DataEHoraDeCadastro { get; set; }
        [Column("Descrição da Tarefa")]
        public string DescriçãoDaTarefa { get; set; }
    }
}
