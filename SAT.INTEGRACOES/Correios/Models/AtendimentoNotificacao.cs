//using prjGenericFunction;
//using prjSATClass;
//using RSharp.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace SAT.INTEGRACOES.Correios
//{
//    public static class AtendimentoNotificacao
//    {
//        private static string GetHtmlEmailTecnicoNaoCadastrado(string mensagem, Tecnico tecnico)
//        {
//            StringBuilder texto = new StringBuilder();

//            texto.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\" >");
//            texto.Append("<html>");
//            texto.Append("	<body>");
//            texto.Append("			<font style=\"FONT-SIZE: 12pt\">");
//            texto.Append("				<div align=\"center\">");
//            texto.AppendFormat("            {0}", mensagem);
//            texto.Append("				</div>");
//            texto.Append("			</font>");
//            texto.Append("			<br>");
//            texto.Append("			<table style=\"BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid ;FONT-SIZE: 10pt; FONT-FAMILY: verdana\" align=\"center\">");
//            texto.Append("				<tr>");
//            texto.Append("					<td style=\"BACKGROUND-COLOR: #47699b; COLOR: white\" align=\"center\" colspan=\"2\">");
//            texto.Append("						<font style=\"FONT-WEIGHT: bold\">");
//            texto.Append("							Dados do Técnico");
//            texto.Append("						</font>");
//            texto.Append("					</td>");
//            texto.Append("				</tr>");

//            texto.Append("				<tr>");
//            texto.Append("					<td align=\"right\">Número Chamado:</td>");
//            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.NUMOSCLIENTE);
//            texto.Append("				</tr>");

//            texto.Append("				<tr>");
//            texto.Append("					<td align=\"right\">OS Perto:</td>");
//            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.CODOS);
//            texto.Append("				</tr>");

//            texto.Append("				<tr>");
//            texto.Append("					<td align=\"right\">Técnico:</td>");
//            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.NOME);
//            texto.Append("				</tr>");

//            texto.Append("				<tr>");
//            texto.Append("					<td align=\"right\">CPF:</td>");
//            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.CPF);
//            texto.Append("				</tr>");

//            texto.Append("				<tr>");
//            texto.Append("					<td align=\"right\">RG:</td>");
//            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.RG);
//            texto.Append("				</tr>");

//            texto.Append("				<tr>");
//            texto.Append("					<td align=\"right\">E-mail:</td>");
//            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.EMAIL);
//            texto.Append("				</tr>");

//            texto.Append("				<tr>");
//            texto.Append("					<td align=\"right\">Fone:</td>");
//            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.FONE);
//            texto.Append("				</tr>");

//            texto.Append("				<tr>");
//            texto.Append("					<td align=\"right\">Fone Celular:</td>");
//            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.FONEPERTO);
//            texto.Append("				</tr>");
//            texto.Append("			</table>");
//            texto.Append("	</body>");
//            texto.Append("</html>");

//            return texto.ToString();
//        }

//        public static void EnviaEmailCadastroTecnico(Tecnico tecnico)
//        {
//            string assunto = "Integração Correios - Técnico não cadastrado";

//            string mensagem = "Necessário cadastro do técnico " + tecnico.NOME + ".<br>Chamado " + tecnico.CODOS + " (OSCliente: " + tecnico.NUMOSCLIENTE + ") não transferido no sistema GST Correios devido a falta de cadastro.<br> " +
//                                " Email de notificação encaminhado para: " + Configuracao.MailNotificacaoTecnico + ".";

//            string texto = GetHtmlEmailTecnicoNaoCadastrado(mensagem, tecnico);

//            clsGF.sendMail("equipe.sat@perto.com.br", Configuracao.MailNotificacaoTecnico, 
//                "", assunto, clsGF.formatEmailMessage(texto), "html");
//        }

//        //public static void EnviaEmailRecusaAbertura(Atendimento atendimento, String motivo)
//        //{
//        //    string assunto = "Integração Banrisul ATM - Situação chamado cliente " +
//        //        atendimento.NumeroIncidente.Valor + " - Analista " + atendimento.NomeContato.Valor;

//        //    string mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " recusado pois não atendeu aos seguintes requisitos: " + "<br/><ul><li>" + motivo + "</li></ul>";

//        //    string texto = GetHtmlEmailAbertura(atendimento, mensagem);

//        //    clsGF.sendMail("equipe.sat@perto.com.br", Configuracao.DestinatarioEmailRetornoRecusaAbertura, 
//        //        "", assunto, clsGF.formatEmailMessage(texto), "html");
//        //}

//        //public static void EnviaEmailAprovacaoOrcamento(Atendimento atendimento, string CodOS)
//        //{
//        //    string assunto = "Integração Banrisul - Aprovação Orçamento - OS: " + CodOS + ".";

//        //    string mensagem = "Chamado Cliente: " + atendimento.NumeroIncidente.Valor + " com orçamento aprovado de acordo com notificação via integração. " + "<br/> <h5 style='color: steelblue;'>Número OS Perto: 12365478</h5><br/>";
            
//        //    string texto = GetHtmlEmailAbertura(atendimento, mensagem);

//        //    clsGF.sendMail("equipe.sat@perto.com.br", Configuracao.DestinatarioEmailAprovacaoOrcamento,
//        //        "", assunto, clsGF.formatEmailMessage(texto), "html");
//        //}
//    }
//}
