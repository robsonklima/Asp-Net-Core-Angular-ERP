using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwExportaChamadosArquivoMorto
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("NUMAGENCIA")]
        [StringLength(5)]
        public string Numagencia { get; set; }
        [Column("CODPOSTO")]
        public int? Codposto { get; set; }
        [Column("NOMELOCAL")]
        [StringLength(50)]
        public string Nomelocal { get; set; }
        [Column("ENDERECO")]
        [StringLength(150)]
        public string Endereco { get; set; }
        [Column("NOMECIDADE")]
        [StringLength(50)]
        public string Nomecidade { get; set; }
        [Column("NOMEUF")]
        [StringLength(50)]
        public string Nomeuf { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [Column("MODELO_EQUIPAMENTO")]
        [StringLength(50)]
        public string ModeloEquipamento { get; set; }
    }
}
