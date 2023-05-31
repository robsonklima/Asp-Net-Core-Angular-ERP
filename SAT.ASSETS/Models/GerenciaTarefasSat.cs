using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("GerenciaTarefasSAT")]
    public partial class GerenciaTarefasSat
    {
        [Key]
        [Column("codGerenciaTarefasSAT")]
        public int CodGerenciaTarefasSat { get; set; }
        [StringLength(250)]
        public string Titulo { get; set; }
        [StringLength(50)]
        public string Usuario { get; set; }
        [StringLength(50)]
        public string Data { get; set; }
        [StringLength(50)]
        public string Minutos { get; set; }
        [StringLength(250)]
        public string Tarefa { get; set; }
        [StringLength(250)]
        public string Atividade { get; set; }
        [StringLength(50)]
        public string Projeto { get; set; }
        [StringLength(50)]
        public string TipoTarefa { get; set; }
        [StringLength(50)]
        public string Complexidade { get; set; }
        [StringLength(50)]
        public string Tecnologia { get; set; }
        [StringLength(50)]
        public string TipoAtendimento { get; set; }
    }
}
