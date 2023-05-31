using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaCartaoCombustivel")]
    public partial class DespesaCartaoCombustivel
    {
        [Key]
        public int CodDespesaCartaoCombustivel { get; set; }
        [StringLength(50)]
        public string Numero { get; set; }
        [StringLength(50)]
        public string Carro { get; set; }
        [StringLength(50)]
        public string Placa { get; set; }
        [StringLength(50)]
        public string Ano { get; set; }
        [StringLength(50)]
        public string Cor { get; set; }
        [StringLength(50)]
        public string Combustivel { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? IndAtivo { get; set; }
    }
}
