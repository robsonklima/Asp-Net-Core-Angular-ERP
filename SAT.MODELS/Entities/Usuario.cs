using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SAT.MODELS.Entities
{
    public class Usuario
    {
        [Key]
        public string CodUsuario { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodCargo { get; set; }
        public int? CodDepartamento { get; set; }
        public int? CodTurno { get; set; }
        public int? CodCidade { get; set; }
        public int? CodFilialPonto { get; set; }
        public int CodFusoHorario { get; set; }
        public int CodLingua { get; set; }
        public int? CodPerfil { get; set; }
        public int? CodSmartCard { get; set; }
        public string CodContrato { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public string NomeUsuario { get; set; }
        public byte? CodPeca { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public string Ramal { get; set; }
        public string Email { get; set; }
        public string NumCracha { get; set; }
        public string CodRelatorioNaoMostrado { get; set; }
        public string InstalPerfilPagina { get; set; }
        public byte IndAtivo { get; set; }
        public byte? IndAssinaInvoice { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public byte? IndPonto { get; set; }
        public byte? IndBloqueio { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int? CodTransportadora { get; set; }
        public bool? IndPermiteRegistrarEquipPOS { get; set; }
        

        [JsonIgnore]
        public string Senha { get; set; }

        
        [ForeignKey("CodFilial")]
        public Filial Filial { get; set; }

        [ForeignKey("CodTecnico")]
        public Tecnico Tecnico { get; set; }

        public int? CodCliente { get; set; }
        [ForeignKey("CodCliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("CodCidade")]
        public Cidade Cidade { get; set; }

        [ForeignKey("CodPerfil")]
        public Perfil Perfil { get; set; }

        [ForeignKey("CodCargo")]
        public Cargo Cargo { get; set; }

        [ForeignKey("CodTurno")]
        public Turno Turno { get; set; }

        [ForeignKey("CodUsuario")]
        public List<Localizacao> Localizacoes { get; set; }

        [ForeignKey("CodUsuario")]
        public List<PontoPeriodoUsuario> PontosPeriodoUsuario { get; set; }

        [ForeignKey("CodFilialPonto")]
        [Column("CodFilial")]
        public Filial FilialPonto { get; set; }
    }
}
