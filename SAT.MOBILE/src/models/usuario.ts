import { UsuarioPerfil } from "./usuario-perfil";
import { Filial } from "./filial";

export class Usuario {
	codUsuario?: string;
	senha: string;
    codTecnico: number;
    nome: string;
    email: string;
    usuarioPerfil: UsuarioPerfil;
    filial: Filial;
    fone: string;
    fonePerto: string;
    cep: string;
    endereco: string;
    enderecoComplemento: string;
    bairro: string;
    cidade: string;
    siglaUF: string;
    numero: string;
    cpf: string;
    foto: string;
}