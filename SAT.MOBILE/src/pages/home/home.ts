import { Component } from '@angular/core';
import { Geolocation } from '@ionic-native/geolocation';
import { LoadingController, NavController, PopoverController, Events, AlertController, Platform } from 'ionic-angular';

import { Market } from '@ionic-native/market';

import { LoginPage } from '../login/login';
import { ChamadosPage } from "../chamados/chamados";
import { PecasPage } from '../pecas/pecas';
import { HomeMaisOpcoesPage } from '../home/home-mais-opcoes';
import { IndicadorMenuPage } from '../indicadores/indicador-menu';
import { MensagensPage } from '../mensagens/mensagens';

import { Chamado } from "../../models/chamado";
import { DadosGlobais } from '../../models/dados-globais';
import { UsuarioPonto } from '../../models/usuario-ponto';
import { Laudo } from '../../models/laudo';

import { Config } from "../../models/config";
import moment from 'moment';

import { DadosGlobaisService } from '../../services/dados-globais';
import { UsuarioService } from '../../services/usuario';
import { ChamadoService } from "../../services/chamado";
import { AcaoService } from "../../services/acao";
import { DefeitoService } from "../../services/defeito";
import { CausaService } from "../../services/causa";
import { PecaService } from "../../services/peca";
import { TipoServicoService } from "../../services/tipo-servico";
import { MensagemTecnicoService } from '../../services/mensagem-tecnico';
import { EquipamentoPOSService } from '../../services/equipamento-pos';
import { TipoComunicacaoService } from '../../services/tipo-comunicacao';
import { MotivoComunicacaoService } from '../../services/motivo-comunicacao';
import { OperadoraTelefoniaService } from '../../services/operadora-telefonia';
import { TestePage } from '../teste/teste';
import { MotivoCancelamentoService } from '../../services/motivo-cancelamento';
import { StatusServicoService } from '../../services/status-servico';
import { DefeitoPOSService } from '../../services/defeito-pos';
import { FerramentasTecnicoPage } from '../ferramentas-tecnico/ferramentas-tecnico';
import { FerramentaTecnicoService } from '../../services/ferramenta-tecnico';
import { GeolocationService } from '../../services/geo-location';
import { EquipamentoCausaService } from '../../services/equipamento-causa';
import { DefeitoCausaService } from '../../services/defeito-causa';
import { AcaoCausaService } from '../../services/acao-causa';
import { UsuarioPage } from '../usuario/usuario';
import { AuditoriasPage } from '../auditorias/auditorias';
import { AuditoriaService } from '../../services/auditoria';
import { MensagemTecnico } from '../../models/mensagem-tecnico';
import { Auditoria } from '../../models/auditoria';
import { PontosPage } from '../ponto/pontos';
import { Localizacao } from '../../models/localizacao';


@Component({
  selector: 'home-page',
  templateUrl: 'home.html'
})
export class HomePage {
  loginPage = LoginPage;
  versaoApp = Config.VERSAO_APP;
  necessidadeRegistrarIntervalo: boolean = false;
  dg: DadosGlobais;
  chamados: Chamado[];
  mensagens: MensagemTecnico[] = [];
  qtdMensagensTecnicoNaoLidas: number;
  auditorias: Auditoria[] = [];
  qtdAuditoriasPendentes: number;
  laudos: Laudo[];
  task: any;
  perfilTecnico: boolean;
  usuarioPonto: UsuarioPonto;

  constructor(
    public platform: Platform,
    private navCtrl: NavController,
    private alertCtrl: AlertController,
    private loadingCtrl: LoadingController,
    private market: Market,
    private events: Events,
    private popoverCtrl: PopoverController,
    private dadosGlobaisService: DadosGlobaisService,
    private chamadoService: ChamadoService,
    private usuarioService: UsuarioService,
    private acaoService: AcaoService,
    private defeitoService: DefeitoService,
    private causaService: CausaService,
    private pecaService: PecaService,
    private tipoServicoService: TipoServicoService,
    private nav: NavController,
    private geolocationService: GeolocationService,
    private mensagemTecnicoService: MensagemTecnicoService,
    private auditoriaService: AuditoriaService,
    private equipamentoPOSService: EquipamentoPOSService,
    private tipoComunicacaoService: TipoComunicacaoService,
    private motivoComunicacaoService: MotivoComunicacaoService,
    private operadoraTelefonicaService: OperadoraTelefoniaService,
    private motivoCancelamentoService: MotivoCancelamentoService,
    private statusServicoService: StatusServicoService,
    private defeitoPOSService: DefeitoPOSService,
    private ferramentaTecnicoService: FerramentaTecnicoService,
    private equipamentoCausaService: EquipamentoCausaService,
    private defeitoCausaService: DefeitoCausaService,
    private acaoCausaService: AcaoCausaService,
    private geolocation: Geolocation,
  ) {
    this.events.subscribe('sincronizacao:efetuada', () => {
      setTimeout(() => { this.carregarChamadosStorage() }, 2000);
    });
  }

  ionViewWillEnter() {
    this.carregarDadosGlobais()
      .then(() => this.carregarChamadosStorage().catch(() => {}))
      .then(() => this.carregarAuditoriasUsuario().catch(() => {}))
      .then(() => this.carregarMensagensTecnico().catch(() => {}))
      .then(() => this.enviarLocalizacao())
      .catch(() => {});

    this.geolocationService.verificarSeGPSEstaAtivoEDirecionarParaConfiguracoes();
  }

  ngOnInit() {
    this.events.subscribe('sincronizacao:efetuada', () => {
      this.carregarChamadosStorage();
    });
  }

  public telaChamados() {
    if (!this.verificarNecessidadeAtualizacao()) {
      this.navCtrl.push(ChamadosPage);

      return;
    }

    const confirmacao = this.alertCtrl.create({
      title: Config.MSG.ATUALIZACAO,
      message: Config.MSG.ATUALIZAR_DADOS_LOCAIS,
      buttons: [
        {
          text: Config.MSG.CANCELAR,
          handler: () => {}
        },
        {
          text: Config.MSG.ATUALIZAR,
          handler: () => {
            this.atualizarBDLocal();

            this.carregarChamadosStorage();
          }
        }
      ]
    });

    confirmacao.present();
  }

  public telaPecas() {
    this.navCtrl.push(PecasPage);
  }

  public telaPontos() {
    this.navCtrl.push(PontosPage);
  }

  public telaIndicadoresMenu() {
    this.navCtrl.push(IndicadorMenuPage);
  }

  public telaMensagensTecnico() {
    this.navCtrl.push(MensagensPage, { mensagens: this.mensagens });
  }

  public telaFerramentasTecnico() {
    this.navCtrl.push(FerramentasTecnicoPage);
  }

  public telaAuditorias() {
    this.navCtrl.push(AuditoriasPage, { auditorias: this.auditorias });
  }

  public telaTeste() {
    this.navCtrl.push(TestePage);
  }

  public abrirPopover(event: MouseEvent) {
    const popover = this.popoverCtrl.create(HomeMaisOpcoesPage);

    popover.present({ev: event});

    popover.onDidDismiss(data => {
      if (!data)
        return;
    });
  }

  public carregarChamadosStorage(): Promise<Chamado[]> {
    return new Promise((resolve, reject) => {
      this.chamadoService.buscarChamadosStorage().then((chamados: Chamado[]) => {
        this.chamados = chamados.filter((c) => {
          return (!c.dataHoraFechamento);
        });

        resolve(chamados);
      })  
      .catch(err => {
        reject();
      })
    });
  }

  public carregarMensagensTecnico(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.mensagemTecnicoService.buscarMensagensTecnicoApi(this.dg.usuario.codUsuario).subscribe(mensagens => {
        this.qtdMensagensTecnicoNaoLidas = mensagens.filter((msg) => { return (!msg.indLeitura) }).length;
        this.mensagens = mensagens;
        resolve();
      }, err => {});
    });
  }

  public carregarAuditoriasUsuario(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.auditoriaService.buscarAuditoriasPorUsuario(this.dg.usuario.codUsuario)
        .subscribe(auditorias => {
          this.qtdAuditoriasPendentes = auditorias.filter((aud) => { return (aud.auditoriaStatus.codAuditoriaStatus === 1) }).length;
          this.auditorias = auditorias;
          resolve();
        }, err => {});
    });
  }

  private carregarDadosGlobais(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.dadosGlobaisService.buscarDadosGlobaisStorage().then((dados) => {
        this.dg = dados;
        
        if (!dados.usuario.foto) {
          const confirm = this.alertCtrl.create({
            title: 'Atenção',
            message: `Percebemos que seus dados não estão atualizados! você será redirecionado para que possa
                      inserir seus dados pessoais e foto.`,
            buttons: [
              {
                text: 'Ok',
                handler: () => {
                  this.navCtrl.push(UsuarioPage);
                }
              }
            ]
          });
          confirm.present();
        }
        
        if (!this.dg.usuario.codTecnico) {
          reject();
          return;
        }
        
        resolve(true);
      })
      .catch((err) => { reject(false) });
    });
  }

  private verificarNecessidadeAtualizacao(): boolean {
    var dataAtualizacaoNecessaria = moment().add(Config.INT_SINC_BD_LOCAL_DIAS, "days");;
    var dataAtualizacao = moment(this.dg.dataHoraCadastro);
    
    if (dataAtualizacao.diff(dataAtualizacaoNecessaria, 'days') >= 0 || !this.dg.dataHoraCadastro) return true;

    this.tipoServicoService.buscarTipoServicosStorage().then((tiposServicos) => {
      this.acaoService.buscarAcoesStorage().then((acoes) => {
        this.defeitoService.buscarDefeitosStorage().then((defeitos) => {
          this.causaService.buscarCausasStorage().then((causas) => {
            this.pecaService.buscarPecasStorage().then((pecas) => {
              this.equipamentoPOSService.buscarEquipamentosPOSStorage().then((equipamentos) => {
                this.operadoraTelefonicaService.buscarOperadorasStorage().then((operadoras) => {
                  this.tipoComunicacaoService.buscarTiposComunicacaoStorage().then((tiposComunicacao) => {
                    this.motivoComunicacaoService.buscarMotivosComunicacaoStorage().then((motivosComuni) => {
                      this.motivoCancelamentoService.buscarMotivosCancelamentoStorage().then((motivosCancel) => {
                        this.statusServicoService.buscarStatusServicosStorage().then((statusServicos) => {
                          this.defeitoPOSService.buscarDefeitosPOSStorage().then((defeitosPOS) => {
                            this.ferramentaTecnicoService.buscarFerramentasTecnicoStorage().then((ferramentas) => {
                              this.equipamentoCausaService.buscarEquipamentosCausasStorage().then((equipCausas) => {
                                this.defeitoCausaService.buscarDefeitosCausasStorage().then((defeitosCausas) => {
                                  this.acaoCausaService.buscarAcoesCausasStorage().then((acoesCausas) => {
                                    if (
                                      !tiposServicos.length || !acoes.length || !defeitos.length || !causas.length || 
                                      !pecas.length || !equipamentos.length || !operadoras.length || !tiposComunicacao.length ||
                                      !motivosComuni.length || !motivosCancel.length || !statusServicos.length || !defeitosPOS.length ||
                                      !ferramentas.length || !equipCausas.length || !defeitosCausas.length || !acoesCausas.length
                                    ) {
                                      return true;
                                    }
                                  });
                                });
                              });
                            });
                          });
                        });
                      });
                    });
                  });
                });
              });
            });
          });
        });
      });
    });

    return false;
  }

  private enviarLocalizacao() {
    this.platform.ready().then(() => {
      this.geolocation.getCurrentPosition(Config.POS_CONFIG).then((location) => {
        let loc = new Localizacao();
        loc.latitude = location.coords.latitude;
        loc.longitude = location.coords.longitude;
        loc.codUsuario = this.dg.usuario.codUsuario;
        loc.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');

        this.geolocationService.enviarLocalizacao(loc).subscribe(() => {}, e => {});
      }).catch();
    }).catch();
  }

  public atualizarBDLocal() {
    let loading = this.loadingCtrl.create({ content: 'Aguarde...' });
    loading.present();
    
    loading.setContent(Config.MSG.CRIANDO_TAB_TIPOS_SERVICOS);
    this.tipoServicoService.buscarTipoServicosApi().subscribe(() => { 
      loading.setContent(Config.MSG.CRIANDO_TAB_ACOES);
      this.acaoService.buscarAcoesApi().subscribe(() => { 
        loading.setContent(Config.MSG.CRIANDO_TAB_DEFEITOS);
        this.defeitoService.buscarDefeitosApi().subscribe(() => { 
          loading.setContent(Config.MSG.CRIANDO_TAB_CAUSAS);
          this.causaService.buscarCausasApi().subscribe(() => { 
            loading.setContent(Config.MSG.CRIANDO_TAB_PECAS);
            this.pecaService.buscarPecasApi().subscribe(() => {
              loading.setContent(Config.MSG.CRIANDO_TAB_EQUIPAMENTOS_POS);
              this.equipamentoPOSService.buscarEquipamentosPOSApi().subscribe(() => {
                loading.setContent(Config.MSG.CRIANDO_TAB_OPERADORAS);
                this.operadoraTelefonicaService.buscarOperadorasApi().subscribe(() => {
                  loading.setContent(Config.MSG.CRIANDO_TAB_TIPOS_COMUNICACAO);
                  this.tipoComunicacaoService.buscarTiposComunicacaoApi().subscribe(() => {
                    loading.setContent(Config.MSG.CRIANDO_TAB_MOTIVOS_COMUNICACAO);
                    this.motivoComunicacaoService.buscarMotivosComunicacaoPOSApi().subscribe(() => {
                      loading.setContent(Config.MSG.CRIANDO_TAB_MOTIVOS_CANCELAMENTO);
                      this.motivoCancelamentoService.buscarMotivosCancelamentoPOSApi().subscribe(() => {
                        loading.setContent(Config.MSG.CRIANDO_TAB_STATUS_SERVICO);
                        this.statusServicoService.buscarStatusServicosApi().subscribe(() => {
                          loading.setContent(Config.MSG.CRIANDO_TAB_DEFEITOS_POS);
                          this.defeitoPOSService.buscarDefeitosPOSApi().subscribe(() => {
                            loading.setContent(Config.MSG.CRIANDO_TAB_FERRAMENTAS);
                            this.ferramentaTecnicoService.buscarFerramentasTecnicoStorage().then((ferramentas) => {
                              this.ferramentaTecnicoService.buscarFerramentasTecnicoApi(this.dg.usuario.codUsuario, ferramentas).subscribe(() => {
                                 loading.setContent(Config.MSG.CRIANDO_TAB_EQUIPAMETNOS_CAUSAS);
                                 this.equipamentoCausaService.buscarEquipamentosCausasApi().subscribe(() => {
                                   loading.setContent(Config.MSG.CRIANDO_TAB_DEFEITOS_CAUSAS);
                                   this.defeitoCausaService.buscarDefeitosCausasApi().subscribe(() => {
                                     loading.setContent(Config.MSG.CRIANDO_TAB_ACOES_CAUSAS);
                                     this.acaoCausaService.buscarAcoesCausasApi().subscribe(() => {
                                      loading.dismiss();
  
                                      this.salvarDadosGlobais();
                                     }, (e) => { 
                                       loading.dismiss(); 
                                       this.exibirAlerta(Config.MSG.ERRO_OBTER_ACOES_CAUSAS);
                                     });
                                   }, (e) => { 
                                     loading.dismiss(); 
                                    this.exibirAlerta(Config.MSG.ERRO_OBTER_DEFEITOS_CAUSAS);
                                  });
                                }, (e) => { 
                                  loading.dismiss(); 
                                  this.exibirAlerta(Config.MSG.ERRO_OBTER_EQUIPAMENTOS_CAUSAS);
                                });
                              }, (e) => { 
                                loading.dismiss(); 
                                this.exibirAlerta(Config.MSG.ERRO_OBTER_FERRAMENTAS); 
                              });
                            }, (e) => { 
                              loading.dismiss(); 
                              this.exibirAlerta(Config.MSG.ERRO_OBTER_FERRAMENTAS); 
                            });
                          }, (e) => { 
                            loading.dismiss(); 
                            this.exibirAlerta(Config.MSG.ERRO_OBTER_DEFEITOS_POS); 
                          });
                        }, (e) => { 
                          loading.dismiss(); 
                          this.exibirAlerta(Config.MSG.ERRO_OBTER_STATUS_SERVICOS); 
                        });
                      }, (e) => { 
                        loading.dismiss(); 
                        this.exibirAlerta(Config.MSG.ERRO_OBTER_MOTIVOS_CANCELAMENTO); 
                      });
                    }, (e) => { 
                      loading.dismiss(); 
                      this.exibirAlerta(Config.MSG.ERRO_OBTER_MOTIVOS_COMUNICACAO); 
                    });
                  }, (e) => { 
                    loading.dismiss(); 
                    this.exibirAlerta(Config.MSG.ERRO_OBTER_TIPOS_COMUNICACAO); 
                  });
                }, (e) => { 
                  loading.dismiss(); 
                  this.exibirAlerta(Config.MSG.ERRO_OBTER_OPERADORAS); 
                });
              }, (e) => { 
                loading.dismiss(); 
                this.exibirAlerta(Config.MSG.ERRO_OBTER_EQUIPAMENTOS_POS); 
              });
            }, (e) => { 
              loading.dismiss(); 
              this.exibirAlerta(Config.MSG.ERRO_OBTER_PECAS); 
            });
          }, (e) => { 
            loading.dismiss(); 
            this.exibirAlerta(Config.MSG.ERRO_OBTER_CAUSAS); 
          });
        }, (e) => { 
          loading.dismiss(); 
          this.exibirAlerta(Config.MSG.ERRO_OBTER_DEFEITOS); 
        });
      }, (e) => { 
        loading.dismiss(); 
        this.exibirAlerta(Config.MSG.ERRO_OBTER_ACOES); 
      });
    }, (e) => { 
      loading.dismiss(); 
      this.exibirAlerta(Config.MSG.ERRO_OBTER_TIPOS_SERVICO); 
    });
  }

  private salvarDadosGlobais() {
    this.dg.dataHoraCadastro = moment().toDate();

    this.dadosGlobaisService.insereDadosGlobaisStorage(this.dg);
  }

  public abrirAplicativoNaLojaGoogle() {
    this.market.open(Config.GOOGLE_PLAY_NOME_APP);
  }

  private verificarNecessidadeRegistroPontoIntervalo(): Promise<any> {
    return new Promise((resolve, reject) => {
      let diff = moment.duration(moment().diff(moment(this.usuarioPonto.registros[0]))).asMinutes();

      if (this.usuarioPonto.registros.length == 1 && diff > 270) {
        this.necessidadeRegistrarIntervalo = true;
      }

      resolve();
    });
  }

  private obterRegistrosPontoDoDia(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.usuarioService.buscarRegistrosPonto(
        this.dg.usuario.codUsuario)
        .subscribe(res => {
          this.usuarioPonto = res;
          console.log(res);
          

          resolve(this.usuarioPonto);
        },
        err => {
          reject();
        });
    });
  }

  private exibirAlerta(msg: string) {
    const alerta = this.alertCtrl.create({
      title: Config.MSG.ALERTA,
      subTitle: msg,
      buttons: ['OK']
    });

    alerta.present();
  }

  public sair() {
    this.dadosGlobaisService.apagarDadosGlobaisStorage().then(() => {
      this.nav.setRoot(this.loginPage);
    }).catch((err) => {});
  }
}