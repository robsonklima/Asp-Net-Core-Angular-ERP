using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class Moedum
    {
        public Moedum()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        public int CodMoeda { get; set; }
        [Required]
        [StringLength(10)]
        public string NomeMoeda { get; set; }
        [Required]
        [StringLength(2)]
        public string SiglaMoeda { get; set; }

        [InverseProperty(nameof(Cliente.CodMoedaNavigation))]
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
