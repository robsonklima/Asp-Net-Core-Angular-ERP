using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SAT.MODELS.Entities
{
    public class Usuario
    {
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
        public string FoneParticular { get; set; }
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
        public Transportadora Transportadora { get; set; }
        public bool? IndPermiteRegistrarEquipPOS { get; set; }
        public int? CodSetor { get; set; }
        public DateTime? UltimoAcesso { get; set; }
        [JsonIgnore]
        public string Senha { get; set; }
        public Filial Filial { get; set; }
        public Tecnico Tecnico { get; set; }
        public int? CodCliente { get; set; }
        public Cliente Cliente { get; set; }
        public Cidade Cidade { get; set; }
        public Perfil Perfil { get; set; }
        public Setor Setor { get; set; }
        public Cargo Cargo { get; set; }
        public Turno Turno { get; set; }
        public List<Localizacao> Localizacoes { get; set; }
        public List<PontoPeriodoUsuario> PontosPeriodoUsuario { get; set; }
        public virtual List<RecursoBloqueado> RecursosBloqueados { get; set; }
        public List<PontoUsuario> PontosUsuario { get; set; }
        public Filial FilialPonto { get; set; }
        public List<FiltroUsuario> FiltroUsuario { get; set; }
        public List<UsuarioDispositivo> UsuarioDispositivos { get; set; }
        public UsuarioSeguranca UsuarioSeguranca { get; set; }
        public List<UsuarioLogin> Acessos { get; set; }
        public ImagemPerfilModel Foto { get; set; }
        public virtual ICollection<NavegacaoConfiguracao> NavegacoesConfiguracao { get; set; }
    }
}