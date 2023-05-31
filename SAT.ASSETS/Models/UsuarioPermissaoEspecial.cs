using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("UsuarioPermissaoEspecial")]
    public partial class UsuarioPermissaoEspecial
    {
        [Key]
        public int CodUsuarioPermissaoEspecial { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column("PermiteGerarOSReincidente")]
        public bool? PermiteGerarOsreincidente { get; set; }
        public bool? ReiniciarServicoBanrisul { get; set; }
        public bool? CadastrarNovoChamado { get; set; }
        public bool? RelatorioDiario { get; set; }
        public bool? ControleEquipamentos { get; set; }
        [Column("DashBoardHD")]
        public bool? DashBoardHd { get; set; }
        public bool? FaturamentoSicredi { get; set; }
        public bool? EquipamentosBanrisul { get; set; }
        public bool? CadastrarPatrimonio { get; set; }

        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.UsuarioPermissaoEspecial))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
