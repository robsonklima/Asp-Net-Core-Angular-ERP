import { BackgroundGeolocationConfig } from "@ionic-native/background-geolocation";

export class Config {
  //public static API_URL = 'http://localhost:60687/api/';
  public static API_URL = 'https://sat.perto.com.br/prjSATWebAPI/api/';

  public static NOME_APP = "App Técnicos";
  public static VERSAO_APP = "0.0.109";
  public static QTD_MAX_FOTOS_POR_ATENDIMENTO = 3;
  public static GOOGLE_KEY = 'AIzaSyCdX8k9LPBt6c3gNKOMCWlgyYmrV5AMkf8';
  public static GOOGLE_PLAY_URL = 'https://play.google.com/store/apps/details?id=perto.sat.apptecnicos';
  public static GOOGLE_PLAY_NOME_APP = 'perto.sat.apptecnicos';
  public static OPEN_CAMERA = 'net.sourceforge.opencamera';
  public static CAMERA = 'com.motorola.camera2';
  public static POS_CONFIG = { enableHighAccuracy: true, maximumAge: 0, timeout: 20000 };
  public static TIPO_CAUSA = { MAQUINA: 1, EXTRA_MAQUINA: 2 };
  public static TIPO_INTERVENCAO = { 
    ALTERAÇÃO_ENGENHARIA:	1, CORRETIVA: 2, DESINSTALAÇÃO: 3, INSTALAÇÃO: 4, ORÇAMENTO: 5, PREVENTIVA: 6, REINSTALAÇÃO: 7,
    INSPECAO_TECNICA: 10, REMANEJAMENTO: 11, TREINAMENTO: 13, AUTORIZACAO_DESL: 14, ORC_APROVADO: 17, ORC_REPROVADO: 18,
    ORC_PEND_APROVAÇÃO_CLIENTE: 19, ORC_PEND_FILIAL_DETALHAR_MOTIVO: 20, CORRETIVAPOS_REINCIDENTES: 22, HELPDESK: 23,
    TROCA_VELOHC: 24, ATUALIZACAO: 25, LAUDO_TECNICO: 26, HELP_DESK_DSS: 27, PREVENTIVA_GERENCIAL: 28, 
    LISTA_ATUALIZACAO_EQUIPAMENTO: 29, VANDALISMO: 30, MANUTENCAO_GERENCIAL: 31, COFRE: 32
   };
  public static STATUS_SERVICO = { 
    ABERTO: 1, CANCELADO: 2, FECHADO: 3, ORCAMENTO: 4, PARCIAL: 5, PECAS_LIBERADAS: 6, PECAS_PENDENTES: 7, TRANSFERIDO: 8,
    PEÇA_TRANSITO: 9, PECA_FALTANTE: 10, PECA_SEPARADA: 11, FECHADO_P_ANALISE: 12, ORCAMENTO_APROVADO: 13,
    AGUARDANDO_CONTATO_CLIENTE: 14, AGUARDANDO_DECLARACÃO: 15, CANCELADO_COM_ATENDIMENTO: 16
   };
  public static EQUIPAMENTO = { TPC_4110_290_01_969: 362, TPC_4110: 90 };
  public static INT_SINC_BD_LOCAL_DIAS = 2;
  public static INT_SINC_CHAMADOS_MILISEG = 60 * 1000; //300000;
  public static INT_MIN_SINC_CHAMADOS_SEG = 10;
  public static CERCA_ELETRONICA = [
    { filial: 'FPR', distancia: 2.00 }, { filial: 'FRS', distancia: 2.55 },
    { filial: 'FDF', distancia: 5.65 }, { filial: 'FES', distancia: 3.43 },
    { filial: 'FSP', distancia: 2.47 }, { filial: 'FBU', distancia: 2.18 },
    { filial: 'FRN', distancia: 3.30 }, { filial: 'FCP', distancia: 2.25 },
    { filial: 'FSC', distancia: 2.12 }, { filial: 'FMS', distancia: 2.04 },
    { filial: 'FTO', distancia: 2.27 }, { filial: 'FPA', distancia: 3.34 },
    { filial: 'FRO', distancia: 3.21 }, { filial: 'FMG', distancia: 2.69 },
    { filial: 'FPE', distancia: 2.70 }, { filial: 'FGO', distancia: 3.15 },
    { filial: 'FMT', distancia: 4.65 }, { filial: 'FMA', distancia: 2.18 },
    { filial: 'FBA', distancia: 4.56 }, { filial: 'FCE', distancia: 2.35 },
    { filial: 'FRJ', distancia: 4.44 }, { filial: 'FAM', distancia: 4.78 }
  ];
  public static EQUIPAMENTOS_POS = { POS_VELOH_3: 289, POS_VELOH_G: 172 };
  public static COR_RGB = { 
    CINZA: '#9E9E9E',
    VERDE: '#8BC34A', 
    VERMELHO: '#D32F2F', 
    LARANJA: '#FFC107',
    AZUL: '#00BCD4'
  };
  public static FOTO = { 
    WIDTH: 360, 
    HEIGHT: 640, 
    QUALITY: 90, 
    STATUS: { 
      ENVIADA: 'ENVIADA', 
      PENDENTE_ENVIO: 'PENDENTE_ENVIO' 
    },
    MODALIDADES: {
      RAT: 'RAT',
      TERMO_ACEITE: 'TERMO_ACEITE',
      TERMO_ENTREGA: 'TERMO_ENTREGA',
      TERMO_RECOLHIMENTO: 'TERMO_RECOLHIMENTO',
      NOTA_FISCAL: 'NOTA_FISCAL'
    }
  };
  public static CHECKIN = { 
    STATUS: { 
      ENVIADO: 'ENVIADO', 
      PENDENTE_ENVIO: 'PENDENTE_ENVIO' 
    }
  };
  public static MAURON_85_CONFIG: BackgroundGeolocationConfig = {
    desiredAccuracy: 7,
    stationaryRadius: 0,
    distanceFilter: 0,
    debug: false,
    stopOnTerminate: true,
    startForeground: true,
    interval: 10 * 60000,
    fastestInterval: 5 * 60000,
    activitiesInterval: 5 * 60000,
    notificationsEnabled: true,
    notificationTitle: 'App Técnicos - Meus Chamados',
    notificationText: 'Sincronizando Chamados',
    maxLocations: 100
  };
  public static CHAMADO = {TRANSFERIDO: 8, FECHADO: 3 };
  public static ACAO = {PENDENCIA_PECA: { CODACAO: 19 }};
  public static USUARIO_PERFIL = {
    ADMINISTRADOR_SISTEMA: 1, FILIAL_SUPORTE_TECNICO: 32,
    FILIAL_TECNICO_DE_CAMPO: 35, FILIAL_SUPORTE_TECNICO_DE_CAMPO: 79,
    FILIAL_TECNICO_DE_CAMPO_COM_CHAMADOS: 84,
  };
  public static CLIENTE = {
    BB: 1, RIO_CARD: 256, BRINKS: 347, VLT_CARIOCA: 444, BVA_BRINKS: 479, 
    METRO_RIO: 538, CEF: 58, BNB: 253, PROTEGE: 490, BANRISUL: 2, BRB: 197,
    SICREDI: 88, SAQUE_PAGUE: 434, PERTO_PARKING: 532, BANESTES: 331,
    BANCO_DA_AMAZONIA: 109
  };
  public static LOG = {
    TIPOS: { SUCCESS: 'SUCCESS', ERROR: 'ERROR', WARNING: 'WARNING' } 
  };
  public static MSG = {
    OK: 'Ok',
    ALERTA: 'Alerta!',
    ATUALIZACAO: 'Atualização',
    CANCELAR: 'Cancelar',
    ATUALIZAR: 'Atualizar',
    AGUARDE: 'Aguarde',
    ENVIANDO_LOGS: 'Enviando logs para o servidor',
    CONFIRMAR: 'Confirmar',
    CONFIRMACAO: 'Confirmação',
    CHECKIN: 'Checkin',
    CHECKOUT: 'Checkout',
    DESLOCAMENTO: 'Deslocamento',
    INICIANDO_SINCRONIZACAO: 'Preparando sincronização de chamados junto ao servidor',
    FECHANDO_CHAMADO: 'Fechando Chamado',
    SALVANDO_CHAMADO_BASE_LOCAL: 'Salvando chamado no banco de dados do smartphone',
    RECURSO_NATIVO: 'Este recurso deve ser acessado no dispositivo',
    ERRO_PERMISSAO_CAMERA: 'Erro ao obter permissões para acessar a câmera',
    ERRO_RESPOSTA_DISPOSITIVO: 'O dispositivo não respondeu',
    ERRO_FOTO: 'Erro ao tirar a foto. Favor tentar novamente',
    ERRO_AO_SINCRONIZAR: 'Erro ao sincronizar junto ao servidor',
    CHAMADO_ENVIADO: 'Chamado enviado com sucesso',
    ERRO_AO_CONSULTAR_CHAMADOS_TECNICO: 'Não foi possível obter resposta do servidor',
    ERRO_UNIFICAR_CHAMADOS_API_STORAGE: 'Erro ao sincronizar chamados entre o servidor e o smartphone',
    ERRO_GRAVAR_CHAMADOS_API_STORAGE: 'Erro ao gravar chamados no smartphone',
    ERRO_TECNICO_NAO_ENCONTRADO: 'Favor realizar o login novamente',
    ERRO_AO_ENVIAR_CHAMADO_FECHADO: 'Erro ao enviar chamado para o servidor',
    ERRO_AO_ENVIAR_CHECKINS: 'Erro ao enviar os checkins para o servidor',
    ERRO_AO_ENVIAR_INTENCOES: 'Erro ao enviar os deslocamentos de atendimento para o servidor',
    ERRO_AO_OBTER_LOCALIZACAO: 'Erro ao obter a localização',
    ERRO_AO_ACESSAR_CAMERA: 'Erro ao acessar a câmera. Favor tentar novamente',
    ERRO_LOCAL_NAO_POSSUI_COORDENADAS: 'O local de atendimento não possui latitude e longitude cadastradas',
    ERRO_ENVIAR_CHAMADO: `Não foi possível enviar o chamado para o servidor`,
    CHECKIN_EM_ABERTO: 'Você possui checkin em aberto',
    CHECKOUT_EM_ABERTO: 'Você possui checkout em aberto',
    CHECKIN_CONFIRMACAO: 'Somente confirme o Checkin se você realmente estiver no local do atendimento',
    CHECKIN_REMOCAO: 'Deseja remover o checkin?',
    INTENCAO_REMOCAO: 'Deseja cancelar o deslocamento?',
    INTENCAO_CANCELADA: 'Deslocamento cancelado',
    INTENCAO_CONFIRMACAO: 'Deseja registrar sua intenção de deslocamento ao local de atendimento?',
    CHECKOUT_CONFIRMACAO: 'Somente confirme o checkout se você já concluiu o chamado e deixará o local de atendimento',
    GPS_DESATIVADO: 'GPS Desativado',
    GPS_DESATIVADO_ORIENTACAO: 'Favor ativar o GPS do seu smartphone para que o aplicativo possa sincronizar seus chamados',
    INTERNET_OFFLINE: "Verifique sua conexão com a internet",
    INTERNET_ONLINE: "Online",
    ENVIANDO_CHAMADO_FECHADO: `Enviando chamado para o servidor`,
    DEFEITOS_NAO_ENCONTRADOS_COMPONENTE: 'Não foi possível encontrar os defeitos deste componente',
    ACOES_NAO_ENCONTRADAS_COMPONENTE: 'Não foi possível encontrar as ações deste componente',
    NENHUM_CHAMADO_ENCONTRADO: 'Nenhum novo chamado encontrado',
    REMOVER_OS_CHAMADOS: 'Deseja remover os chamados do dispositivo?',
    REMOVER_OS_LOGS: 'Deseja remover os logs do dispositivo?',
    ENVIAR_OS_LOGS: 'Deseja enviar os logs para a equipe de desenvolvimento do aplicativo?',
    OBTENDO_LOCALIZACAO: 'Obtendo sua localização',
    ERRO_OBTER_ACOES_CAUSAS: 'Não foi possível carregar as Ações e Causas',
    ERRO_OBTER_DEFEITOS_CAUSAS: 'Não foi possível carregar os Defeitos e Causas',
    ERRO_OBTER_EQUIPAMENTOS_CAUSAS: 'Não foi possível carregar os Equipamentos e Causas',
    ERRO_OBTER_FERRAMENTAS: 'Não foi possível carregar os Ferramentas dos Técnicos',
    ERRO_OBTER_DEFEITOS_POS: 'Não foi possível carregar os Defeitos dos Equipamentos POS',
    ERRO_OBTER_STATUS_SERVICOS: 'Não foi possível carregar os Status de Serviços',
    ERRO_OBTER_MOTIVOS_CANCELAMENTO: 'Não foi possível carregar os Motivos de Cancelamento',
    ERRO_OBTER_MOTIVOS_COMUNICACAO: 'Não foi possível carregar os Motivos de Comunicação',
    ERRO_OBTER_TIPOS_COMUNICACAO: 'Não foi possível carregar os Tipos de Comunicação',
    ERRO_OBTER_OPERADORAS: 'Não foi possível carregar as Operadoras',
    ERRO_OBTER_EQUIPAMENTOS_POS: 'Não foi possível carregar os Equipamentos POS',
    ERRO_OBTER_PECAS: 'Não foi possível carregar as Peças',
    ERRO_OBTER_CAUSAS: 'Não foi possível carregar as Causas',
    ERRO_OBTER_DEFEITOS: 'Não foi possível carregar as Defeitos',
    ERRO_OBTER_ACOES: 'Não foi possível carregar as Ações',
    ERRO_OBTER_TIPOS_SERVICO: 'Não foi possível carregar os Tipos de Serviço',
    ERRO_ENVIAR_FOTOS_PARA_SERVIDOR: 'Erro ao enviar fotos ao servidor',
    ERRO_ENVIAR_FOTO: 'Erro ao enviar foto para o servidor',
    ATUALIZAR_DADOS_LOCAIS: 'É necessário atualizar os dados do aplicativo para prosseguir',
    CRIANDO_TAB_TIPOS_SERVICOS: "1/16: Configurando os Tipos de Serviço",
    CRIANDO_TAB_ACOES: "2/16: Configurando as Ações",
    CRIANDO_TAB_DEFEITOS: "3/16: Configurando os Defeitos",
    CRIANDO_TAB_CAUSAS: "4/16: Configurando as Causas",
    CRIANDO_TAB_PECAS: "5/16: Configurando as Peças",
    CRIANDO_TAB_EQUIPAMENTOS_POS: "6/16: Configurando os Equipamentos POS",
    CRIANDO_TAB_OPERADORAS: "7/16: Configurando as Operadoras",
    CRIANDO_TAB_TIPOS_COMUNICACAO: "8/16: Configurando os Tipos de Comunicação",
    CRIANDO_TAB_MOTIVOS_COMUNICACAO: "9/16: Configurando os Motivos de Comunicação",
    CRIANDO_TAB_MOTIVOS_CANCELAMENTO: "10/16: Configurando os Motivos de Cancelamento",
    CRIANDO_TAB_STATUS_SERVICO: "11/16: Configurando os Status de Serviços",
    CRIANDO_TAB_DEFEITOS_POS: "12/16: Configurando os Defeitos do POS",
    CRIANDO_TAB_FERRAMENTAS: "13/16: Configurando as Ferramentas dos Técnicos",
    CRIANDO_TAB_EQUIPAMETNOS_CAUSAS: "14/16: Configurando os Equipamentos e Causas",
    CRIANDO_TAB_DEFEITOS_CAUSAS: "15/16: Configurando os Defeitos e Causas",
    CRIANDO_TAB_ACOES_CAUSAS: "16/16: Configurando as Ações e Causas",
    BUSCANDO_CHAMADOS_BASE_LOCAL: "Buscando chamados armazenados do smartphone",
    ENVIANDO_CHAMADOS_FECHADOS: "Enviando chamados ao servidor",
    ENVIANDO_CHECKINS: "Enviando checkins ao servidor",
    ENVIANDO_INTENCOES: "Enviando intenções de atendimento ao servidor",
    BUSCANDO_CHAMADOS_SERVIDOR: "Buscando chamados junto ao servidor",
    COMBINANDO_CHAMADOS_SERVIDOR_SMARTPHONE: "Armazenando chamados do servidor no smartphone",
    CHAMADO_FECHADO_COM_SUCESSO: 'Chamado fechado! Aguarde a sincronização ou clique em atualizar',
    ATUALIZAR_CHAMADOS_STORAGE: "Atualizando chamados armazenados do smartphone",
    CHAMADO_EXIGE_LAUDO: 'Este chamado exige lançamento de laudo!',
    CHAMADOS_SINCRONIZADOS: 'Chamados sincronizados junto ao servidor',
    AGUARDE_ALGUNS_INSTANTES: 'Sincronizando. Por favor aguarde',
    MUITAS_REQUISICOES: 'Muitas requisições. Por favor aguarde',
    ENVIANDO_FOTOS_PARA_SERVIDOR: 'Enviando fotos ao servidor',
    SINCRONIZANDO_CHAMADOS: 'Sincronizando chamados',
    AGUARDE_SINCRONIZACAO: 'Aguarde a sincronização',
    DETALHE_ADICIONADO: 'Detalhe adicionado com sucesso',
    LOGS_ENVIADOS_COM_SUCESSO: 'Logs enviados com sucesso',
    LOGS_REMOVIDOS_COM_SUCESSO: 'Logs removidos com sucesso',
    ERRO_ENVIAR_LOGS: 'Erro ao enviar os logs',
    ERRO_REMOVER_LOGS: 'Erro ao remover os logs',
    ERRO_EMAIL_INVALIDO: 'Favor digitar um e-mail válido',
    LINK_AVALIACAO_ATENDIMENTO: 'E-mail salvo com sucesso! Enviaremos um link para que avalie o atendimento prestado',
    SAIR_APLICATIVO: `Ao sair do aplicativo você perderá todos os dados armazenados 
                      no dispositivo, como checkin, fotos ou dados dos chamados. 
                      Favor manter-se logado no app.`
  };
  public static TOAST = { 
    INFO: 'info', 
    WARNING: 'warning',
    ERROR: 'error',
    SUCCESS: 'success',
    DURACAO: 5000,
    POSITION: {
      TOP: 'top', 
      MIDDLE: 'middle'
    }
  }
  public static L = { 
    ICONES: {
      VERMELHO: 'https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
      VERDE: 'https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png'
    },
    SOMBRA: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png'
  };
}