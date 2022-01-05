
export interface Email
{
    emailRemetente: string;
    nomeRemetente: string;
    nomeCC?: string;
    emailCC?: string;
    emailDestinatario: string;
    nomeDestinatario?: string;
    assunto: string;
    corpo: string;
}

export interface EmailAddress
{
    address: string;
}