using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LocalizacaoConf")]
    public partial class LocalizacaoConf
    {
        [StringLength(50)]
        public string CodUsuario { get; set; }
        [StringLength(300)]
        public string Filiais { get; set; }
        public int? IntervaloCarregamento { get; set; }
        public int? Zoom { get; set; }
    }
}
