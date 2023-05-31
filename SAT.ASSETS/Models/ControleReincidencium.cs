using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ControleReincidencium
    {
        [Key]
        public int CodControleReincidencia { get; set; }
        public int? CodCliente { get; set; }
        public int NumReincidencia { get; set; }
        [StringLength(1000)]
        public string Email { get; set; }
        public byte? IndAvisoFilial { get; set; }
        [Column("IndAvisoPAT")]
        public byte? IndAvisoPat { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.ControleReincidencia))]
        public virtual Cliente CodClienteNavigation { get; set; }
    }
}
