using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MobileVersao")]
    public partial class MobileVersao
    {
        [Key]
        public int CodMobileVersao { get; set; }
        [Required]
        [StringLength(15)]
        public string Version { get; set; }
        [Required]
        [StringLength(10)]
        public string Protocol { get; set; }
        [Required]
        [StringLength(50)]
        public string HostName { get; set; }
        [Required]
        [StringLength(30)]
        public string RemotePath { get; set; }
        [Required]
        [StringLength(30)]
        public string RemoteFile { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
        [StringLength(30)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string LocalPath { get; set; }
        public byte DeleteLocalFiles { get; set; }
        public byte RunOnStartUp { get; set; }
        [Required]
        [StringLength(10)]
        public string TipoAplicacao { get; set; }
    }
}
