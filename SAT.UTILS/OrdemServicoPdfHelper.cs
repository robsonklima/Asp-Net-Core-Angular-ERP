using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;

namespace SAT.UTILS
{
    public class OrdemServicoPdfHelper : IDocument
    {

        public OrdemServico OrdemServico { get; }

        public OrdemServicoPdfHelper(OrdemServico ordemServico)
        {
            OrdemServico = ordemServico;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        static IContainer CellStyle(IContainer container)
        {
            return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
        }

        static IContainer TitleStyle(IContainer container)
        {
            return container.PaddingVertical(5);
        }

        static TextStyle FontStyle()
        {
            return TextStyle.Default.FontSize(10).LineHeight(0.8f);
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(30);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().Height(25).Row(row =>
                    {
                        // row.ConstantItem(350).AlignLeft().Text(x =>
                        // {
                        //     x.Span("Os valores deste orçamento são apenas uma previsão. O efetivo faturamento será efetuado de acordo com os serviços executados e comprovados em Relatórios Técnicos, assinados pelo cliente.").FontSize(8);
                        //     x.EmptyLine();
                        //     x.Span("*O valor de desconto a base de troca está condicionado ao recolhimento da peça substituída").FontSize(8);
                        // });
                        row.RelativeItem().AlignRight().Text(x =>
                        {
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.ConstantItem(280).Column(column =>
                {

                    column.Item().Row(async cr =>
                    {
                        cr.Spacing(20);

                        using (HttpClient webClient = new HttpClient())
                        {
                            byte[] dataArr = await webClient.GetAsync("https://sat.perto.com.br/sat.v2.frontend/assets/images/logo/logo.png").Result.Content.ReadAsByteArrayAsync();

                            cr.ConstantItem(65).AlignMiddle().Image(dataArr, ImageScaling.FitArea);
                        }                        cr.RelativeItem().Column(t =>
                        {
                            t.Item().Text($"Perto S.A").SemiBold().FontSize(10);
                            t.Item().Text($"Tecnologia para Bancos e Varejo").FontSize(10);
                            t.Item().Text($"Rua Nissin Castiel, 640 Distrito Industrial").FontSize(10);
                            t.Item().Text($"CEP: 94045-420 | Gravataí | RS | Brasil").FontSize(10);
                            t.Item().Text($"(51) 3489-8700").FontSize(10);
                            t.Item().Text($"www.perto.com.br").FontSize(10);
                        });
                    });
                });

                row.RelativeItem().Column(column =>
                {
                    column.Item().AlignCenter().AlignMiddle().Text(tx =>
                     { 
                        tx.Span("OS  ").Style(TextStyle.Default.FontColor(Colors.Grey.Medium).FontSize(20));
                        tx.Span($"{OrdemServico.CodOS}").Style(TextStyle.Default.FontSize(24));
                     });
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Border(1).BorderColor(Colors.Grey.Lighten1).Column(column =>
            {
                column.Spacing(10);

                column.Item().Element(ComporDadosOs);
                column.Item().Element(ComporRelatorios);

            });
        }

        void ComporDadosOs(IContainer container)
        {
            container.Padding(10).Grid(grid =>
            {
                grid.VerticalSpacing(5);
                grid.HorizontalSpacing(15);
                grid.AlignCenter();

                grid.Item(4).Text($"Intervenção: {OrdemServico.TipoIntervencao.NomTipoIntervencao}").FontSize(10); // INTERVENCAO
                grid.Item(4).Text($"Abertura: {OrdemServico.DataHoraAberturaOS}").FontSize(10); // ABERTURA
                grid.Item(4).Text($"Solicitado em: {OrdemServico.DataHoraSolicitacao}").FontSize(10);// SOLICITADO

                grid.Item(4).Text($"OS Cliente: {OrdemServico.NumOSCliente}").FontSize(10); // OS CLIENTE
                grid.Item(4).Text($"OS 4º: {OrdemServico.NumOSQuarteirizada}").FontSize(10); // OS Quarteirizada
                grid.Item(4).Text($"Status: {OrdemServico.StatusServico?.NomeStatusServico}").FontSize(10); // STATUS

                grid.Item(4).Text($"Cliente: {OrdemServico.Cliente.NumBanco} - {OrdemServico.Cliente.NomeFantasia}").FontSize(10); // CLIENTE
                grid.Item(4).Text($"Agência: {OrdemServico.LocalAtendimento?.NumAgencia}/{OrdemServico.LocalAtendimento?.DCPosto}").FontSize(10); // CLIENTE
                grid.Item(4).Text($"Endereço: {OrdemServico.LocalAtendimento?.Endereco}").FontSize(10); // ENDERECO

                grid.Item(6).Text($"Bairro: {OrdemServico.LocalAtendimento?.Bairro?? ""}").FontSize(10); // BAIRRO
                grid.Item(6).Text($"Cidade: {OrdemServico.LocalAtendimento?.Cidade.NomeCidade?? ""}").FontSize(10); // CIDADE
                grid.Item(4).Text($"Solicitante: {OrdemServico.NomeSolicitante}").FontSize(10); // SOLICITANTE
                grid.Item(4).Text($"Contato: {OrdemServico.NomeContato}").FontSize(10); // CONTATO
                grid.Item(4).Text($"Telefone: {OrdemServico.LocalAtendimento?.Telefone1_DEL} \n                  {OrdemServico.LocalAtendimento?.Telefone2_DEL} ").FontSize(10); // TELEFONE

                grid.Item(4).Text($"Equipamento: {OrdemServico.EquipamentoContrato?.Equipamento.NomeEquip}").FontSize(10); // EQUIPAMENTO
                grid.Item(4).Text($"Série: {OrdemServico.EquipamentoContrato?.NumSerie}").FontSize(10); // SERIE
                grid.Item(4).Text($"SLA: {OrdemServico.EquipamentoContrato?.AcordoNivelServico.NomeSLA}").FontSize(10); // SLA

                grid.Item(12).Text($"Defeito: {OrdemServico.DefeitoRelatado}").FontSize(10); // DEFEITO

                grid.Item(12).Text($"Obs OS: {OrdemServico.ObservacaoCliente}").FontSize(10); // OBS OS

                grid.Item(4).Text($"FILIAL: {OrdemServico.EquipamentoContrato?.Filial.NomeFilial}").FontSize(10); // FILIAL
                grid.Item(4).Text($"PAT: {OrdemServico.EquipamentoContrato?.Autorizada.NomeFantasia}").FontSize(10); // PAT
                grid.Item(4).Text($"Região: {OrdemServico.EquipamentoContrato?.Regiao.NomeRegiao}").FontSize(10); // REGIAO

                grid.Item(12).Text($"Data/Hora Fim SLA: {OrdemServico.PrazosAtendimento.LastOrDefault()?.DataHoraLimiteAtendimento.ToString() ?? ""}").FontSize(10); // DATA FIM SLA
            });
        }

        void ComporRelatorios(IContainer container)
        {
            container.BorderTop(1).BorderColor(Colors.Grey.Lighten1).Padding(10).Grid(grid =>
            {
                int index = 1;
                OrdemServico.RelatoriosAtendimento.ForEach((rel) =>
                {
                    grid.VerticalSpacing(5);
                    grid.HorizontalSpacing(10);
                    grid.AlignCenter();

                    grid.Item(12).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Text($"Atendimento #{index}");
                    index++;
                    grid.Item(6).Text($"Técnico: {rel.Tecnico.Nome}").FontSize(10); // INTERVENCAO
                    grid.Item(6).Text($"Status: {rel.StatusServico.NomeStatusServico}").FontSize(10); // ABERTURA

                    grid.Item(6).Text($"Nº: {rel.NumRAT}").FontSize(10); // OS CLIENTE
                    grid.Item(6).Text($"Acompanhante: {rel.NomeAcompanhante}").FontSize(10); // OS Quarteirizada

                    var detalhe = rel.RelatorioAtendimentoDetalhes.FirstOrDefault();

                    if (detalhe != null) {
                        grid.Item(12).Text( tx =>
                        {
                            tx.Span($"Tipo Serviço: {detalhe.TipoServico.NomeServico}").FontSize(10);
                            var tipoServico = detalhe.TipoServico.CodETipoServico.StartsWith("1") ? "MÁQUINA" : "EXTRA-MÁQUINA";
                            tx.Span($"  ({tipoServico})").FontSize(10);
                        });
                    }
                    
                    grid.Item(4).Text($"Início: {rel.DataHoraInicio}").FontSize(10); // BAIRRO
                    grid.Item(4).Text($"Término: {rel.DataHoraSolucao}").FontSize(10); // CIDADE
                    grid.Item(4).Text($"Horas gastas: {rel.DataHoraSolucao.Subtract(rel.DataHoraInicio).Hours.ToString("00")}:{rel.DataHoraSolucao.Subtract(rel.DataHoraInicio).Minutes.ToString("00")}").FontSize(10); // CIDADE

                    grid.Item(12).Text($"Desc. Solução: {rel.RelatoSolucao}").FontSize(10); // SOLICITANTE

                    grid.Item(12).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TitleStyle).Text("PEÇAS");

                        });

                        if (rel.RelatorioAtendimentoDetalhes.SelectMany(del => del.RelatorioAtendimentoDetalhePecas).Any())
                        {
                            table.Cell().Table(t2 =>
                            {
                                t2.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                t2.Header(h2 =>
                                {
                                    h2.Cell().Element(CellStyle).Text("Causa").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Defeito").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Ação").Style(FontStyle());
                                    h2.Cell().Element(CellStyle).Text("Peças").Style(FontStyle());
                                });

                                rel.RelatorioAtendimentoDetalhes.ForEach(det =>
                                {
                                    t2.Cell().Element(CellStyle).Text($"{det.Causa.CodECausa} - {det.Causa.NomeCausa}").Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text($"{det.Defeito.CodEDefeito} - {det.Defeito.NomeDefeito}").Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text($"{det.Acao.CodEAcao} - {det.Acao.NomeAcao}").Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(txt => 
                                    {  
                                        det.RelatorioAtendimentoDetalhePecas.ForEach(peca =>
                                        {
                                            txt.Span($"{peca.Peca?.CodMagnus} P({peca.QtdePecas})").Style(FontStyle());
                                            txt.EmptyLine();
                                        });
                                    });
                                });
                            });
                        }
                        else
                        {
                            table.Cell().Text("Nenhuma peça utilizada");
                        }
                    });
                });
            });
        }

        //     void ComporDadosLocalFaturamento(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.ConstantColumn(200);
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("DADOS DO LOCAL PARA FATURAMENTO");
        //                     });

        //                     table.Cell().Element(CellStyle).Text("Razão Social").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.NomeFantasia).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("CNPJ").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.Cnpj).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("I.E").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.InscricaoEstadual).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Responsável").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.ResponsavelFaturamento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("E-mail").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.EmailFaturamento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Fone").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.FoneFaturamento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Endereço").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.EnderecoFaturamento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Número").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.NumeroFaturamento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Complemento").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.ComplementoFaturamento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Bairro").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.BairroFaturamento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Cidade").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CidadeFaturamento?.NomeCidade).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("UF").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CidadeFaturamento?.UnidadeFederativa.SiglaUF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("CEP").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CepFaturamento).Style(FontStyle());

        //                 });
        //     }

        //     void ComporDadosLocalEnvioNf(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.ConstantColumn(250);
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("DADOS DO LOCAL PARA ENVIO DE NOTA FISCAL");
        //                     });

        //                     table.Cell().Element(CellStyle).Text("Razão Social").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.NomeFantasia).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("CNPJ").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.Cnpj).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("I.E").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Cliente?.InscricaoEstadual).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Responsável").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.ResponsavelEnvioNF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("E-mail").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.EmailEnvioNF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Fone").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.FoneEnvioNF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Endereço").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.EnderecoEnvioNF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Número").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.NumeroEnvioNF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Complemento").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.ComplementoEnvioNF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Bairro").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.BairroEnvioNF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Cidade").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CidadeEnvioNF?.NomeCidade).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("UF").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CidadeEnvioNF?.UnidadeFederativa.SiglaUF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("CEP").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.LocalEnvioNFFaturamento?.CepEnvioNF).Style(FontStyle());
        //                 });
        //     }

        //     void ComporDadosLocalAtendimento(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.ConstantColumn(320);
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("DADOS DO LOCAL DE ATENDIMENTO/OCORRÊNCIA");
        //                     });

        //                     table.Cell().Element(CellStyle).Text("Nro Contrato").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.EquipamentoContrato?.Contrato?.NroContrato).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Série").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.EquipamentoContrato?.NumSerie).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Modelo").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.Equipamento?.NomeEquip).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("OS Cliente").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.NumOSCliente).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Os Perto").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.CodOS).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Agência").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text($"{Orcamento.OrdemServico.LocalAtendimento.NumAgencia}/{Orcamento.OrdemServico.LocalAtendimento.DCPosto}").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Nome Local").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.NomeLocal).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Bairro").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.Bairro).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Endereço").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.Endereco).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Número").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.NumeroEnd).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Complemento").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.EnderecoComplemento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Cidade").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.Cidade?.NomeCidade).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("UF").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento.Cidade?.UnidadeFederativa?.SiglaUF).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("CEP").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrdemServico.LocalAtendimento?.Cep).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Motivo").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoMotivo?.Descricao).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Detalhes").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.Detalhe).Style(FontStyle());
        //                 });
        //     }

        //     void ComporMaoDeObra(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.ConstantColumn(150);
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("MÃO DE OBRA");
        //                     });

        //                     table.Cell().Element(CellStyle).Text("Hora Técnica").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.MaoDeObra.ValorHoraTecnica).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Previsão de Horas").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.MaoDeObra.PrevisaoHoras).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Total").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.MaoDeObra.ValorTotal).Style(FontStyle().Bold());
        //                 });
        //     }

        //     void ComporDeslocamento(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.ConstantColumn(150);
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("DESLOCAMENTO");


        //                     });

        //                     table.Cell().Element(CellStyle).Text("Unitário KM Rodado").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.ValorUnitarioKmRodado).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("KM").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.QuantidadeKm).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Percurso").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text("Ida e Volta").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Total KM Rodado").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.ValorTotalKmRodado).Style(FontStyle().Bold());
        //                     table.Cell().Element(CellStyle).Text("Hora em Deslocamento").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.ValorHoraDeslocamento).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Qtd 1h a cada 65km rodados").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.QuantidadeHoraCadaSessentaKm).Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Total KM Deslocamento").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.OrcamentoDeslocamento.ValorTotalKmDeslocamento).Style(FontStyle().Bold());
        //                 });
        //     }

        //     void ComporMaterialUtilizado(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("MATERIAL A SER UTILIZADO");

        //                     });

        //                     if (Orcamento.Materiais.Any())
        //                     {
        //                         table.Cell().Table(t2 =>
        //                         {
        //                             t2.ColumnsDefinition(columns =>
        //                             {
        //                                 columns.RelativeColumn();
        //                                 columns.ConstantColumn(160);
        //                                 columns.RelativeColumn();
        //                                 columns.RelativeColumn();
        //                                 columns.ConstantColumn(180);
        //                                 columns.RelativeColumn();
        //                             });
        //                             t2.Header(h2 =>
        //                             {
        //                                 h2.Cell().Element(CellStyle).Text("Código").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Descrição").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Qtd").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Unitário").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Desconto a base de troca*").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Total").Style(FontStyle());
        //                             });

        //                             Orcamento.Materiais.ForEach(mat =>
        //                             {
        //                                 t2.Cell().Element(CellStyle).Text(mat.CodigoMagnus).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(mat.Descricao).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(mat.Quantidade).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(mat.ValorUnitario).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(mat.ValorDesconto).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(mat.ValorTotal).Style(FontStyle().Bold());
        //                             });
        //                         });
        //                     }
        //                     else
        //                     {
        //                         table.Cell().Text("Nenhum material cadastrado neste orçamento");
        //                     }
        //                 });
        //     }

        //     void ComporOutrosServicos(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     // step 1
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("OUTROS SERVIÇOS");


        //                     });

        //                     if (Orcamento.OutrosServicos.Any())
        //                     {
        //                         table.Cell().Table(t2 =>
        //                         {
        //                             t2.ColumnsDefinition(columns =>
        //                             {
        //                                 columns.ConstantColumn(200);
        //                                 columns.RelativeColumn();
        //                                 columns.RelativeColumn();
        //                                 columns.RelativeColumn();
        //                             });
        //                             t2.Header(h2 =>
        //                             {
        //                                 h2.Cell().Element(CellStyle).Text("Descrição").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Unitário").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Qtd").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Total").Style(FontStyle());
        //                             });

        //                             Orcamento.OutrosServicos.ForEach(serv =>
        //                             {
        //                                 t2.Cell().Element(CellStyle).Text(serv.Descricao).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(serv.ValorUnitario).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(serv.Quantidade).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(serv.ValorTotal).Style(FontStyle().Bold());
        //                             });
        //                         });
        //                     }
        //                     else
        //                     {
        //                         table.Cell().Text("Nenhum serviço adicional cadastrado neste orçamento");
        //                     }
        //                 });
        //     }

        //     void ComporDescontos(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     // step 1
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("DESCONTOS");


        //                     });

        //                     if (Orcamento.Descontos.Any())
        //                     {
        //                         table.Cell().Table(t2 =>
        //                         {
        //                             t2.ColumnsDefinition(columns =>
        //                             {
        //                                 columns.ConstantColumn(100);
        //                                 columns.RelativeColumn();
        //                                 columns.RelativeColumn();

        //                             });
        //                             t2.Header(h2 =>
        //                             {
        //                                 h2.Cell().Element(CellStyle).Text("Motivo").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Tipo").Style(FontStyle());
        //                                 h2.Cell().Element(CellStyle).Text("Total").Style(FontStyle());

        //                             });

        //                             Orcamento.Descontos.ForEach(desc =>
        //                             {
        //                                 t2.Cell().Element(CellStyle).Text(desc.Motivo).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(desc.NomeTipo).Style(FontStyle());
        //                                 t2.Cell().Element(CellStyle).Text(desc.ValorTotal).Style(FontStyle()).Bold();
        //                             });
        //                         });
        //                     }
        //                     else
        //                     {
        //                         table.Cell().Text("Nenhum desconto cadastrado neste orçamento");
        //                     }
        //                 });
        //     }

        //     void ComporTotal(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     // step 1
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.ConstantColumn(150);
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("TOTAL");
        //                     });

        //                     table.Cell().Element(CellStyle).Text("Descontos").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.ValorTotalDesconto).Style(FontStyle().Bold());
        //                     table.Cell().Element(CellStyle).Text("ValorTotal").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text(Orcamento.ValorTotal).Style(FontStyle().Bold());          
        //                 });
        //     }

        //     void ComporCondicoes(IContainer container)
        //     {
        //         container.Table(table =>
        //                 {
        //                     // step 1
        //                     table.ColumnsDefinition(columns =>
        //                     {
        //                         columns.ConstantColumn(150);
        //                         columns.RelativeColumn();
        //                     });

        //                     table.Header(header =>
        //                     {
        //                         header.Cell().Element(TitleStyle).Text("CONDIÇÕES");
        //                     });

        //                     table.Cell().Element(CellStyle).Text("Validade Orçamento").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text("3 dias").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).Text("Impostos Inclusos").Style(FontStyle());
        //                     table.Cell().Element(CellStyle).AlignRight().Text("Sim").Style(FontStyle());          
        //                 });
        //     }
        // }
    }
}