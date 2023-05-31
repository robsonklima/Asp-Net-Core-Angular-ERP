using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TecnicoAnalisador")]
    public partial class TecnicoAnalisador
    {
        public int CodPosto { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [StringLength(15)]
        public string Cep { get; set; }
        [StringLength(150)]
        public string Endereco { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [StringLength(50)]
        public string Uf { get; set; }
        [StringLength(50)]
        public string Numero { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
