using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcUsuarioAtivo
    {
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodCliente { get; set; }
        public int? CodCargo { get; set; }
        public int? CodDepartamento { get; set; }
        public int? CodTurno { get; set; }
        public int? CodCidade { get; set; }
        public int? CodFilialPonto { get; set; }
        public int CodFusoHorario { get; set; }
        public int CodLingua { get; set; }
        public int? CodPerfil { get; set; }
        public int? CodSmartCard { get; set; }
        [StringLength(150)]
        public string CodContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAdmissao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        public byte? CodPeca { get; set; }
        [Column("CPF")]
        [StringLength(20)]
        public string Cpf { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(30)]
        public string Bairro { get; set; }
        [StringLength(40)]
        public string Fone { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(20)]
        public string Ramal { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(6)]
        public string NumCracha { get; set; }
        [StringLength(100)]
        public string CodRelatorioNaoMostrado { get; set; }
        [StringLength(2000)]
        public string InstalPerfilPagina { get; set; }
        [Required]
        [StringLength(255)]
        public string Senha { get; set; }
        public byte IndAtivo { get; set; }
        public byte? IndAssinaInvoice { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public byte? IndPonto { get; set; }
        public byte? IndBloqueio { get; set; }
        [StringLength(50)]
        public string Numero { get; set; }
        [StringLength(50)]
        public string Complemento { get; set; }
        public int? CodTransportadora { get; set; }
        [Column("IndPermiteRegistrarEquipPOS")]
        public bool? IndPermiteRegistrarEquipPos { get; set; }
    }
}
