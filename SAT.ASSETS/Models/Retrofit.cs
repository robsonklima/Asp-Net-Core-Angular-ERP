using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Retrofit")]
    public partial class Retrofit
    {
        public int CodRetrofit { get; set; }
        [StringLength(80)]
        public string NomeRetrofit { get; set; }
        public string DescricaoRetrofit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataRetrofit { get; set; }
        public int? CodEquipContrato { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
