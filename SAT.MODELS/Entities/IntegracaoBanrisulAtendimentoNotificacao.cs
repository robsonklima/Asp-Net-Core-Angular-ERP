using System.Text;

namespace SAT.MODELS.Entities
{
    public enum ResolucaoEnum
    {
        RESOLUCAO_RECEBIDA = 1,
        RESOLUCAO_DATA_INVALIDA = 2,
        RESOLUCAO_RAT_NAO_ENCONTRADA = 3
    }

    public static class AtendimentoNotificacao
    {
        private static string GetHtmlEmailAbertura(IntegracaoBanrisulAtendimento atendimento, string mensagem)
        {
            StringBuilder texto = new StringBuilder();

            texto.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\" >");
            texto.Append("<html>");
            texto.Append("	<body>");
            texto.Append("			<font style=\"FONT-SIZE: 12pt\">");
            texto.Append("				<div align=\"center\">");
            texto.AppendFormat("            {0}", mensagem);
            texto.Append("				</div>");
            texto.Append("			</font>");
            texto.Append("			<br>");
            texto.Append("			<table style=\"BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid ;FONT-SIZE: 10pt; FONT-FAMILY: verdana\" align=\"center\">");
            texto.Append("				<tr>");
            texto.Append("					<td style=\"BACKGROUND-COLOR: #47699b; COLOR: white\" align=\"center\" colspan=\"2\">");
            texto.Append("						<font style=\"FONT-WEIGHT: bold\">");
            texto.Append("							Dados do chamado");
            texto.Append("						</font>");
            texto.Append("					</td>");
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Número Chamado:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.NumeroIncidente.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Agência:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.CodigoLocalEquipamento.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Posto:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.CodigoLocalEquipamento.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Número Série Equipamento:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.NumeroSerie.Valor);
            texto.Append("				</tr>");
            texto.Append("			</table>");
            texto.Append("	</body>");
            texto.Append("</html>");

            return texto.ToString();
        }

        public static void EnviaEmailAbertura(IntegracaoBanrisulAtendimento atendimento, OrdemServico ordemServico, bool reaberto = false)
        {
            string assunto = "Integração Banrisul ATM - Situação chamado cliente " +
                atendimento.NumeroIncidente.Valor + " - Analista " + atendimento.NomeContato.Valor;

            string status = reaberto ? "reaberto" : "cadastrado";
            string mensagem = "Chamado cliente " + atendimento.NumeroIncidente.Valor +
                " " + status + " com sucesso.<br>Numero do chamado cadastrado no SAT " + ordemServico.CodOS + "<br>";

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

            //clsGF.sendMail("equipe.sat@perto.com.br", Configuracao.DestinatarioEmailRetornoAbertura, "", assunto, clsGF.formatEmailMessage(texto), "html");
        }

        public static void EnviaEmailRecusaAbertura(IntegracaoBanrisulAtendimento atendimento, string motivo, string destinatario = "")
        {
            string assunto = "Integração Banrisul ATM - Situação chamado cliente " +
                atendimento.NumeroIncidente.Valor + " - Analista " + atendimento.NomeContato.Valor;

            string mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " recusado pois não atendeu aos seguintes requisitos: " + "<br/><ul><li>" + motivo + "</li></ul>";

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

            //clsGF.sendMail("equipe.sat@perto.com.br;", Configuracao.DestinatarioEmailRetornoRecusaAbertura + ";" + destinatario, "", assunto, clsGF.formatEmailMessage(texto), "html");
        }

        public static void EnviaEmailResolucao(IntegracaoBanrisulAtendimento atendimento, string motivo, string destinatario = "", ResolucaoEnum resolucaoEnum = ResolucaoEnum.RESOLUCAO_RECEBIDA)
        {
            string assunto = "Integração Banrisul ATM - Resolução do atendimento " + atendimento.NumeroIncidente.Valor;

            string mensagem = null;
            switch (resolucaoEnum)
            {
                case ResolucaoEnum.RESOLUCAO_RECEBIDA:
                    mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " - Resolução acatada com sucesso.";
                    break;
                case ResolucaoEnum.RESOLUCAO_DATA_INVALIDA:
                    mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " - Resolução não acatada, Data de resolução inválida.";
                    break;
                case ResolucaoEnum.RESOLUCAO_RAT_NAO_ENCONTRADA:
                    mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " - Resolução não acatada, RAT do chamado não encontrada.";
                    break;
            }

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

            //clsGF.sendMail("equipe.sat@perto.com.br;", Configuracao.DestinatarioEmailRetornoRecusaAbertura + ";" + destinatario, "", assunto, clsGF.formatEmailMessage(texto), "html");
        }

        public static void EnviaEmailAprovacaoOrcamento(IntegracaoBanrisulAtendimento atendimento, string CodOS)
        {
            string assunto = "Integração Banrisul - Aprovação Orçamento - OS: " + CodOS + ".";

            string mensagem = "Chamado Cliente: " + atendimento.NumeroIncidente.Valor + " com orçamento aprovado de acordo com notificação via integração. " + "<br/> <h5 style='color: steelblue;'>Número OS Perto: 12365478</h5><br/>";

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

            //clsGF.sendMail("equipe.sat@perto.com.br", Configuracao.DestinatarioEmailAprovacaoOrcamento, "", assunto, clsGF.formatEmailMessage(texto), "html");
        }
    }
}