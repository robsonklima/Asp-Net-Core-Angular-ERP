using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwLocaisListagem
    {
        public int CodPosto { get; set; }
        [Required]
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [StringLength(150)]
        public string Endereco { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [StringLength(50)]
        public string Fone { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [Column("FILIAL")]
        [StringLength(50)]
        public string Filial { get; set; }
        [StringLength(50)]
        public string Regiao { get; set; }
        [StringLength(50)]
        public string Autorizada { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [Required]
        [StringLength(50)]
        public string Cliente { get; set; }
        [Required]
        [Column("Local_Status")]
        [StringLength(3)]
        public string LocalStatus { get; set; }
        [Column("Qtd_Equipamentos")]
        public int? QtdEquipamentos { get; set; }
    }
}
