using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ProtocoloSTN")]
    public partial class ProtocoloStn
    {
        [Key]
        [Column("CodProtocoloSTN")]
        public int CodProtocoloStn { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int NumProtocolo { get; set; }
    }
}
