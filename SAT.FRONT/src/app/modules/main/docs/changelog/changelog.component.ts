import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'changelog',
    templateUrl: './changelog.component.html',
    styles: [''],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChangelogComponent
{
    changelog: any[] = [
        {
            version: 'v1.0.0',
            releaseDate: '01 de Março de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Criação do sistema',
                    ],

                },
            ]
        },
        {
            version: 'v1.0.1',
            releaseDate: '19 de Outubro de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Opção de cancelamento de transferência de chamado',
                        'Filtro pelo técnico na tela de chamados',
                        'Informações sobre o técnico ao passar o mouse sobre a coluna',
                        'Status de peças faltantes ao passar o mouse sobre a coluna',
                        'Coluna de status de SLA',
                    ],

                },
                {
                    type: 'Correções',
                    list: [
                        'Mantém filtro na tela de chamados',
                        'Nome do técnico apenas para chamados transferidos',
                        'Validação da RAT'
                    ],
                }
            ]
        },
        {
            version: 'v1.0.2',
            releaseDate: '19 de Outubro de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Técnico padrão na RAT quando o chamado estiver no status transferido'
                    ],

                },
                {
                    type: 'Correções',
                    list: [
                        'Filtro de equipamento por texto'
                    ],
                }
            ]
        },
        {
            version: 'v1.0.3',
            releaseDate: '20 de Outubro de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Mapa de roteirização'
                    ],

                },
                {
                    type: 'Correções',
                    list: [
                        'Melhoria performance Agenda Técnicos'
                    ],
                }
            ]
        },
        {
            version: 'v1.0.4',
            releaseDate: '26 de Outubro de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Impressão da OS',
                        'Filtro na agenda técnicos',
                        'Arrastar para transferir na agenda técnicos'
                    ],

                },
                {
                    type: 'Correções',
                    list: [
                        'Melhorias no filtro por equipamento na listagem de OS'
                    ],
                }
            ]
        },
        {
            version: 'v1.0.5',
            releaseDate: '29 de Outubro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Ajuste no filtro de técnico da RAT',
                        'Ajustes na impressão de PDF',
                        'Ajustes na Agenda Técnico'
                    ],
                }
            ]
        },
        {
            version: 'v1.0.6',
            releaseDate: '01 de Novembro de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Filtro por filial na Agenda Técnico',
                        'Ajustes na pesquisa de técnicos da RAT',
                    ],
                }
            ]
        },
        {
            version: 'v1.0.7',
            releaseDate: '03 de Novembro de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Carregamento de chamados da filial do perfil de usuário',
                        'Manter filtro de filial após limpar filtros',
                    ],
                }
            ]
        },
        {
            version: 'v1.0.8',
            releaseDate: '10 de Novembro de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Atualização de Dashboard',
                        'Inclusão de módulos de ponto e despesa',
                    ],
                }
            ]
        },
        {
            version: 'v1.0.9',
            releaseDate: '16 de Novembro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Filtro tela de chamados',
                    ],
                }
            ]
        },
        {
            version: 'v1.1.0',
            releaseDate: '01 de Dezembro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Agendamento da OS',
                    ],
                },
                {
                    type: 'Adições',
                    list: [
                        'Filtro de chamados na Agenda Técnico',
                        'Realocação de chamados',
                        'Validação de horário retroativo no agendamento da OS',
                        'Histórico de agendamentos da OS'
                    ],
                }
            ]
        },
        {
            version: 'v1.1.1',
            releaseDate: '03 de Dezembro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Ajustes no filtro de chamados da agenda',
                        'Ajustes na tela de contratos',
                    ],
                },
                {
                    type: 'Adições',
                    list: [
                        'Módulo de despesas'
                    ],
                }
            ]
        },
        {
            version: 'v1.1.2',
            releaseDate: '04 de Dezembro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Ajustes na tela de contratos',
                        'Ajustes na tela de créditos cartão',
                    ],
                },
                {
                    type: 'Adições',
                    list: [
                        'Módulo de ponto',
                        'Módulo de instalação'
                    ],
                }
            ]
        },
        {
            version: 'v1.1.3',
            releaseDate: '06 de Dezembro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Histórico Agenda Técnico',
                        'Filtro Tipo Equipamento',
                    ],
                }
            ]
        },
        {
            version: 'v1.1.4',
            releaseDate: '07 de Dezembro de 2021',
            changes: [
                {
                    type: 'Adições',
                    list: [
                        'Ponto na Agenda'
                    ],
                }
            ]
        },
        {
            version: 'v1.1.5',
            releaseDate: '08 de Dezembro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Modificação do Layout dos chamados',
                        'Ajustes nos filtros'
                    ],
                }
            ]
        },
        {
            version: 'v1.1.6',
            releaseDate: '14 de Dezembro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Modificação da Agenda',
                        'Alertas nos chamados',
                        'Sistema de notificações'
                    ],
                }
            ]
        },
        {
            version: 'v1.1.7',
            releaseDate: '16 de Dezembro de 2021',
            changes: [
                {
                    type: 'Correções',
                    list: [
                        'Ajustes na tela de contratos',
                    ],
                },
                {
                    type: 'Adições',
                    list: [
                        'Layout da Agenda Técnico'
                    ],
                }
            ]
        }
    ];
    constructor () { }
}