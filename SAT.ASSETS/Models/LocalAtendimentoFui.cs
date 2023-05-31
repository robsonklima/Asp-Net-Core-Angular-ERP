using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LocalAtendimentoFui")]
    public partial class LocalAtendimentoFui
    {
        public int? Codposto { get; set; }
        public string Loc { get; set; }
        [StringLength(50)]
        public string Cod { get; set; }
        [StringLength(60)]
        public string Reg { get; set; }
        public string Cli { get; set; }
        [StringLength(10)]
        public string Ag { get; set; }
        [StringLength(10)]
        public string Dc { get; set; }
        [StringLength(20)]
        public string Cep { get; set; }
        [Column("Cep_3")]
        [StringLength(20)]
        public string Cep3 { get; set; }
        [StringLength(80)]
        public string Distrito { get; set; }
        [Column("Z_Urb")]
        [StringLength(50)]
        public string ZUrb { get; set; }
        public string Cidade { get; set; }
        [StringLength(20)]
        public string Uf { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        [StringLength(20)]
        public string Setor { get; set; }
        public string Tecnico { get; set; }
        [Column("Tipo_Endereco")]
        [StringLength(10)]
        public string TipoEndereco { get; set; }
        public string Endereco { get; set; }
        [StringLength(10)]
        public string Numero { get; set; }
        [Column("Usuario_Tecnico_Fui")]
        [StringLength(300)]
        public string UsuarioTecnicoFui { get; set; }
    }
}
