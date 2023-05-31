using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingIndiceDePecasPendentesGeovane
    {
        public int CodTipoEquip { get; set; }
        public int CodCliente { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        public int CodRegiao { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        public int ChamadosMes { get; set; }
        public int ChamadosMesPecasPendentes { get; set; }
        public int? HasPendencia { get; set; }
        [Column("ProtocoloSTN")]
        public int? ProtocoloStn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPendencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFaltante { get; set; }
        [StringLength(30)]
        public string Dia { get; set; }
        [StringLength(50)]
        public string Tecnico { get; set; }
        public int? CodTecnico { get; set; }
        [Column("CPF")]
        [StringLength(20)]
        public string Cpf { get; set; }
        public int CodStatusServico { get; set; }
    }
}
