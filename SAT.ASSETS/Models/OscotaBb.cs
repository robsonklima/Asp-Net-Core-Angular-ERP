using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OSCotaBB")]
    public partial class OscotaBb
    {
        [Column("CodOSCotaBB")]
        public int CodOscotaBb { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
    }
}
