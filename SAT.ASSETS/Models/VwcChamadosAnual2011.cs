using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcChamadosAnual2011
    {
        [Column("Número do chamado")]
        public int NúmeroDoChamado { get; set; }
        [Column("Data da abertura do chamado", TypeName = "datetime")]
        public DateTime? DataDaAberturaDoChamado { get; set; }
        [Column("Tipo da intervenção")]
        [StringLength(50)]
        public string TipoDaIntervenção { get; set; }
        [Required]
        [Column("Nome do cliente")]
        [StringLength(50)]
        public string NomeDoCliente { get; set; }
        [Required]
        [Column("Nome da agência")]
        [StringLength(50)]
        public string NomeDaAgência { get; set; }
        [Required]
        [Column("Modelo Equipamento")]
        [StringLength(50)]
        public string ModeloEquipamento { get; set; }
        [Column("Nome do contrato")]
        [StringLength(30)]
        public string NomeDoContrato { get; set; }
        [Column("Data da conclusão do chamado", TypeName = "datetime")]
        public DateTime? DataDaConclusãoDoChamado { get; set; }
        [Required]
        [Column("Equipamento em garantia?")]
        [StringLength(3)]
        public string EquipamentoEmGarantia { get; set; }
    }
}
