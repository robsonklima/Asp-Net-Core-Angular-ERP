import { Filial } from "../types/filial.types";
import { FiltroUsuarioData } from "../types/filtro.types";
import { Navegacao } from "../types/navegacao.types";

export interface UserSession {
    usuario: User;
    navegacoes: Navegacao[];
    token: string;
}

export interface User {
    codUsuario: string;
    codFilial?: number;
    filial?: Filial;
    codAutorizada?: any;
    codTecnico?: any;
    codCliente?: any;
    cliente?: any;
    codCargo: number;
    codDepartamento?: any;
    codTurno?: any;
    codCidade: number;
    cidade?: any;
    codFilialPonto?: any;
    codFusoHorario: number;
    codLingua: number;
    codPerfil: number;
    perfil: any;
    codSmartCard: number;
    codContrato: string;
    dataAdmissao: Date;
    nomeUsuario: string;
    codPeca: number;
    cpf: string;
    cep: string;
    endereco: string;
    bairro: string;
    fone: string;
    fax?: any;
    ramal?: any;
    email?: any;
    numCracha?: any;
    codRelatorioNaoMostrado: string;
    instalPerfilPagina?: any;
    senha: string;
    indAtivo: number;
    indAssinaInvoice?: any;
    codUsuarioCad: string;
    dataHoraCad: Date;
    codUsuarioManut: string;
    dataHoraManut: Date;
    indPonto: number;
    indBloqueio?: any;
    numero?: any;
    complemento?: any;
    codTransportadora?: any;
    indPermiteRegistrarEquipPOS: boolean;
    navegacoes: Navegacao[];
    localizacoes: any[];
    tecnico: any;
    avatar?: string;
    status?: string;
    filtroUsuario?: FiltroUsuarioData[];
}

export enum RoleEnum {
    ADMIN = 3,
    FILIAL_COORDENADOR = 5,
    FINANCEIRO_COORDENADOR = 16,
    PV_COORDENADOR_DE_CONTRATO = 29,
    FILIAL_SUPORTE_TECNICO = 32,
    CLIENTE = 34,
    FILIAL_TECNICO_DE_CAMPO = 35,
    CLIENTE_BASICO = 40,
    FINANCEIRO_ADMINISTRATIVO = 59,
    BANCADA_TECNICO = 61,
    PONTO_FINANCEIRO = 65,
    FINANCEIRO_COORDENADOR_PONTO = 70,
    FILIAIS_SUPERVISOR = 75,
    FILIAL_SUPORTE_TECNICO_CAMPO = 79,
    CLIENTE_BASICO_S_ABERTURA = 81,
    FILIAL_LIDER = 82,
    FILIAL_SUPORTE_TECNICO_CAMPO_COM_CHAMADOS = 84,
    CLIENTE_CORREIOS = 87,
    CLIENTE_BASICO_C_RESTRICOES = 90,
    CLIENTE_BASICO_BIOMETRIA = 93,
    FINANCEIRO_COORDENADOR_CREDITO = 100,
    FILIAL_LIDER_DE_SETOR = 80,    
    PV_CENTRAL_ATENDENTE = 2,
    PLANTAO_HELP_DESK = 89
}

export enum RoleGroup {
    TECNICOS = "32, 35, 61, 79, 84",
}

export class SegurancaUsuarioModel {
    public senhaAtual: string;
    public novaSenha: string;
    public codUsuario: string;
}