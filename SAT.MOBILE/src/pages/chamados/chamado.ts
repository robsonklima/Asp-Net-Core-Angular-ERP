import { Component, ViewChild } from '@angular/core';
import { NavParams, Platform, Slides, AlertController, Toast, ModalController, NavController, ToastController } from 'ionic-angular';
import { NgForm } from '@angular/forms';
import { Geolocation } from '@ionic-native/geolocation';
import { Camera } from '@ionic-native/camera';
import { AndroidPermissions } from '@ionic-native/android-permissions';
import { AppAvailability } from '@ionic-native/app-availability';
import { Diagnostic } from '@ionic-native/diagnostic';
import { Market } from '@ionic-native/market';
import moment from 'moment';
import _ from 'lodash';
import { Config } from '../../models/config';
import { DadosGlobais } from '../../models/dados-globais';
import { Chamado } from "../../models/chamado";
import { Rat } from "../../models/rat";
import { UsuarioPonto } from '../../models/usuario-ponto';
import { Foto } from '../../models/foto';
import { RatDetalhe } from '../../models/rat-detalhe';
import { EquipamentoPOS } from '../../models/equipamentoPOS';
import { DadosGlobaisService } from '../../services/dados-globais';
import { EquipamentoPOSService } from '../../services/equipamento-pos';
import { ChamadoService } from './../../services/chamado';
import { UsuarioService } from '../../services/usuario';
import { GeolocationService } from '../../services/geo-location';
import { LoadingFactory } from '../../factories/loading-factory';
import { RatDetalhePage } from "../rat-detalhe/rat-detalhe";
import { RatDetalhePecaPage } from "../rat-detalhe-peca/rat-detalhe-peca";
import { HistoricoListaPage } from '../historico/historico-lista';
import { FotosPage } from '../fotos/fotos';
import { LaudoPage } from '../laudos/laudo';
import { FotoPage } from '../fotos/foto';
import { ChamadoConfPage } from './chamado-conf';
import { FotoService } from '../../services/foto';
import { AssinaturaPage } from '../assinatura/assinatura';
import { ChecklistPreventivaPage } from '../checklists/checklist-preventiva';

@Component({
  selector: 'chamado-page',
  templateUrl: 'chamado.html'
})
export class ChamadoPage {
  toast: Toast;
  qtdMaximaFotos: number = Config.QTD_MAX_FOTOS_POR_ATENDIMENTO;
  distanciaCercaEletronica: number = 0;
  dataAtual: string = moment().format('YYYY-MM-DD');
  horaAtual: string = moment().format('HH:mm:ss');
  equipamentosPOS: EquipamentoPOS[] = [];
  @ViewChild(Slides) slides: Slides;
  usuarioPonto: UsuarioPonto;
  indexSlide: number;
  tituloSlide: string;
  dg: DadosGlobais;
  chamado: Chamado;
  foto: Foto;
  config: any;

  constructor(
    private platform: Platform,
    private toastCtrl: ToastController,
    private appAvailability: AppAvailability,
    private geolocation: Geolocation,
    private diagnostic: Diagnostic,
    private androidPerm: AndroidPermissions,
    private market: Market,
    private camera: Camera,
    private modalCtrl: ModalController,
    private navParams: NavParams,
    private loadingFactory: LoadingFactory,
    private alertCtrl: AlertController,
    private navCtrl: NavController,
    private geolocationService: GeolocationService,
    private equipamentoPOSService: EquipamentoPOSService,
    private dadosGlobaisService: DadosGlobaisService,
    private chamadoService: ChamadoService,
    private usuarioService: UsuarioService,
    private fotoService: FotoService
  ) {
    this.chamado = this.navParams.get('chamado');
  }

  ionViewWillEnter() {
    this.configurarSlide(this.slides.getActiveIndex());
    this.criarRat();

    this.carregarDadosGlobais()
      .then(() => this.buscarEquipamentosPOS())
      .then(() => this.obterRegistrosPonto())
      .then(() => {
        this.registrarLeituraOs();
      })
      .catch(() => {});

    this.geolocationService.verificarSeGPSEstaAtivoEDirecionarParaConfiguracoes();
  }

  public alterarSlide() {
    this.configurarSlide(this.slides.getActiveIndex());
  }

  public prosseguirSlide() {
    this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
    this.configurarSlide(this.slides.getActiveIndex());
  }

  private criarRat() {
    if (this.chamado.rats.length == 0) {
      let rat = new Rat();
      rat.fotos = [];
      rat.ratDetalhes = [];
      rat.laudos = [];
      this.chamado.rats.push(rat);
      this.chamadoService.atualizarChamado(this.chamado).catch();
    }
  }

  public telaRatDetalhe() {
    const modal = this.modalCtrl.create(RatDetalhePage, { chamado: this.chamado });
    modal.present();
    modal.onDidDismiss(() => {
      setTimeout(() => {
        this.configurarSlide(this.slides.getActiveIndex());  
      }, 500);
    });
  }

  public telaRatDetalhePeca(chamado: Chamado, i: number) {
    const modal = this.modalCtrl.create(RatDetalhePecaPage, { chamado: this.chamado, i: i });
    modal.present();
    modal.onDidDismiss(
      () => {}
    );
  }

  public telaLaudo(chamado: Chamado) {
    const modal = this.modalCtrl.create(LaudoPage, { chamado: this.chamado });
    modal.present();
    modal.onDidDismiss(() => {
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public telaChecklistPreventiva(chamado: Chamado) {
    const modal = this.modalCtrl.create(ChecklistPreventivaPage, { chamado: this.chamado });
    modal.present();
    modal.onDidDismiss((chamado: Chamado) => {
      this.configurarSlide(this.slides.getActiveIndex());
      this.chamado = chamado;
    });
  }

  public telaFoto(modalidade: string) {
    if (!this.verificarExistenciaFoto(modalidade)) return;

    const modal = this.modalCtrl.create(FotoPage, { foto: this.carregarFoto(modalidade) });
    modal.present();
    modal.onDidDismiss(() => {
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public telaFotos() {
    const modal = this.modalCtrl.create(FotosPage, { chamado: this.chamado, modalidade: 'FOTO' });
    modal.present();
    modal.onDidDismiss(() => {
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public telaChamadoConf() {
    const modal = this.modalCtrl.create(ChamadoConfPage, { chamado: this.chamado });
    modal.present();
    modal.onDidDismiss(() => {
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public tirarFoto(modalidade: string) {
    this.platform.ready().then(() => {
      let width: number = Config.FOTO.WIDTH;
      let height: number = Config.FOTO.HEIGHT;
      let quality: number = Config.FOTO.QUALITY;

      if (
        modalidade == Config.FOTO.MODALIDADES.RAT ||
        modalidade == Config.FOTO.MODALIDADES.TERMO_ACEITE ||
        modalidade == Config.FOTO.MODALIDADES.TERMO_ENTREGA ||
        modalidade == Config.FOTO.MODALIDADES.TERMO_RECOLHIMENTO ||
        modalidade == Config.FOTO.MODALIDADES.NOTA_FISCAL
      ) {
        width = 720;
        height = 1280;
        quality = 90;
      }

      this.diagnostic.requestRuntimePermissions([ this.diagnostic.permission.WRITE_EXTERNAL_STORAGE, this.diagnostic.permission.CAMERA ]).then(() => {
        this.androidPerm.requestPermissions([ this.androidPerm.PERMISSION.WRITE_EXTERNAL_STORAGE, this.androidPerm.PERMISSION.CAMERA ]).then(() => {
          this.camera.getPicture({
            quality: quality,
            targetWidth: width,
            targetHeight: height,
            destinationType: this.camera.DestinationType.DATA_URL,
            encodingType: this.camera.EncodingType.JPEG,
            mediaType: this.camera.MediaType.PICTURE,
            allowEdit: false,
            sourceType: 1,
          }).then(imageData => {
            this.foto = new Foto();
            this.foto.codOS = this.chamado.codOs;
            this.foto.nome = moment().format('YYYYMMDDHHmmss') + "_" + this.chamado.codOs.toString() + '_' + modalidade;
            this.foto.str = 'data:image/jpeg;base64,' + imageData;
            this.foto.modalidade = modalidade;
            this.chamado.rats[0].fotos.push(this.foto);
            this.chamadoService.atualizarChamado(this.chamado).catch();
            this.camera.cleanup().catch();
          }).catch((e) => { this.exibirAlerta(e) });
        }).catch((e) => { this.exibirAlerta(e) });
      }).catch((e) => { this.exibirAlerta(e) });

    }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_RESPOSTA_DISPOSITIVO) });
  }

  public carregarFoto(modalidade: string): string {
    if (this.chamado.rats.length > 0) {

      let fotos = this.chamado.rats[0].fotos.filter((foto) => {
        return (foto.modalidade == modalidade);
      });

      if (fotos.length > 0) {
        return fotos[0].str;
      }
    }

    return 'assets/imgs/no-photo.png';
  }

  public removerFoto(modalidade: string) {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja excluir esta foto?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Excluir',
          handler: () => {
            if (this.chamado.rats.length > 0) {
              this.chamado.rats[0].fotos = this.chamado.rats[0].fotos.filter((f) => {
                return (f.modalidade != modalidade);
              });

              this.chamadoService.atualizarChamado(this.chamado);
            }
          }
        }
      ]
    });

    confirmacao.present();
  }

  public enviarFoto(modalidade: string) {
    const confirmacao = this.alertCtrl.create({
      title: 'Enviar para o Servidor',
      message: 'Deseja enviar a foto para o servidor agora?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Enviar',
          handler: () => {
            if (this.chamado.rats.length > 0) {
              let fotos = this.chamado.rats[0].fotos.filter((foto) => {
                return (foto.modalidade == modalidade);
              });
        
              if (fotos.length == 0) return;

              this.loadingFactory.exibir('Enviado foto ao servidor');

              this.fotoService.enviarFotoApi(fotos[0]).subscribe(() => {
                this.chamado.rats[0].fotos.forEach((foto, i) => {
                  if (foto.modalidade == modalidade) {
                    this.chamado.rats[0].fotos[i].status = Config.FOTO.STATUS.ENVIADA;
                  }
                });

                this.exibirToast('Foto enviada com sucesso', Config.TOAST.SUCCESS);
                this.loadingFactory.encerrar();
              }, e => {
                this.exibirToast('Não foi possível enviar ao foto ao servidor', Config.TOAST.ERROR);

                this.loadingFactory.encerrar();
              });
            }
          }
        }
      ]
    });

    confirmacao.present();
  }

  public verificarExistenciaFoto(modalidade: string): boolean {
    if (typeof(this.chamado.rats) !== 'undefined') {
      if (this.chamado.rats.length > 0) {

        let fotos = this.chamado.rats[0].fotos.filter((foto) => {
          return (foto.modalidade == modalidade);
        });

        if (fotos.length > 0) {
          return true;
        }
      }
    }

    return false;
  }

  public verificarStatusFoto(modalidade: string): string {
    if (this.chamado.rats.length > 0) {
      this.chamado.rats[0].fotos.forEach((foto, i) => {
        if (foto.modalidade === modalidade && foto.status !== Config.FOTO.STATUS.ENVIADA) {
          return Config.FOTO.STATUS.ENVIADA;
        }
      });

      return Config.FOTO.STATUS.PENDENTE_ENVIO;
    }
  }

  public verificarExistenciaLaudo(): boolean {
    if (typeof(this.chamado.rats) !== 'undefined') {
      if (this.chamado.rats.length > 0) {
        if (typeof(this.chamado.rats[0].laudos) !== 'undefined') {
          if (this.chamado.rats[0].laudos.length > 0) {
              return true;
          }
        }
      }
    }

    return false;
  }

  public removerLaudo() {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja remover este laudo?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Excluir',
          handler: () => {
            this.chamado.rats[0].laudos = [];
            this.chamadoService.atualizarChamado(this.chamado);
          }
        }
      ]
    });

    confirmacao.present();
  }

  public informarIntencaoAtendimento() {
    if (this.chamadoService.verificarExisteCheckinEmOutroChamado()) {
      this.exibirToast(Config.MSG.CHECKIN_EM_ABERTO, Config.TOAST.ERROR)
      return
    }

    const alerta = this.alertCtrl.create({
      title: Config.MSG.DESLOCAMENTO,
      message: Config.MSG.INTENCAO_CONFIRMACAO,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.chamadoService.buscarChamadosStorage().then((chamados) => {
              chamados.forEach((c, i) => {
                chamados[i].indIntencaoAtendimento = false;
                chamados[i].dataHoraIntencaoAtendimento = null;
              });

              this.chamadoService.atualizarChamadosStorage(chamados);

              this.platform.ready().then(() => {
                this.loadingFactory.exibir(Config.MSG.OBTENDO_LOCALIZACAO);
                
                this.geolocation.getCurrentPosition(Config.POS_CONFIG).then((c) => {
                  this.loadingFactory.encerrar();
  
                  this.chamado.indIntencaoAtendimento = true;
                  this.chamado.dataHoraIntencaoAtendimento = new Date().toLocaleString('pt-BR');
                  this.geolocationService.atualizarMinhaLocalizacao({ 
                    latitude: c.coords.latitude, longitude: c.coords.longitude,
                    dataHoraCad: new Date().toLocaleString('pt-BR'),
                    codUsuario: this.dg.usuario.codUsuario
                  });
  
                  this.chamadoService.atualizarChamado(this.chamado).then(() => {
                    this.exibirToast(`Em deslocamento para o chamado ${this.chamado.codOs}`, Config.TOAST.SUCCESS);
                  }).catch(() => { this.loadingFactory.encerrar() });
                }).catch(() => { this.loadingFactory.encerrar() });
              }).catch(() => {});
            });
          }
        }
      ]
    });

    alerta.present();
  }

  public cancelarIntencaoAtendimento() {
    if (this.chamadoService.verificarExisteCheckinEmOutroChamado()) {
      this.exibirToast(Config.MSG.CHECKIN_EM_ABERTO, Config.TOAST.ERROR)
      return
    }

    const alerta = this.alertCtrl.create({
      title: Config.MSG.CONFIRMACAO,
      message: Config.MSG.INTENCAO_REMOCAO,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Sim',
          handler: () => {
            this.chamado.indIntencaoAtendimento = false;
            this.chamado.dataHoraIntencaoAtendimento = null;

            this.chamadoService.atualizarChamado(this.chamado).then(() => {
              this.exibirToast(Config.MSG.INTENCAO_CANCELADA, Config.TOAST.INFO)
              this.configurarSlide(this.slides.getActiveIndex());
              this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
            }).catch(() => { this.loadingFactory.encerrar() });
          }
        }
      ]
    });

    alerta.present();
  }

  public removerCheckin() {
    if (this.chamado.checkout.dataHoraCadastro) {
      this.exibirToast(Config.MSG.CHECKOUT_EM_ABERTO, Config.TOAST.ERROR)
      return
    }

    const alerta = this.alertCtrl.create({
      title: Config.MSG.CONFIRMACAO,
      message: Config.MSG.CHECKIN_REMOCAO,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.chamado.checkin.localizacao.latitude = null;
            this.chamado.checkin.localizacao.longitude = null;
            this.chamado.checkin.dataHoraCadastro = null;

            this.chamadoService.atualizarChamado(this.chamado).then(() => {
              this.exibirToast(`Checkin removido`, Config.TOAST.INFO);
              this.configurarSlide(this.slides.getActiveIndex());
              this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
            }).catch(() => { this.loadingFactory.encerrar() });
          }
        }
      ]
    });

    alerta.present();
  }

  private verificarLaudoObrigatorio(): boolean {
    if (
      (
        this.chamado.cliente.codCliente == Config.CLIENTE.METRO_RIO   ||
        this.chamado.cliente.codCliente == Config.CLIENTE.RIO_CARD    ||
        this.chamado.cliente.codCliente == Config.CLIENTE.VLT_CARIOCA ||
        this.chamado.cliente.codCliente == Config.CLIENTE.BRINKS      ||
        this.chamado.cliente.codCliente == Config.CLIENTE.BVA_BRINKS  ||
        this.chamado.cliente.codCliente == Config.CLIENTE.CEF         ||
        this.chamado.cliente.codCliente == Config.CLIENTE.BNB         ||
        this.chamado.cliente.codCliente == Config.CLIENTE.PROTEGE     ||
        this.chamado.cliente.codCliente == Config.CLIENTE.BANRISUL    ||
        (
          this.chamado.cliente.codCliente == Config.CLIENTE.BB &&
          this.chamado.equipamentoContrato.indGarantia == 1
        )
      ) && this.verificarChamadoOrcamento()
    ) {
      return true;
    }

    if (this.chamado.numReincidencias >= 5) {
      return true;
    }

    if (this.verificarChamadoVandalismo()) {
      return true;
    }

    if (this.chamado.indOSIntervencaoEquipamento) {
      return true;
    }

    return false;
  }

  private verificarChamadoVandalismo(): boolean {
    if (typeof(this.chamado.rats) !== 'undefined') {
      if (this.chamado.rats.length > 0) {
        this.chamado.rats.forEach((rat: Rat) => {
          rat.ratDetalhes.forEach((ratDetalhe: RatDetalhe) => {
            if (ratDetalhe.causa.codCausa == 526) {
              return true;
            }
          });
        });
      }
    }

    return false;
  }

  public buscarEquipamentosPOS(): Promise<EquipamentoPOS[]> {
    return new Promise((resolve, reject) => {
      this.equipamentoPOSService.buscarEquipamentosPOSStorage().then((equips: EquipamentoPOS[]) => {
        this.equipamentosPOS = equips;
        
        resolve(equips);
      }).catch(err => {
        reject(err);
      });
    });
  }

  private verificarChamadoOrcamento(): boolean {
    if (typeof(this.chamado.rats) !== 'undefined') {
      if (this.chamado.rats.length > 0) {
        this.chamado.rats.forEach((rat: Rat) => {
          rat.ratDetalhes.forEach((ratDetalhe: RatDetalhe) => {
            if (ratDetalhe.acao.codAcao == 17) {
              return true;
            }
          });
        });
      }
    }

    return false;
  }

  public verificarChamadoExtraMaquina(): boolean {
    let retorno: boolean = false;

    if (this.chamado.rats.length > 0) {
      if (this.chamado.rats[0].ratDetalhes.length > 0) {
        this.chamado.rats[0].ratDetalhes.forEach((d) => {
          if(Number(d.tipoCausa.codTipoCausa) === Number(Config.TIPO_CAUSA.EXTRA_MAQUINA)) {
            retorno = true;
          }
        });
      }
    }

    return retorno;
  }

  public telaDocumentos() {
    const modal = this.modalCtrl.create(FotosPage, { chamado: this.chamado, modalidade: 'DOCUMENTO' });
    modal.present();
    modal.onDidDismiss(() => {
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public telaAssinaturaTecnico() {
    const modal = this.modalCtrl.create(AssinaturaPage, { paginaOrigem: "RAT_TECNICO", chamado: this.chamado });
    modal.present();
    modal.onDidDismiss((chamado) => {
      this.chamado = chamado;
      this.chamadoService.atualizarChamado(this.chamado);
      
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public telaAssinaturaCliente() {
    const modal = this.modalCtrl.create(AssinaturaPage, { paginaOrigem: "RAT_CLIENTE", chamado: this.chamado });
    modal.present();
    modal.onDidDismiss((chamado) => {
      this.chamado = chamado;
      this.chamadoService.atualizarChamado(this.chamado);
      
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public telaEquipamentosHistorico(chamado: Chamado) {
    const modal = this.modalCtrl.create(HistoricoListaPage, { chamado: this.chamado });
    modal.present();
    modal.onDidDismiss(() => {});
  }

  public efetuarCheckin() {
    const alerta = this.alertCtrl.create({
      title: Config.MSG.CHECKIN,
      message: Config.MSG.CHECKIN_CONFIRMACAO,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Confirmar',
          handler: () => {
            if (this.chamadoService.verificarExisteCheckinEmOutroChamado()) {
              this.exibirToast(Config.MSG.CHECKIN_EM_ABERTO, Config.TOAST.ERROR);
              return
            }

            if (!this.chamado.indIntencaoAtendimento) {
              this.exibirToast(`Favor informar que está a caminho do chamado ${this.chamado.codOs} antes de efetuar o checkin`, Config.TOAST.ERROR);
              return
            }

            this.chamadoService.buscarStatusExecucao().then(executando => {
              this.platform.ready().then(() => {
                this.loadingFactory.exibir(Config.MSG.OBTENDO_LOCALIZACAO);
                
                this.geolocation.getCurrentPosition(Config.POS_CONFIG).then((location) => {
                  this.loadingFactory.encerrar();
                  if (this.chamado.indOSIntervencaoEquipamento) {
                    this.exibirAlerta(Config.MSG.CHAMADO_EXIGE_LAUDO);
                  }
  
                  this.chamado.checkin.dataHoraCadastro = new Date().toLocaleString('pt-BR');
                  this.chamado.checkin.localizacao.latitude = location.coords.latitude;
                  this.chamado.checkin.localizacao.longitude = location.coords.longitude;
                  this.chamado.checkin.codUsuario = this.dg.usuario.codUsuario;
                  this.chamado.checkin.codOS = this.chamado.codOs;
  
                  this.chamadoService.atualizarChamado(this.chamado).then(() => {
                    this.configurarSlide(this.slides.getActiveIndex());
                    this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
                  }).catch(() => { this.loadingFactory.encerrar() });
                }).catch(() => { this.loadingFactory.encerrar() });
              }).catch(() => {});
            });
          }
        }
      ]
    });

    alerta.present();
  }

  public efetuarCheckout() {
    this.chamado.checkout.tentativas.push(moment().format());
    this.chamadoService.atualizarChamado(this.chamado);

    const alerta = this.alertCtrl.create({
      title: Config.MSG.CHECKOUT,
      message: Config.MSG.CHECKOUT_CONFIRMACAO,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Confirmar',
          handler: () => {
            if (!this.validarCamposObrigatorios()) return;

            this.chamadoService.buscarStatusExecucao().then(executando => {
              if (executando) {
                this.exibirToast(Config.MSG.AGUARDE_ALGUNS_INSTANTES, Config.TOAST.WARNING);
                return;
              }

              this.loadingFactory.exibir(Config.MSG.OBTENDO_LOCALIZACAO);

              this.platform.ready().then(() => {
                this.geolocation.getCurrentPosition(Config.POS_CONFIG).then((location) => {

                  this.chamado.checkout.dataHoraCadastro = new Date().toLocaleString('pt-BR');
                  this.chamado.checkout.localizacao.latitude = location.coords.latitude;
                  this.chamado.checkout.localizacao.longitude = location.coords.longitude;
                  this.chamado.checkout.codUsuario = this.dg.usuario.codUsuario;
                  this.chamado.checkout.codOS = this.chamado.codOs;

                  this.loadingFactory.encerrar();
                  this.chamadoService.atualizarChamado(this.chamado).then(() => {
                    this.configurarSlide(this.slides.getActiveIndex());
                    this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
                  }).catch(() => { this.loadingFactory.encerrar() });
                }).catch(() => { this.loadingFactory.encerrar() });
              }).catch(() => {});
            });
          }
        }
      ]
    });

    alerta.present();
  }

  public salvarRat(form: NgForm) {
    let rat = new Rat();
    rat.numRat = form.value.numRat || 'AUTOMATICO';
    rat.dataInicio = form.value.dataInicio;
    rat.horaInicio = form.value.horaInicio;
    rat.dataSolucao = form.value.dataInicio;
    rat.horaSolucao = form.value.horaSolucao;
    rat.nomeAcompanhante = form.value.nomeAcompanhante;
    rat.codUsuarioCad = this.dg.usuario.codUsuario;
    rat.obsRAT = form.value.obsRAT;
    rat.statusServico = form.value.statusServico;
    rat.ratDetalhes = [];
    rat.fotos = [];

    if (this.usuarioPonto) {
      rat.horarioInicioIntervalo = this.usuarioPonto.registros[1];
      rat.horarioTerminoIntervalo = this.usuarioPonto.registros[2];
    }

    if (moment(rat.dataInicio + ' ' +  rat.horaInicio, 'YYYY-MM-DD HH:mm').isBefore(moment(this.chamado.dataHoraAgendamento, 'YYYY-MM-DD HH:mm'))) {
      this.exibirToast('A horário de atendimento deve ocorrer depois do horário de agendamento do chamado', Config.TOAST.ERROR);
      return
    }

    if (moment(rat.dataInicio + ' ' +  rat.horaInicio, 'YYYY-MM-DD HH:mm').isBefore(moment(this.chamado.dataHoraAberturaOS, 'YYYY-MM-DD HH:mm'))) {
      this.exibirToast('A horário de atendimento deve ocorrer depois da data de abertura do chamado', Config.TOAST.ERROR);
      return
    }

    if (!this.verificarSeEquipamentoEPOS()) {
      if (moment.duration(moment(rat.dataInicio + ' ' +  rat.horaSolucao, 'YYYY-MM-DD HH:mm').diff(moment(rat.dataInicio + ' ' +  rat.horaInicio, 'YYYY-MM-DD HH:mm'))).asMinutes() < 20) {
        this.exibirToast('O período mínimo de atendimento é de 20 minutos', Config.TOAST.ERROR);
        return
      }
    }

    if (moment(rat.dataInicio + ' ' +  rat.horaSolucao, 'YYYY-MM-DD HH:mm').isBefore(moment(rat.dataInicio + ' ' +  rat.horaInicio, 'YYYY-MM-DD HH:mm'))) {
      this.exibirToast('A solução deve ocorrer após o início', Config.TOAST.ERROR);
      return
    }

    if (moment().isBefore(moment(rat.dataInicio + ' ' +  rat.horaSolucao, 'YYYY-MM-DD HH:mm'))) {
      this.exibirToast('A solução não pode ocorrer no futuro', Config.TOAST.ERROR);
      return
    }

    if (this.chamado.rats.length == 0) {
      this.chamado.rats.push(rat);
    } else {
      this.chamado.rats[0].numRat = form.value.numRat;
      this.chamado.rats[0].dataInicio = form.value.dataInicio;
      this.chamado.rats[0].horaInicio = form.value.horaInicio;
      this.chamado.rats[0].dataSolucao = form.value.dataInicio;
      this.chamado.rats[0].horaSolucao = form.value.horaSolucao;
      this.chamado.rats[0].nomeAcompanhante = form.value.nomeAcompanhante;
      this.chamado.rats[0].obsRAT = form.value.obsRAT;
      this.chamado.rats[0].codUsuarioCad = this.dg.usuario.codUsuario;

      if (this.usuarioPonto) {
        this.chamado.rats[0].horarioInicioIntervalo = this.usuarioPonto.registros[1];
        this.chamado.rats[0].horarioTerminoIntervalo = this.usuarioPonto.registros[2];
      }
    }

    this.chamadoService.atualizarChamado(this.chamado);
    this.configurarSlide(this.slides.getActiveIndex());
    this.slides.slideTo(this.slides.getActiveIndex()+1, 500);
  }

  public fecharChamado() {
    let mensagem: string;

    if (this.chamado.dataFimSLA && this.chamado.rats.length) {
      let inicio = moment(this.chamado.rats[0].dataSolucao + ' ' + this.chamado.rats[0].horaSolucao);
      let fim = moment(this.chamado.dataFimSLA);

      let difMin = fim.diff(inicio, 'minutes');

      if (difMin < 0 && difMin > -60) {
        mensagem = `Confirma o fechamento deste chamado com ${difMin} minutos de atraso?`;
      } else {
        mensagem = `Deseja fechar do chamado ${this.chamado.codOs}?`;
      }
    } else {
      mensagem = `Deseja fechar do chamado ${this.chamado.codOs}?`;
    }

    const confirmacao = this.alertCtrl.create({
      title: 'Fechar',
      message: mensagem,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.chamadoService.buscarStatusExecucao().then(executando => {
              if (!this.validarCamposObrigatorios()) return;

              if (executando) {
                this.exibirToast(Config.MSG.AGUARDE_ALGUNS_INSTANTES, Config.TOAST.WARNING);
                return;
              }

              this.chamado.statusServico.codStatusServico = Config.CHAMADO.FECHADO;
              this.chamado.statusServico.abreviacao = "F";
              this.chamado.statusServico.nomeStatusServico = "FECHADO";
              this.chamado.dataHoraFechamento = new Date().toLocaleString('pt-BR');
              this.chamado.appVersao = Config.VERSAO_APP;

              this.loadingFactory.exibir(Config.MSG.FECHANDO_CHAMADO);

              setTimeout(() => {
                this.chamadoService.atualizarChamado(this.chamado).catch();
                this.loadingFactory.alterar(Config.MSG.SALVANDO_CHAMADO_BASE_LOCAL);

                setTimeout(() => {
                  this.loadingFactory.encerrar();
                  this.navCtrl.pop().catch();
                }, 1000);
              }, 1500);
            }).catch();
          }
        }
      ]
    });

    confirmacao.present();
  }

  private validarCamposObrigatorios(): boolean {
    if (!this.chamado.checklistPreventiva.realizado && this.chamado.checklistPreventiva.itens.length > 0) {
      this.exibirToast("Favor realizar o Checklist");

      return;
    }

    if (this.chamado.rats.length == 0) {
      this.exibirToast("favor inserir a RAT");

      return;
    }

    if (this.platform.is('cordova')) {
      if (this.chamado.indRatEletronica && this.chamado.rats[0].fotos.length < 3 && this.chamado.tipoIntervencao.codTipoIntervencao !== 14) {
        this.exibirToast("Este chamado deve conter no mínimo 3 fotos");

        return;
      } 
      
      if (!this.chamado.indRatEletronica && this.chamado.rats[0].fotos.length < 4 && this.chamado.tipoIntervencao.codTipoIntervencao !== 14) {
        this.exibirToast("Este chamado deve conter no mínimo 4 fotos");

        return;
      }

      if (this.verificarSeExigeDocumentosAdicionaisInstalacao() && this.chamado.rats[0].fotos.length < 7) {
        this.exibirToast("Este chamado deve conter no mínimo 7 fotos");
      }
    }

    if (this.chamado.rats[0].laudos.length == 0 && this.verificarLaudoObrigatorio()) {
      this.exibirToast("Este chamado deve possuir um laudo");

      return;
    }

    if ((!this.chamado.rats[0].numRat && !this.chamado.indRatEletronica) || !this.chamado.rats[0].horaInicio 
      || !this.chamado.rats[0].horaSolucao || !this.chamado.rats[0].obsRAT || !this.chamado.rats[0].nomeAcompanhante) {
      this.exibirToast('Favor informar os dados da RAT');

      return false;
    }

    if (this.chamado.rats[0].ratDetalhes.length == 0) {
      this.exibirToast('Favor inserir os detalhes da RAT');

      return false;
    }

    return true;
  }

  private carregarDadosGlobais(): Promise<DadosGlobais> {
    return new Promise((resolve, reject) => {
      this.dadosGlobaisService.buscarDadosGlobaisStorage()
        .then((dados) => {
          if (dados)
            this.dg = dados;
            resolve(dados);
        })
        .catch((err) => {
          reject(new Error(err.message))
        });
    });
  }

  private obterRegistrosPonto(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.usuarioService.buscarRegistrosPonto(
        this.dg.usuario.codUsuario)
        .subscribe(res => {
          this.usuarioPonto = res;

          resolve(this.usuarioPonto);
        },
        err => {
          reject();
        });
    });
  }

  public verificarSeEquipamentoEPOS(): boolean {
    var i;

    for (i = 0; i < this.equipamentosPOS.length; i++) {
      if (this.equipamentosPOS[i].codEquip === this.chamado.equipamentoContrato.equipamento.codEquip) {
          return true;
      }

      if (this.equipamentosPOS[i].codEquip === this.chamado.codEquip) {
        return true;
      }
    }

    return false;
  }

  public verificarSeExigeDocumentosAdicionaisInstalacao(): boolean {
    if (this.chamado.codInstalacao !== undefined || this.chamado.codInstalacao !== 0) {
      if (this.chamado.tipoIntervencao.codTipoIntervencao === Config.TIPO_INTERVENCAO.INSTALAÇÃO) {
        if (this.chamado.cliente.codCliente === Config.CLIENTE.BANRISUL || 
            this.chamado.cliente.codCliente === Config.CLIENTE.BB ||
            this.chamado.cliente.codCliente ===  Config.CLIENTE.BANESTES ||
            this.chamado.cliente.codCliente ===  Config.CLIENTE.BANCO_DA_AMAZONIA ||
            this.chamado.cliente.codCliente ===  Config.CLIENTE.CEF) {
          return true;
        }
      }
    }

    return false;
  }

  public removerRatDetalhe(ratDetalhe: any, i: number) {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja excluir este detalhe?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.chamado.rats[0].ratDetalhes.splice(i, 1);
            this.chamadoService.atualizarChamado(this.chamado);
            this.exibirToast('Detalhe excluído com sucesso', Config.TOAST.SUCCESS)
          }
        }
      ]
    });

    confirmacao.present();
  }

  private configurarSlide(i: number) {
    switch (i) {
      case 0:
        this.indexSlide = (i + 1);
        this.tituloSlide = (i + 1) + ". " + "Informações do Chamado";

        this.slides.lockSwipeToPrev(true);
        this.slides.lockSwipeToNext(false);
        break;
      case 1:
        this.indexSlide = (i + 1);
        this.tituloSlide = `${this.indexSlide}. Deslocamento e Checkin`;

        this.slides.lockSwipeToPrev(false);
        if (!this.chamado.checkin.localizacao.latitude
          || !this.chamado.checkin.localizacao.longitude
          || this.chamado.indBloqueioReincidencia)
          this.slides.lockSwipeToNext(true);
          else
            this.slides.lockSwipeToNext(false);
        break;
      case 2:
        this.indexSlide = (i + 1);
        this.tituloSlide = `${this.indexSlide}. Imagens do RAT`;
        
        this.slides.lockSwipeToPrev(false);
        this.slides.lockSwipeToNext(false);
        break;
      case 3:
        this.indexSlide = (i + 1);
        this.tituloSlide = `${this.indexSlide}. Relatório de Atendimento`;
        
        this.slides.lockSwipeToPrev(false);
        if ((!this.chamado.rats[0].numRat && !this.chamado.indRatEletronica) || !this.chamado.rats[0].horaInicio
          || !this.chamado.rats[0].horaSolucao || !this.chamado.rats[0].obsRAT
          || !this.chamado.rats[0].nomeAcompanhante) {
          this.slides.lockSwipeToNext(true);
          } else {
            this.slides.lockSwipeToNext(false);
          }
        break;
      case 4:
        this.indexSlide = (i + 1);
        this.tituloSlide = `${this.indexSlide}. Detalhes do RAT`;

        this.slides.lockSwipeToPrev(false);
        if (
            (this.chamado.rats[0].ratDetalhes.length == 0 && !this.verificarSeEquipamentoEPOS()) ||
            (!this.chamado.rats[0].defeitoPOS && this.verificarSeEquipamentoEPOS())
          )
          this.slides.lockSwipeToNext(true);
          else {
            this.slides.lockSwipeToNext(false);
          }
        break;
      case 5:
          this.indexSlide = (i + 1);
          this.tituloSlide = `${this.indexSlide}. Assinatura do Técnico`;
          
          this.slides.lockSwipeToPrev(false);
          if (!this.chamado.rats[0].assinaturaTecnico)
            //this.slides.lockSwipeToNext(true);
            this.slides.lockSwipeToNext(true);
          else {
            this.slides.lockSwipeToNext(false);
          }
          break;
      case 6:
          this.indexSlide = (i + 1);
          this.tituloSlide = `${this.indexSlide}. Assinatura do Cliente`;
          
          this.slides.lockSwipeToPrev(false);
          //if (!this.chamado.rats[0].assinaturaCliente) this.slides.lockSwipeToNext(false);
          this.slides.lockSwipeToNext(false);
          break;
      case 7:
        this.indexSlide = (i + 1);
        this.tituloSlide = `${this.indexSlide}. Checkout e Fechamento`;

        this.slides.lockSwipeToPrev(false);
        if (!this.chamado.checkout.localizacao.latitude || !this.chamado.checkout.localizacao.longitude) {
          this.slides.lockSwipeToNext(true);
        }
        else {
          this.slides.lockSwipeToNext(false);
        }
        break;
    }
  }

  private registrarLeituraOs(): Promise<any> {
    return new Promise((resolve, reject) => {
      if (!this.chamado.dataHoraOSMobileLida) {
        this.chamado.dataHoraOSMobileLida = new Date().toLocaleString('pt-BR');

        this.chamadoService.registrarLeituraChamadoApi(this.chamado).subscribe((r) => {
          this.chamadoService.atualizarChamado(this.chamado);

          resolve();
        },
        err => {
          this.chamado.dataHoraOSMobileLida = null;
          reject();
        });
      }
    });
  }

  private exibirAlerta(msg: string) {
    const alerta = this.alertCtrl.create({
      title: 'Alerta!',
      subTitle: msg,
      buttons: ['OK']
    });

    alerta.present();
  }

  private exibirToast(mensagem: string, tipo: string='info', posicao: string=null) {
    const toast = this.toastCtrl.create({
      message: mensagem, 
      duration: Config.TOAST.DURACAO, 
      position: posicao || 'bottom', 
      cssClass: 'toast-' + tipo
    });
    
    toast.present();
  }

  ionViewWillLeave(){
    this.chamadoService.sincronizarChamados(this.chamado.dataHoraFechamento ? true : false, this.dg.usuario.codTecnico).catch();
  }
}