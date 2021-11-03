import { Filial } from "../types/filial.types";
import { Navegacao } from "../types/navegacao.types";

export interface UserSession
{
    usuario: User;
    navegacoes: Navegacao[];
    token: string;
}

export interface User
{
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
}
