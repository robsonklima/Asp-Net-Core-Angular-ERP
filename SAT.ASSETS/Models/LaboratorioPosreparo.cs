using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LaboratorioPOSReparo")]
    public partial class LaboratorioPosreparo
    {
        [Key]
        [Column("CodLaboratorioPOSReparo")]
        public int CodLaboratorioPosreparo { get; set; }
        public int CodItemReparoRede { get; set; }
        [Column("CodLaboratorioPOSItem")]
        public int CodLaboratorioPositem { get; set; }

        [ForeignKey(nameof(CodItemReparoRede))]
        [InverseProperty(nameof(ItemReparoRede.LaboratorioPosreparos))]
        public virtual ItemReparoRede CodItemReparoRedeNavigation { get; set; }
        [ForeignKey(nameof(CodLaboratorioPositem))]
        [InverseProperty(nameof(LaboratorioPositem.LaboratorioPosreparos))]
        public virtual LaboratorioPositem CodLaboratorioPositemNavigation { get; set; }
    }
}
