namespace SAT.SERVICES.Interfaces
{
    public interface IEmailService
    {
        void Enviar(string nomeRemetente,
                    string emailRemetente,
                    string nomeDestinatario,
                    string emailDestinatario,
                    string assunto,
                    string corpo);
    }
}