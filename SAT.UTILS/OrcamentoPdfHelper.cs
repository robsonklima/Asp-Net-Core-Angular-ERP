using System.Globalization;
using System.Net;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;

namespace SAT.UTILS
{
    public class OrcamentoPdfHelper : IDocument
    {

        public Orcamento Orcamento { get; }

        public OrcamentoPdfHelper(Orcamento orcamento)
        {
            Orcamento = orcamento;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        static IContainer CellStyle(IContainer container)
        {
            return container.BorderColor(Colors.Grey.Lighten2).PaddingVertical(3);
        }

        static IContainer TitleStyle(IContainer container)
        {
            return container.PaddingVertical(5);
        }

        static TextStyle FontStyle()
        {
            return TextStyle.Default.FontSize(8).LineHeight(0.8f);
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.MarginVertical(15);
                    page.MarginHorizontal(30);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().AlignRight().Text(x =>
                        {
                            x.CurrentPageNumber().FontSize(8);
                            x.Span(" / ").FontSize(8);
                            x.TotalPages().FontSize(8);
                        });
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(10).SemiBold().FontColor(Colors.Black);

            container.PaddingBottom(5).Row(row =>
            {
                row.ConstantItem(280).Column(column =>
                {

                    column.Item().Row(async cr =>
                    {
                        cr.Spacing(20);

                        using (HttpClient webClient = new HttpClient())
                        {
                            byte[] dataArr = await webClient.GetAsync("https://sat.perto.com.br/sat.v2.frontend/assets/images/logo/logo.png").Result.Content.ReadAsByteArrayAsync();

                            cr.ConstantItem(60).AlignMiddle().Image(dataArr, ImageScaling.FitArea);
                        }
                        cr.RelativeItem().Column(t =>
                        {
                            t.Item().Text($"Perto S.A").SemiBold().FontSize(8);
                            t.Item().Text($"Tecnologia para Bancos e Varejo").FontSize(8);
                            t.Item().Text($"Rua Nissin Castiel, 640 Distrito Industrial").FontSize(8);
                            t.Item().Text($"CEP: 94045-420 | Gravataí | RS | Brasil").FontSize(8);
                            t.Item().Text($"(51) 3489-8700 - www.perto.com.br").FontSize(8);
                        });
                    });
                });

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"ORÇAMENTO DE SERVIÇOS EXTRAS").Style(titleStyle);
                    column.Item().Text(text =>
                    {
                        text.Span($"{Orcamento?.Filial?.NomeFilial} - ").FontSize(8);
                        text.Span($"{Orcamento?.Filial?.Cidade?.NomeCidade},  ").FontSize(8);
                        text.Span($"{Orcamento?.Filial?.Cidade?.UnidadeFederativa?.SiglaUF}").FontSize(8);
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"{Orcamento.Numero} - ").SemiBold().FontSize(8);
                        text.Span($"{DateTime.Now.ToString("dd/MM/yyyy")}").FontSize(8);
                    });

                    column.Item().Text(text =>
                    {
                        text.Span($"OS: ").SemiBold().FontSize(8);
                        text.Span($"{Orcamento.OrdemServico.CodOS}").FontSize(8);
                    });
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Border(1).BorderColor(Colors.Grey.Lighten1).Padding(10).Column(column =>
            {
                column.Spacing(1);
                column.Item().Element(ComporDadosLocalFaturamento);
                column.Item().Element(ComporDadosLocalEnvioNf);
                column.Item().Element(ComporDadosLocalAtendimento);
                column.Item().Element(ComporDadosChamado);
                column.Item().Element(ComporMaoDeObra);
                column.Item().Element(ComporDeslocamento);
                column.Item().Element(ComporOutrosServicos);
                column.Item().Element(ComporMaterialUtilizado);
                column.Item().Element(ComporDescontos);
                column.Item().Element(ComporTotal);
                column.Item().Element(ComporCondicoes);

            });
        }

        void ComporDadosLocalFaturamento(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DADOS PARA FATURAMENTO").FontSize(10).Bold();
                        });

                        table.Cell().Element(CellStyle).Grid(grid =>
                        {
                            grid.VerticalSpacing(1);
                            grid.HorizontalSpacing(15);
                            grid.AlignCenter();

                            grid.Item(6).Text(t =>
                            {
                                t.Span($"Razão Social: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.RazaoSocialFaturamento}").FontSize(8);
                            });

                            grid.Item(3).Text(t =>
                            {
                                t.Span($"        CNPJ: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.CnpjFaturamento}").FontSize(8);
                            });

                            grid.Item(3).Text(t =>
                            {
                                t.Span($"  Ins. Estadual: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.InscricaoEstadualFaturamento}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Endereço: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.EnderecoFaturamento}").FontSize(8);
                            });

                            grid.Item(2).Text(t =>
                            {
                                t.Span($"   Número: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.NumeroFaturamento}").FontSize(8);
                            });

                            grid.Item(3).Text(t =>
                            {
                                t.Span($"    Complemento: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.ComplementoFaturamento}").FontSize(8);
                            });

                            grid.Item(3).Text(t =>
                            {
                                t.Span($"CEP: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.CepFaturamento}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Bairro: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.BairroFaturamento}").FontSize(8);
                            });

                            grid.Item(2).Text(t =>
                            {
                                t.Span($"Cidade: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.CidadeFaturamento?.NomeCidade}").FontSize(8);
                            });

                            grid.Item(6).Text(t =>
                            {
                                t.Span($"UF: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.CidadeFaturamento?.UnidadeFederativa.SiglaUF}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Responsável: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.ResponsavelFaturamento}").FontSize(8);
                            });

                            grid.Item(2).Text(t =>
                            {
                                t.Span($"Fone: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.FoneFaturamento}").FontSize(8);
                            });

                            grid.Item(6).Text(t =>
                            {
                                t.Span($"E-mail: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.EmailFaturamento}").FontSize(8);
                            });

                        });
                    });
        }

        void ComporDadosLocalEnvioNf(IContainer container)
        {

            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DADOS PARA ENVIO DE NOTA FISCAL").FontSize(10).Bold();
                        });

                        table.Cell().Element(CellStyle).Grid(grid =>
                        {
                            grid.VerticalSpacing(1);
                            grid.HorizontalSpacing(15);
                            grid.AlignCenter();

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Responsável: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.ResponsavelEnvioNF}").FontSize(8);
                            });

                            grid.Item(2).Text(t =>
                            {
                                t.Span($"Fone: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.FoneEnvioNF}").FontSize(8);
                            });

                            grid.Item(6).Text(t =>
                            {
                                t.Span($"E-mail: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.LocalEnvioNFFaturamentoVinculado?.LocalEnvioNFFaturamento?.EmailEnvioNF}").FontSize(8);
                            });
                        });
                    });
        }

        void ComporDadosLocalAtendimento(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DADOS DO LOCAL DE ATENDIMENTO").FontSize(10).Bold();
                        });

                        table.Cell().Element(CellStyle).Grid(grid =>
                        {
                            grid.VerticalSpacing(1);
                            grid.HorizontalSpacing(15);
                            grid.AlignCenter();

                            grid.Item(6).Text(t =>
                            {
                                t.Span($"Nome Local: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.LocalAtendimento.NomeLocal}").FontSize(8);
                            });
                            
                            grid.Item(3).Text(t =>
                            {
                                t.Span($"       CNPJ: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.LocalAtendimento.Cnpj}").FontSize(8);
                            });

                            grid.Item(3).Text(t =>
                            {
                                t.Span($"Nro Contrato: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.EquipamentoContrato?.Contrato?.NroContrato}").FontSize(8);
                            });

                            grid.Item(6).Text(t =>
                            {
                                t.Span($"Endereço: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.LocalAtendimento.Endereco}").FontSize(8);
                            });

                            grid.Item(3).Text(t =>
                            {
                                t.Span($"       Agência: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.LocalAtendimento.NumAgencia}/{Orcamento.OrdemServico.LocalAtendimento.DCPosto}").FontSize(8);
                            });
                            
                            grid.Item(3).Text(t =>
                            {
                                t.Span($"CEP: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.LocalAtendimento?.Cep}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Bairro: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.LocalAtendimento?.Bairro}").FontSize(8);
                            });

                            grid.Item(2).Text(t =>
                            {
                                t.Span($"Cidade: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.LocalAtendimento?.Cidade?.NomeCidade}").FontSize(8);
                            });

                            grid.Item(6).Text(t =>
                            {
                                t.Span($"UF: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF}").FontSize(8);
                            });
                        });
                    });
        }

        void ComporDadosChamado(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DADOS DO CHAMADO").FontSize(10).Bold();
                        });

                        table.Cell().Element(CellStyle).Grid(grid =>
                        {
                            grid.VerticalSpacing(1);
                            grid.HorizontalSpacing(15);
                            grid.AlignCenter();

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"OS Cliente: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.NumOSCliente}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"OS Perto: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.CodOS}").FontSize(8);
                            });

                            grid.Item(4);

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Modelo: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.Equipamento?.NomeEquip}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Série: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.EquipamentoContrato?.NumSerie}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"BEM: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrdemServico.EquipamentoContrato?.NumSerieCliente}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Motivo Orçamento: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.OrcamentoMotivo?.Descricao}").FontSize(8);
                            });

                            grid.Item(4).Text(t =>
                            {
                                t.Span($"Detalhes: ").FontSize(8).Bold();
                                t.Span($"{Orcamento.Detalhe}").FontSize(8);
                            });

                            grid.Item(4);
                        });
                    });
        }

        void ComporOutrosServicos(IContainer container)
        {
            container.Table(table =>
                    {
                        // step 1
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("OUTROS SERVIÇOS").FontSize(10).Bold();
                        });

                        if (Orcamento.OutrosServicos.Any())
                        {
                            table.Cell().Table(t2 =>
                            {
                                t2.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(200);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                t2.Header(h2 =>
                                {
                                    h2.Cell().Element(CellStyle).AlignLeft().Text("Descrição").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignCenter().Text("Unitário").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignCenter().Text("Qtd").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignRight().Text("Total").Style(FontStyle()).Bold();
                                });

                                Orcamento.OutrosServicos.ForEach(serv =>
                                {
                                    t2.Cell().Element(CellStyle).AlignLeft().Text(serv.Descricao).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignCenter().Text(serv.ValorUnitario).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignCenter().Text(serv.Quantidade).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignRight().Text(serv.ValorTotal).Style(FontStyle().Bold());
                                });
                            });
                        }
                        else
                        {
                            table.Cell().Text("Nenhum serviço adicional cadastrado neste orçamento").FontSize(8);
                        }
                    });
        }

        void ComporMaoDeObra(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("MÃO DE OBRA").FontSize(10).Bold();
                        });

                        table.Cell().Element(CellStyle).Table(t2 =>
                        {
                            t2.ColumnsDefinition(col2 =>
                            {
                                col2.RelativeColumn();
                                col2.RelativeColumn();
                                col2.RelativeColumn();
                            });

                            t2.Header(h2 =>
                            {
                                h2.Cell().AlignLeft().Element(CellStyle).Text("Valor Hora Técnica").Style(FontStyle()).Bold();
                                h2.Cell().AlignCenter().Element(CellStyle).Text("Previsão de Horas").Style(FontStyle()).Bold();
                                h2.Cell().AlignRight().Element(CellStyle).Text("Valor Total Hora Técnica").Style(FontStyle()).Bold();
                            });

                            t2.Cell().Element(CellStyle).AlignLeft().Text(string.Format("{0:C}", Orcamento.MaoDeObra.ValorHoraTecnica)).Style(FontStyle());
                            t2.Cell().Element(CellStyle).AlignCenter().Text(Orcamento.MaoDeObra.PrevisaoHoras).Style(FontStyle());
                            t2.Cell().Element(CellStyle).AlignRight().Text(string.Format("{0:C}", Orcamento.MaoDeObra.ValorTotal)).Style(FontStyle().Bold());
                        });
                    });
        }

        void ComporDeslocamento(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DESLOCAMENTO").FontSize(10).Bold();
                        });

                        table.Cell().Element(CellStyle).Table(t2 =>
                        {
                            t2.ColumnsDefinition(col2 =>
                            {
                                col2.RelativeColumn();
                                col2.RelativeColumn();
                                col2.RelativeColumn();
                            });

                            t2.Header(h2 =>
                            {
                                h2.Cell().AlignLeft().Element(CellStyle).Text("Valor Unitário KM Rodado").Style(FontStyle()).Bold();
                                h2.Cell().AlignCenter().Element(CellStyle).Text("KM(Percurso Ida e Volta)").Style(FontStyle()).Bold();
                                h2.Cell().AlignRight().Element(CellStyle).Text("Valor Total KM Rodado").Style(FontStyle()).Bold();
                            });

                            t2.Cell().Element(CellStyle).AlignLeft().Text(string.Format("{0:C}", Orcamento.OrcamentoDeslocamento.ValorUnitarioKmRodado)).Style(FontStyle());
                            t2.Cell().Element(CellStyle).AlignCenter().Text(Orcamento.OrcamentoDeslocamento.QuantidadeKm).Style(FontStyle());
                            t2.Cell().Element(CellStyle).AlignRight().Text(string.Format("{0:C}", Orcamento.OrcamentoDeslocamento.ValorTotalKmRodado)).Style(FontStyle().Bold());
                        });

                        table.Cell().Element(CellStyle).Table(t2 =>
                        {
                            t2.ColumnsDefinition(col2 =>
                            {
                                col2.RelativeColumn();
                                col2.RelativeColumn();
                                col2.RelativeColumn();
                            });

                            t2.Header(h2 =>
                            {
                                h2.Cell().Element(CellStyle).AlignLeft().Text("Valor Hora em Deslocamento").Style(FontStyle()).Bold();
                                h2.Cell().Element(CellStyle).AlignCenter().Text("Qtd 1h a cada 60km rodados").Style(FontStyle()).Bold();
                                h2.Cell().Element(CellStyle).AlignRight().Text("Valor Total Hora Deslocamento").Style(FontStyle()).Bold();
                            });

                            t2.Cell().Element(CellStyle).AlignLeft().Text(string.Format("{0:C}", Orcamento.OrcamentoDeslocamento.ValorHoraDeslocamento)).Style(FontStyle());
                            t2.Cell().Element(CellStyle).AlignCenter().Text($"{Orcamento.OrcamentoDeslocamento.QuantidadeHoraCadaSessentaKm:0.00}").Style(FontStyle());
                            t2.Cell().Element(CellStyle).AlignRight().Text(string.Format("{0:C}", Orcamento.OrcamentoDeslocamento.ValorTotalKmDeslocamento)).Style(FontStyle().Bold());
                        });

                    });
        }

        void ComporMaterialUtilizado(IContainer container)
        {
            container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("MATERIAL A SER UTILIZADO").FontSize(10).Bold();
                        });

                        if (Orcamento.Materiais.Any())
                        {
                            table.Cell().Table(t2 =>
                            {
                                t2.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(130);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(160);
                                    columns.RelativeColumn();
                                });
                                t2.Header(h2 =>
                                {
                                    h2.Cell().Element(CellStyle).Text("Código").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).Text("Descrição").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignCenter().Text("Qtd").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignCenter().Text("Valor Unitário").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignCenter().Text("Desconto a base de troca(*)").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignRight().Text("Valor Total Peças").Style(FontStyle()).Bold();
                                });

                                Orcamento.Materiais.ForEach(mat =>
                                {
                                    t2.Cell().Element(CellStyle).Text(mat.CodigoMagnus).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).Text(mat.Descricao).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignCenter().Text(mat.Quantidade).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignCenter().Text(string.Format("{0:C}", mat.ValorUnitario)).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignCenter().Text(string.Format("{0:C}", mat.ValorDesconto)).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignRight().Text(string.Format("{0:C}", mat.ValorTotal)).Style(FontStyle().Bold());
                                });
                            });
                        }
                        else
                        {
                            table.Cell().Text("Nenhum material cadastrado neste orçamento").FontSize(8);
                        }
                    });
        }

        void ComporDescontos(IContainer container)
        {
            container.Table(table =>
                    {
                        // step 1
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("DESCONTOS").FontSize(10).Bold();
                        });

                        if (Orcamento.Descontos.Any())
                        {
                            table.Cell().Table(t2 =>
                            {
                                t2.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(100);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();

                                });
                                t2.Header(h2 =>
                                {
                                    h2.Cell().Element(CellStyle).AlignLeft().Text("Motivo").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignCenter().Text("Tipo").Style(FontStyle()).Bold();
                                    h2.Cell().Element(CellStyle).AlignRight().Text("Total").Style(FontStyle()).Bold();

                                });

                                Orcamento.Descontos.ForEach(desc =>
                                {
                                    t2.Cell().Element(CellStyle).AlignLeft().Text(desc.Motivo).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignCenter().Text(desc.NomeTipo).Style(FontStyle());
                                    t2.Cell().Element(CellStyle).AlignRight().Text(desc.ValorTotal).Style(FontStyle()).Bold();
                                });
                            });
                        }
                        else
                        {
                            table.Cell().Text("Nenhum desconto cadastrado neste orçamento").FontSize(8);
                        }
                    });
        }

        void ComporTotal(IContainer container)
        {
            container.Table(table =>
                    {
                        // step 1
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("TOTAL DO ORÇAMENTO").FontSize(10).Bold();
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle);
                        });

                        table.Cell().Element(CellStyle).Text("Valor Total de Descontos").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(string.Format("{0:C}", Orcamento.ValorTotalDesconto)).Style(FontStyle().Bold());
                        table.Cell().Element(CellStyle).Text("Valor Total").Style(FontStyle());
                        table.Cell().Element(CellStyle).AlignRight().Text(string.Format("{0:C}", Orcamento.ValorTotal)).Style(FontStyle().Bold());
                    });
        }

        void ComporCondicoes(IContainer container)
        {
            container.Table(table =>
                    {
                        // step 1
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Element(TitleStyle).Text("CONDIÇÕES").FontSize(10).Bold();
                        });

                        table.Cell().Element(CellStyle).Text("Validade Orçamento: 3 dias").Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Impostos Inclusos: Sim").Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("Os valores deste orçamento são apenas uma previsão. O efetivo faturamento será efetuado de acordo com os serviços executados e comprovados em Relatórios Técnicos, assinados pelo cliente.").Style(FontStyle());
                        table.Cell().Element(CellStyle).Text("(*)O valor de desconto a base de troca está condicionado ao recolhimento da peça substituída").Style(FontStyle());
                    });
        }
    }
}

