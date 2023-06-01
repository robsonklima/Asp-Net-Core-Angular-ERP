using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EmailSAT")]
    public partial class EmailSat
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTimeReceived { get; set; }
        public string SendTo { get; set; }
        [Column("SendCC")]
        public string SendCc { get; set; }
        public string SendFrom { get; set; }
    }
}
