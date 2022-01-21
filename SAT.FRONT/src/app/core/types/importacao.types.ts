import { TipoIntervencaoConst} from './tipo-intervencao.types';


export interface ImportacaoAberturaOrdemServico {
    nomeFantasia: string;
    numSerie: string;
    numAgenciaBanco: string;
    dcPosto: string;
    defeitoRelatado: string;
    tipoIntervencao: string;
    numOSQuarteirizada: string;
    numOSCliente: string;
    codUsuarioCad: string;
    dataHoraCad?: string;
}

export const ImportacaoColunas = {

    atualizaImplantacao: [
        { title: 'Código', width: 70},
        { title: 'Série', width: 60  },
        { title: 'Bem Trade In', width: 110  },
        { title: 'NF Venda', width: 90  },
        { title: 'NF Venda - Data', width: 130  },
        { title: 'NF Remessa', width: 100  },
        { title: 'NF - Data Expedição', width: 150  },
        { title: 'Data Expedição', width: 130  },
        { title: 'Transportadora', width: 130  },
        { title: 'Previsão de entrega', width: 150 },
        { title: 'Data da Entrega', width: 130  },
        { title: 'Data de Instalação', width: 150  },
        { title: 'Resp. pelo Recebimento', width: 180  },
        { title: 'Matrícula do Responsável', width: 180  },
    ],

    aberturaChamados: [
        { title: 'Cliente', width: 80 },
        { title: 'ID Equipamento', width: 140  },
        { title: 'Série Equipamento', width: 160  },
        { title: 'Número Agência', width: 140  },
        { title: 'DC Posto', width: 90  },
        { title: 'Defeito', width: 80  },
        { title: 'Intervenção', width: 100 , type: 'dropdown', autocomplete:true , source: TipoIntervencaoConst  },
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
            codInstalacao: '',
            numSerie: '',
            bemTradeIn:'',
            nfVenda: '',
            nfVendaData: '',
            nfremessa: '',
            dataNfremessa: '',
            dataExpedicao: '',
            nomeTransportadora: '',
            dataSugEntrega: '',
            dataConfEntrega: '',
            dataConfInstalacao: '',
            nomeRespBancoBt: '',
            numMatriculaBt: '',
        }
    ],

    aberturaChamados: [
        {
            nomeFantasia: '',
            codEquipContrato: 0,
            numSerie: '',
            numAgenciaBanco: 0,
            dcPosto: 0,
            defeitoRelatado: '',
            tipoIntervencao: [],
            numOSQuarteirizada: '',
            numOSCliente: '',
        }
    ],

    criacaoLotes: [
        {
            nomeCliente:  '0',
            nroContrato: '0',
            algumaCoisa1: '0',
            algumaCoisa2: '0'
        }
    ]


}

export enum ImportacaoEnum
{
    ATUALIZA_IMPLANTACAO,
    ABERTURA_CHAMADOS,
    CRIACAO_LOTES
}

