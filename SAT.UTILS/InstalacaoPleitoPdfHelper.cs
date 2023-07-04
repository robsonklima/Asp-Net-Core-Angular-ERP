using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SAT.MODELS.Entities;

namespace SAT.UTILS
{
    public class InstalacaoPleitoPdfHelper : IDocument
    {
        public InstalacaoPleito pleito { get; }
        public List<Instalacao> inst { get; }

        public InstalacaoPleitoPdfHelper(InstalacaoPleito instalacaoPleito, List<Instalacao> instalacoes)
        {
            pleito = instalacaoPleito;
            inst = instalacoes;
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
                    page.Size(PageSizes.A4.Landscape());
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Black);

            container.Row(row =>
            {
                row.Spacing(20);

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"BORDERÔ - {pleito.InstalacaoTipoPleito?.NomeTipoPleito}").Style(titleStyle);
                    column.Item().Text($"CÓD.: {pleito.CodInstalPleito} - {inst[0]?.Equipamento?.NomeEquip}");
                });

                row.ConstantItem(280).Column(column =>
                {
                    column.Item().Row(async cr =>
                    {
                        cr.Spacing(20);

                        cr.RelativeItem().Column(t =>
                        {
                            t.Item().AlignRight().Text($"Perto S.A").SemiBold().FontSize(10);
                            t.Item().AlignRight().Text($"Tecnologia para Bancos e Varejo").FontSize(10);
                            t.Item().AlignRight().Text($"www.perto.com.br").FontSize(10);
                        });

                        using (HttpClient webClient = new HttpClient())
                        {
                            byte[] dataArr = await webClient.GetAsync("https://sat.perto.com.br/sat.v2.frontend/assets/images/logo/logo.png")
                                .Result.Content.ReadAsByteArrayAsync();

                            cr.ConstantItem(35).AlignRight().Image(dataArr, ImageScaling.FitArea);
                        }
                    });
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Padding(10).Column(column =>
            {
                column.Spacing(1);
                column.Item().Element(ComporDadosCliente);
                column.Item().Element(ComporTabelaPrincipal);
                column.Item().Element(ComporTotal);
                column.Item().Element(ComporProtocolo);
                column.Item().Element(ComporInformacoesPerto);
            });
        }

        void ComporDadosCliente(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(TitleStyle).Text("").FontSize(10).Bold();
                });
                table.Cell().Row(tr =>
                {
                    tr.RelativeItem().Column(column =>
                    {
                        column.Item().Element(CellStyle).Text(pleito.DataHoraCad).Style(FontStyle());
                        column.Item().Element(CellStyle).Text(pleito.Contrato?.Cliente?.RazaoSocial).Style(FontStyle());
                        column.Item().Element(CellStyle).Text(pleito.Contrato?.Cliente?.Endereco).Style(FontStyle());
                        column.Item().Element(CellStyle).Text(pleito.Contrato?.Cliente?.Cep).Style(FontStyle());
                        column.Item().Element(CellStyle).Text(pleito.Contrato?.Cliente?.Cidade?.NomeCidade).Style(FontStyle());
                        column.Item().Element(CellStyle).Text(pleito.Contrato?.Cliente?.Cidade?.UnidadeFederativa?.SiglaUF).Style(FontStyle());
                        column.Item().Element(CellStyle).Text(pleito.InstalacaoTipoPleito?.IntroTipoPleito).Style(FontStyle());
                    });
                });
            });
        }

        void ComporProtocolo(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(TitleStyle).Text("").FontSize(10).Bold();
                });
                table.Cell().Row(tr =>
                {
                    tr.RelativeItem().Border(1).Column(column =>
                    {
                         column.Item().Element(CellStyle).AlignCenter().Text($"PROTOCOLAR ESTA VIA").Style(FontStyle());
                        column.Item().Element(CellStyle).AlignCenter().Text($"Data ____ / ___________ / _______").Style(FontStyle());
                        column.Item().Element(CellStyle).AlignCenter().Text($"Recebido por ________________________").Style(FontStyle());
                        column.Item().Element(CellStyle).AlignCenter().Text($"(Nome Legível)").Style(FontStyle());
                        column.Item().Element(CellStyle).AlignCenter().Text($"________________________").Style(FontStyle());
                        column.Item().Element(CellStyle).AlignCenter().Text($"(Assinatura com Cabrimbo)").Style(FontStyle());
                    });
                });
            });
        }

        void ComporInformacoesPerto(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(TitleStyle).Text("").FontSize(10).Bold();
                });
                table.Cell().Row(tr =>
                {
                    tr.RelativeItem().Column(column =>
                    {
                        column.Item().Element(CellStyle).AlignCenter().Text($"PERTO S/A - Periféricos para Automação").Style(FontStyle());
                        column.Item().Element(CellStyle).AlignCenter().Text($"FÁBRICA = > Rua Nissin Castiel, 640 Distrito Industrial CEP 94060-520 Gravataí/RS Brasil Fone (51) 489-8700 Fax (51) 489-1424").Style(FontStyle());
                        column.Item().Element(CellStyle).AlignCenter().Text($"SUPORTE SERVIÇOS = > Rua São Paulo, 82 - Alphaville - Barueri - São Paul/SP - Fone (11) 4196-4300/4304 Fax (11 ) 4133-4106").Style(FontStyle());
                    });
                });
            });
        }

        void ComporTotal(IContainer container)
        {   
            var qtd = 0;
            decimal valorInstalacao = 0;
            decimal valorUnitario = 0;

            inst.ForEach(item =>
            {
                qtd = qtd + 1;

                var valorInstalacaoItens = item?.Contrato?.ContratosEquipamento?
                             .Where(ce => ce?.CodEquip == item?.CodEquip)
                             .Sum(ce => ce.VlrInstalacao); 

                valorInstalacao = valorInstalacao + (valorInstalacaoItens != null ? (decimal)valorInstalacaoItens : 0);

                if (item != null) 
                {
                    decimal valorUnitarioItens = (decimal)item.Contrato.ContratosEquipamento
                            .Where(ce => ce.CodEquip == item.CodEquip)
                            .Sum(ce => ce.VlrUnitario);

                    valorUnitario = valorUnitario + valorUnitarioItens;
                }
            });

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.ConstantColumn(20);
                    columns.ConstantColumn(45);
                });

                table.Cell().BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                {
                    t.Span("Totais").FontSize(6).Bold();
                });

                table.Cell()
                .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                {
                    t.Span($"{qtd}").FontSize(6);
                });

                if (pleito.CodInstalTipoPleito == 7)
                {
                    table.Cell()
                        .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.8))).FontSize(6);
                        });
                }
                else if (pleito.CodInstalTipoPleito == 8)
                {
                    table.Cell()
                        .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.2))).FontSize(6);
                        });
                }
                else if (pleito.CodInstalTipoPleito == 9)
                {
                    table.Cell()
                        .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.9))).FontSize(6);
                        });
                }
                else if (pleito.CodInstalTipoPleito == 10)
                {
                    table.Cell()
                        .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.1))).FontSize(6);
                        });
                }   
                else if (pleito.CodInstalTipoPleito == 11)
                {
                    table.Cell()
                        .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.5))).FontSize(6);
                        });
                }                                                 
                else
                {
                    table.Cell()
                        .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span(string.Format("{0:C}", valorInstalacao)).FontSize(6);
                        });
                }
            });
        }

        void ComporTabelaPrincipal(IContainer container)
        {
            container
                .PaddingTop(20)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.ConstantColumn(45);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(40);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(45);
                        columns.ConstantColumn(45);
                        columns.ConstantColumn(45);
                        columns.ConstantColumn(45);
                        columns.ConstantColumn(40);                        
                        columns.ConstantColumn(20);                                                
                        columns.ConstantColumn(45);
                    });

                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Lote").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Contrato").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("NF Venda").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("NF Remessa").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Prefixo").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Posto").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Agência").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Endereço").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Cidade").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("UF").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Filial").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Equipamento").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Num. Série").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Bem Trade In").FontSize(6).Bold();
                    });
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Pedido Compra").FontSize(6).Bold();
                    });                    
                    table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                    {
                        t.Span("Qtd").FontSize(6).Bold();
                    });

                    if (pleito.CodInstalTipoPleito == 7)
                    {
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span("Valor Entregue - 80%").FontSize(6).Bold();
                        });
                    }
                    else if (pleito.CodInstalTipoPleito == 8)
                    {
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span("Valor Instalado - 20%").FontSize(6).Bold();
                        });
                    }
                    else
                    {
                        table.Cell().BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                        {
                            t.Span("Valor Instalação").FontSize(6).Bold();
                        });
                    }

                    inst.ForEach(item =>
                    {
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item?.InstalacaoLote?.NomeLote).FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span(item?.EquipamentoContrato?.Contrato?.NomeContrato).FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.InstalacaoNFVenda?.NumNFVenda}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.NFRemessa}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.LocalAtendimentoIns?.NumAgencia}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.LocalAtendimentoIns?.DCPosto}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.LocalAtendimentoIns?.NomeLocal}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.LocalAtendimentoIns?.Endereco}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.LocalAtendimentoIns?.Cidade?.NomeCidade}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.LocalAtendimentoIns?.Cidade?.UnidadeFederativa?.SiglaUF}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.Filial?.NomeFilial}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.Equipamento?.NomeEquip}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.EquipamentoContrato?.NumSerie}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.BemTradeIn}").FontSize(6);
                            });
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{item?.PedidoCompra}").FontSize(6);
                            });                            
                        table.Cell()
                            .BorderBottom(1).BorderTop(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                            {
                                t.Span($"{1}").FontSize(6);
                            });

                        var valorInstalacao = item?.Contrato?.ContratosEquipamento?
                             .Where(ce => ce?.CodEquip == item?.CodEquip)
                             .Sum(ce => ce.VlrInstalacao);

                        var valorUnitario = item?.Contrato?.ContratosEquipamento?
                             .Where(ce => ce?.CodEquip == item?.CodEquip)
                             .Sum(ce => ce.VlrUnitario);

                        if (pleito.CodInstalTipoPleito == 7)
                        {
                            table.Cell()
                                .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                                {
                                    t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.8))).FontSize(6);
                                });
                        }
                        else if (pleito.CodInstalTipoPleito == 8)
                        {
                            table.Cell()
                                .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                                {
                                    t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.2))).FontSize(6);
                                });
                        }
                        else if (pleito.CodInstalTipoPleito == 9)
                        {
                            table.Cell()
                                .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                                {
                                    t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.9))).FontSize(6);
                                });
                        }
                        else if (pleito.CodInstalTipoPleito == 10)
                        {
                            table.Cell()
                                .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                                {
                                    t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.1))).FontSize(6);
                                });
                        }   
                        else if (pleito.CodInstalTipoPleito == 11)
                        {
                            table.Cell()
                                .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                                {
                                    t.Span(string.Format("{0:C}", (valorUnitario * (decimal)0.5))).FontSize(6);
                                });
                        }                                                 
                        else
                        {
                            table.Cell()
                                .BorderBottom(1).PaddingTop(1).PaddingBottom(1).Text(t =>
                                {
                                    t.Span(string.Format("{0:C}", valorInstalacao)).FontSize(6);
                                });
                        }
                    });
                });
        }
    }
}