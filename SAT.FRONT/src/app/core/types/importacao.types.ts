import { TipoIntervencaoConst, TipoIntervencaoEnum } from './tipo-intervencao.types';

// export class ImportacaoColunasType
// {
//     public atualizaImplantacao = [
//         { title: 'Código Instalacao', width: 75 },
//         { title: 'Série' },
//         { title: 'NF Remessa' }
//     ];

// }

export const ImportacaoColunas = {

    atualizaImplantacao: [
        { title: 'Código Instalacao', width: 150 },
        { title: 'Série', width: 70  },
        { title: 'NF Remessa', width: 120  }
    ],

    aberturaChamados: [
        { title: 'Cliente', width: 80 },
        { title: 'Série Equipamento', width: 160  },
        { title: 'Número Agência', width: 140  },
        { title: 'DC Posto', width: 90  },
        { title: 'Defeito', width: 80  },
        { title: 'Intervenção', width: 100 , type: 'dropdown', source: TipoIntervencaoConst  },
        { title: 'OS Quarteirizada', width: 140  },
        { title: 'OS Cliente', width: 90  },
    ],

    criacaoLotes: [
        { title: 'Cliente', width: 80 },
        { title: 'Contrato', width: 80  },
        { title: 'Algum valor', width: 120  },
        { title: 'Outro Valor', width: 120  }
    ],

}

export const ImportacaoDados = {

    atualizaImplantacao: [
        {
            codInstalacao: 0,
            numSerie: '0',
            nfRemessa: 0
        }
    ],

    aberturaChamados: [
        {
            nomeFantasia: '',
            numSerie: '',
            numAgenciaBanco: '',
            dcPosto: '',
            defeitoRelatado: '',
            tipoIntervencao: [],
            numOSQuarteirizada: '',
            numOSCliente: '',
        }
    ],

    criacaoLotes: [
        {
            codInstalacao:  0,
            numSerie: '0',
            nfRemessa: 0
        }
    ]


}

export enum ImportacaoEnum
{
    ATUALIZA_IMPLANTACAO,
    ABERTURA_CHAMADOS,
    CRIACAO_LOTES
}