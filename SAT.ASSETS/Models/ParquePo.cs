using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ParquePo
    {
        [StringLength(255)]
        public string CpfCnpjEstabelecimento { get; set; }
        [StringLength(255)]
        public string Estabelecimento { get; set; }
        [StringLength(255)]
        public string Cep { get; set; }
        [StringLength(255)]
        public string Uf { get; set; }
        [StringLength(255)]
        public string Cidade { get; set; }
        [StringLength(255)]
        public string Endereco { get; set; }
        public double? Numero { get; set; }
        [StringLength(255)]
        public string Complemento { get; set; }
        [StringLength(255)]
        public string Contato { get; set; }
        [StringLength(255)]
        public string TelefoneContato { get; set; }
        [StringLength(255)]
        public string Rede { get; set; }
        [StringLength(255)]
        public string Mnemonico { get; set; }
        [StringLength(255)]
        public string Estab { get; set; }
        [StringLength(255)]
        public string Terminal { get; set; }
        [StringLength(255)]
        public string TipoTerminal { get; set; }
        [StringLength(255)]
        public string Comunicação { get; set; }
        [StringLength(255)]
        public string TerminalHabilitado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInstalacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataDesabilitacao { get; set; }
        [StringLength(255)]
        public string Versao { get; set; }
    }
}
