using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcEnderecoSatCoordenadum
    {
        public int? CodPosto { get; set; }
        [StringLength(10)]
        public string Agencia { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(50)]
        public string Nomelocal { get; set; }
        [StringLength(150)]
        public string EnderecoSat { get; set; }
        [Required]
        [StringLength(50)]
        public string CidadeSat { get; set; }
        [Required]
        [Column("UFSat")]
        [StringLength(50)]
        public string Ufsat { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [StringLength(100)]
        public string EnderecoCoordenada { get; set; }
        [StringLength(60)]
        public string CidadeCoordenada { get; set; }
        [Column("UFCoordenadas")]
        [StringLength(2)]
        public string Ufcoordenadas { get; set; }
        [Required]
        [StringLength(3)]
        public string PossuiEquipamentoAtivo { get; set; }
    }
}
