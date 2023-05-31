using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class Maquina
    {
        [StringLength(255)]
        public string TipoPonto { get; set; }
        [StringLength(255)]
        public string ModPonto { get; set; }
        [Column("NivelCriticidadeNC")]
        [StringLength(255)]
        public string NivelCriticidadeNc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorInicioFunc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorFimFunc { get; set; }
        [StringLength(255)]
        public string FuncSabado { get; set; }
        [StringLength(255)]
        public string FuncDomingo { get; set; }
        public double? CodEquip { get; set; }
        [StringLength(255)]
        public string NomeLogico { get; set; }
        [StringLength(255)]
        public string NomePonto { get; set; }
        [StringLength(255)]
        public string Endereco { get; set; }
        [StringLength(255)]
        public string Complemento { get; set; }
        [StringLength(255)]
        public string Bairro { get; set; }
        [Column("CEP")]
        [StringLength(255)]
        public string Cep { get; set; }
        [StringLength(255)]
        public string Cidade { get; set; }
        [StringLength(255)]
        public string Filial { get; set; }
        [StringLength(255)]
        public string Marca { get; set; }
        [StringLength(255)]
        public string Modelo { get; set; }
        public double? NumSerie { get; set; }
        public double? UnidVinc { get; set; }
        [Column("CGCGITEC")]
        public double? Cgcgitec { get; set; }
        [Column("GITECVinc")]
        [StringLength(255)]
        public string Gitecvinc { get; set; }
        [Column("CGCSR")]
        public double? Cgcsr { get; set; }
        [Column("SRVinc")]
        [StringLength(255)]
        public string Srvinc { get; set; }
        [Column("CGCSUAT")]
        public double? Cgcsuat { get; set; }
        [Column("SUAT")]
        [StringLength(255)]
        public string Suat { get; set; }
    }
}
