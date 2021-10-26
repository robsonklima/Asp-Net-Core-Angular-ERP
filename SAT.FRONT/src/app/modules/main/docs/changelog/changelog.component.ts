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
        }
    ];

    constructor () { }
}