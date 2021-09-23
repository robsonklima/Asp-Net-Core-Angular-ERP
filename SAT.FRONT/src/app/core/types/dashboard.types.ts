export interface Dashboard {
    id: number;
    nome: string;
    slides: Slide[];
} 

export interface Slide {
    id: number;
    nome: string
    largura: Largura;
}

export type Largura = 1 | 2 | 3 | 4;

export const dashboardsConst: Dashboard[] = [
    {
        id: 1,
        nome: 'Performance das Filiais',
        slides: [
            {
                id: 1,
                nome: 'Status das Filiais',
                largura: 2,
            },
            {
                id: 2,
                nome: 'Indicadores das Filiais',
                largura: 2,
            },
        ]
    },
    {
        id: 2,
        nome: 'Resultado Geral do DSS',
        slides: [
            {
                id: 1,
                nome: 'Resultado Geral do DSS',
                largura: 2,
            },
            {
                id: 2,
                nome: 'Chamados Mais Antigos',
                largura: 2,
            },
        ]
    }
];