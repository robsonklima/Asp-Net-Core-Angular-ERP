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
    codSetor: number;
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
        ADM_DO_SISTEMA = 3,
        PV_COORDENADOR_DE_CONTRATO = 29,
        FILIAL_TECNICO_DE_CAMPO = 35,
        CLIENTE_AVANÇADO = 33,
    	CLIENTE_BASICO = 81,
        CLIENTE_INTERMEDIARIO = 93,
        CONTABILIDADE_PAGAMENTOS = 66,
        PARCEIROS = 71,
        FILIAL_SUPORTE_TÉCNICO_CAMPO = 79,
        RASTREAMENTO = 83,
        ANALISTA = 100,
    	ASSISTENTE = 107,
    	LIDER = 108,
    	COORDENADOR = 103,
    	SUPERVISOR = 104,
    	TECNICO = 105,
        TECNICO_OPERACOES = 106,
        INSPETOR = 109
}

export enum RoleGroup {
    TECNICOS = "32, 35, 61, 79, 84",
}

export class SegurancaUsuarioModel {
    public senhaAtual: string;
    public novaSenha: string;
    public codUsuario: string;
}