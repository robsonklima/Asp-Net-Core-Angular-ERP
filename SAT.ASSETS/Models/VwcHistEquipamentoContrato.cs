using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcHistEquipamentoContrato
    {
        public int CodEquipContrato { get; set; }
        [StringLength(20)]
        public string NumSerieEquipamento { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCliente { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [StringLength(50)]
        public string NomeAutorizada { get; set; }
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [Required]
        [StringLength(3)]
        public string Receita { get; set; }
        [Required]
        [StringLength(3)]
        public string Repasse { get; set; }
        [Required]
        [StringLength(3)]
        public string Garantia { get; set; }
        [Required]
        [StringLength(3)]
        public string Ativo { get; set; }
        [StringLength(20)]
        public string UsuarioCadastro { get; set; }
        [StringLength(20)]
        public string UsuarioAlteracao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}
