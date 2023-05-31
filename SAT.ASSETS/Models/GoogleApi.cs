using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("GoogleApi")]
    public partial class GoogleApi
    {
        public int CodGoogleApi { get; set; }
        [StringLength(50)]
        public string LatOrigem { get; set; }
        [StringLength(50)]
        public string LngOrigem { get; set; }
        [StringLength(350)]
        public string EnderecoOrigem { get; set; }
        [StringLength(50)]
        public string LatDestino { get; set; }
        [StringLength(50)]
        public string LngDestino { get; set; }
        [StringLength(350)]
        public string EnderecoDestino { get; set; }
        [StringLength(50)]
        public string Distancia { get; set; }
        [StringLength(50)]
        public string Duracao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
