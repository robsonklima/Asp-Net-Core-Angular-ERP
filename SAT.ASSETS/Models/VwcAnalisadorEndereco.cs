using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcAnalisadorEndereco
    {
        public int CodPosto { get; set; }
        public int CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string Local { get; set; }
        [StringLength(10)]
        public string Agência { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(150)]
        public string Endereço { get; set; }
        [Column("Endereço-Analisador")]
        [StringLength(150)]
        public string EndereçoAnalisador { get; set; }
        [Column("Numero-Analisador")]
        [StringLength(50)]
        public string NumeroAnalisador { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [Column("Bairro-Analisador")]
        [StringLength(100)]
        public string BairroAnalisador { get; set; }
        [Column("Bairro-Coordenadas")]
        [StringLength(60)]
        public string BairroCoordenadas { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [Column("Cidade-Analisador")]
        [StringLength(50)]
        public string CidadeAnalisador { get; set; }
        [Required]
        [Column("Comparação Cidade Analisador")]
        [StringLength(9)]
        public string ComparaçãoCidadeAnalisador { get; set; }
        [Required]
        [Column("Comparação Cidade Coordenadas")]
        [StringLength(9)]
        public string ComparaçãoCidadeCoordenadas { get; set; }
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [Column("UF-Analisador")]
        [StringLength(50)]
        public string UfAnalisador { get; set; }
        [Column("UF-Coordenadas")]
        [StringLength(2)]
        public string UfCoordenadas { get; set; }
        [Required]
        [Column("Comparação UF Analisador")]
        [StringLength(9)]
        public string ComparaçãoUfAnalisador { get; set; }
        [Required]
        [Column("Comparação UF Coordenadas")]
        [StringLength(9)]
        public string ComparaçãoUfCoordenadas { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [Column("Latitude-Analisador")]
        [StringLength(50)]
        public string LatitudeAnalisador { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [Column("Longitude-Analisador")]
        [StringLength(50)]
        public string LongitudeAnalisador { get; set; }
        [Required]
        [Column("Comparação Coordenadas")]
        [StringLength(9)]
        public string ComparaçãoCoordenadas { get; set; }
        [Required]
        [Column("Possui Equipamentos")]
        [StringLength(3)]
        public string PossuiEquipamentos { get; set; }
        [Required]
        [Column("Possui Equipamentos Ativos")]
        [StringLength(3)]
        public string PossuiEquipamentosAtivos { get; set; }
    }
}
