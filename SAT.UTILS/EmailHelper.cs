using System.ComponentModel;
using System.Data;

namespace SAT.UTILS {
    public class EmailHelper
    {
        private EmailHelper()
        {}
        
        public static string ConverterParaHtml<T>(List<T> list, string titulo="E-mail automático SAT 2.0", string descricao="Este é um e-mail automático enviado pelo SAT 2.0")
        {
            DataTable table = Criar<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                table.Rows.Add(row);
            }

            
            string style = @"<style>
                    html,
                    body,
                    table,
                    tbody,
                    tr,
                    td,
                    div,
                    p,
                    ul,
                    ol,
                    li,
                    h1,
                    h2,
                    h3,
                    h4,
                    h5,
                    h6 {
                    margin: 0;
                    padding: 0;
                    }

                    body {
                    margin: 0;
                    padding: 0;
                    font-size: 0;
                    line-height: 0;
                    -ms-text-size-adjust: 100%;
                    -webkit-text-size-adjust: 100%;
                    }

                    table {
                    border-spacing: 0;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    }

                    table td {
                    border-collapse: collapse;
                    }

                    .ExternalClass {
                    width: 100%;
                    }

                    .ExternalClass,
                    .ExternalClass p,
                    .ExternalClass span,
                    .ExternalClass font,
                    .ExternalClass td,
                    .ExternalClass div {
                    line-height: 100%;
                    }
                    /* Outermost container in Outlook.com */

                    .ReadMsgBody {
                    width: 100%;
                    }

                    img {
                    -ms-interpolation-mode: bicubic;
                    }

                    h1,
                    h2,
                    h3,
                    h4,
                    h5,
                    h6 {
                    font-family: Arial;
                    }

                    h1 {
                    font-size: 28px;
                    line-height: 32px;
                    padding-top: 10px;
                    padding-bottom: 24px;
                    }

                    h2 {
                    font-size: 24px;
                    line-height: 28px;
                    padding-top: 10px;
                    padding-bottom: 20px;
                    }

                    h3 {
                    font-size: 20px;
                    line-height: 24px;
                    padding-top: 10px;
                    padding-bottom: 16px;
                    }

                    p {
                    font-size: 16px;
                    line-height: 20px;
                    font-family: Georgia, Arial, sans-serif;
                    }

                    </style>
                    <style>
                    .full-container {
                        width: 95%;
                        max-width: 100%;
                    }

                    .container600 {
                        width: 600px;
                        max-width: 100%;
                    }

                    @media all and (max-width: 599px) {
                    .container600 {
                        width: 100% !important;
                    }
                    
                    .smarttable {
                        border: 0px;
                    }
                    .smarttable thead {
                        display:none;
                        border: none;
                        height: 0px;
                        margin: 0px;
                        overflow: hidden;
                        padding: 0px;
                        max-width:0px;
                        max-height:0px;
                    }
                    .smarttable tr {
                        display: block;
                        width:90%;
                        margin:20px auto;
                    }
                    .smarttable td {
                        border-bottom: 1px solid #ddd;
                        display: block;
                        font-size: 15px;
                        text-align: center;
                    }
                    }
                </style>

                <!--[if gte mso 9]>
                        <style>
                            .ol {
                            width: 100%;
                            }
                        </style>
                    <![endif]-->";

            string html = @$"<!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>SAT 2.0 - E-mail</title>
                    {style}
                </head>
                <body style='background-color:#F4F4F4;'>
                    <center>

                        <!--[if gte mso 9]><table width='{table.Columns.Count * 88}' cellpadding='0' cellspacing='0'><tr><td>
                                    <![endif]-->
                    <table class='full-container' cellpadding='0' cellspacing='0' border='0' width='100%' style='width:calc(100%);max-width:calc(600px);margin: 0 auto;'>
                        <tr>
                        <td width='100%' style='text-align: left;'>
                                <!--<table width='100%' cellpadding='0' cellspacing='0' style='min-width:100%;'>
                                    <tr>
                                        <td style='background-color:#FFFFFF;color:#000000;padding:30;'>
                                        <img alt='' src='https://sat.perto.com.br/SAT.V2.FRONTEND/assets/images/logo/logo-2.png' width='68' style='display: block;' />
                                        </td>
                                    </tr>
                                </table>-->
                                
                                <table width='100%' cellpadding='0' cellspacing='0' style='min-width:100%;'>
                                <tr>
                                    <td style='background-color:white;color:#58585A;padding:30px;'>

                                    <h1>{titulo}</h1>
                                    <p>{descricao}</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='padding:20px;background-color:#ECF0F1;'>

                                        <table class='smarttable' width='100%' cellpadding='0' cellspacing='0' style='min-width:100%;'>
                                        <thead>";
            html += "<tr>";
            for(int i=0;i<table.Columns.Count;i++)
                html+="<th scope='col' style='padding:5px; font-family: Arial,sans-serif; font-size: 12px; line-height:20px;line-height:30px' align='left'>"+table.Columns[i].ColumnName+"</th>";
            html += "</tr>";
            html += "<tbody>";
            
            for (int i = 0; i < table.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j< table.Columns.Count; j++)
                    html += "<td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 12px; line-height:20px;'>" + table.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += @"</tbody>
                            </table>

                             </td>
                            </tr>
                            </table>
                            <table width='100%' cellpadding='0' cellspacing='0' style='min-width:100%;'>
                            <tr>
                                <td width='100%' style='min-width:100%;background-color:#58585A;color:#FFFFFF;padding:30px;'>
                                <p style='font-size:16px;line-height:20px;font-family:Arial,sans-serif;text-align:center;'>
                                    <a href='https://sat.perto.com.br/SAT.V2.FRONTEND' style='text-decoration: none;color:white'>SAT 2.0</a>
                                </p>
                                </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!--[if gte mso 9]></td></tr></table>
                    <![endif]-->
                </center>
                <br><br>
            </body>
            </html>";

            return html;        
        }
        
        private static DataTable Criar<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }
    }
}