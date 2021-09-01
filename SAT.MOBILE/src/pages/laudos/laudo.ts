import { Component, ViewChild, OnInit  } from '@angular/core';
import { NavParams, Slides, ToastController, AlertController, ViewController, ModalController } from 'ionic-angular';
import { Laudo } from '../../models/laudo';
import { Chamado } from '../../models/chamado';
import { NgForm } from '@angular/forms';
import { Foto } from '../../models/foto';
import { Config } from '../../models/config';

import { SituacaoPage } from './situacao';
import { ChamadoService } from '../../services/chamado';

import moment from 'moment';
import { AssinaturaPage } from '../assinatura/assinatura';


@Component({
  selector: 'laudo-page',
  templateUrl: 'laudo.html'
})
export class LaudoPage {
  @ViewChild(Slides) slides: Slides;
  semInformacoesRedeEletrica: boolean;
  tituloSlide: string;
  chamado: Chamado;
  laudo: Laudo;
  foto: Foto;

  constructor(
    private chamadoService: ChamadoService,
    private navParams: NavParams,
    private toastCtrl: ToastController,
    private alertCtrl: AlertController,
    private viewCtrl: ViewController,
    private modalCtrl: ModalController
  ) {
    this.chamado = this.navParams.get('chamado');
  }

  ionViewWillEnter() {
    this.configurarSlide(this.slides.getActiveIndex());
  }

  ngOnInit() {
    this.laudo = new Laudo();
  }

  public telaSituacao() {
    const modal = this.modalCtrl.create(SituacaoPage, { laudo: this.laudo });
    modal.present();
    modal.onDidDismiss((laudo) => {
      this.laudo = laudo;

      this.configurarSlide(this.slides.getActiveIndex());
    }); 
  }

  public telaAssinaturaTecnico() {
    const modal = this.modalCtrl.create(AssinaturaPage, { paginaOrigem: "LAUDO_TECNICO", laudo: this.laudo });
    modal.present();
    modal.onDidDismiss((laudo) => {
      this.laudo = laudo;
      
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public telaAssinaturaCliente() {
    let prompt = this.alertCtrl.create({
      title: 'Dados do Cliente',
      message: `Favor informar os dados do cliente`,
      enableBackdropDismiss: false,
      inputs: [
        {
          name: 'nomeCliente',
          type: 'text',
          placeholder: 'Nome do Cliente'
        },
        {
          name: 'matriculaCliente',
          type: 'text',
          placeholder: 'Matrícula do Cliente'
        }
      ],
      buttons: [
        {
          text: 'Salvar',
          handler: res => {
            if (!res.nomeCliente || !res.matriculaCliente) {
              return false
            }

            this.laudo.nomeCliente = res.nomeCliente.trim();
            this.laudo.matriculaCliente = res.matriculaCliente.trim();

            const modal = this.modalCtrl.create(AssinaturaPage, { paginaOrigem: "LAUDO_CLIENTE", laudo: this.laudo });
            modal.present();
            modal.onDidDismiss((laudo) => {
              this.laudo = laudo;

              this.configurarSlide(this.slides.getActiveIndex());
            });
          }
        }
      ]
    });
    prompt.present();
  }

  public criarLaudo(form: NgForm) {
    this.laudo.codOS = this.chamado.codOs;
    this.laudo.codTecnico = this.chamado.tecnico.codTecnico;
    this.laudo.relatoCliente = form.value.relatoCliente;
    this.laudo.dataHoraCad = moment().format();
    this.laudo.situacoes = [];
    this.laudo.assinaturaTecnico = null;
    this.laudo.assinaturaCliente = null;
    this.laudo.nomeCliente = null;
    this.laudo.matriculaCliente = null;
    this.laudo.indAtivo = 1;
    
    this.configurarSlide(this.slides.getActiveIndex());
    this.slides.slideTo(1, 500);
  }

  public salvarLaudo(form: NgForm) {
    this.laudo.conclusao = form.value.conclusao;
    
    this.configurarSlide(this.slides.getActiveIndex());
    this.slides.slideTo(4, 500);
  }

  public removerSituacao(i: number) {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja excluir esta situação?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.laudo.situacoes.splice(i, 1);
            this.exibirToast("Situação removida com sucesso")
          }
        }
      ]
    });

    confirmacao.present();
  }

  public marcarSemInformacoesRedeEletrica(event){
    this.semInformacoesRedeEletrica = event.checked;
  }

  public salvarInformacoesRedeEletrica(form: NgForm) {
    this.laudo.tensaoSemCarga = form.value.tensaoSemCarga;
    this.laudo.tensaoComCarga = form.value.tensaoComCarga;
    this.laudo.tensaoTerraENeutro = form.value.tensaoTerraENeutro;
    this.laudo.temperatura = form.value.temperatura;
    this.laudo.indRedeEstabilizada = form.value.indRedeEstabilizada;
    this.laudo.indPossuiNobreak = form.value.indPossuiNobreak;
    this.laudo.indPossuiArCond = form.value.indPossuiArCond;

    if (form.value.indSemInformacoesRedeEletrica) {
      this.solicitarJustificativa(); 
    }
    
    this.configurarSlide(this.slides.getActiveIndex());
    this.slides.slideTo(3, 500);
  }

  private solicitarJustificativa() {
    let prompt = this.alertCtrl.create({
      title: 'Importante!',
      message: `Você não informou todos os campos da rede elétrica e infra estrutura`,
      enableBackdropDismiss: false,
      inputs: [
        {
          name: 'justificativa',
          type: 'text',
          placeholder: 'Justificativa'
        },
      ],
      buttons: [
        {
          text: 'Salvar',
          handler: res => {
            if (!res.justificativa || res.justificativa.trim() == '') {
              return false
            }

            this.laudo.justificativa = res.justificativa.trim();
          }
        }
      ]
    });
    prompt.present();
  }

  public finalizarLaudo() {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja finalizar este laudo?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Confirmar',
          handler: () => {
            if (this.laudo.situacoes.length == 0) {
              this.exibirToast('Favor informar as situações do laudo');
              return
            }

            this.chamado.rats[0].laudos.push(this.laudo);
            this.chamadoService.atualizarChamado(this.chamado);

            this.fecharModal();
          }
        }
      ]
    });

    confirmacao.present();
  }

  public alterarSlide() {
    this.configurarSlide(this.slides.getActiveIndex());
  }

  private configurarSlide(i: number) {
    switch (i) {
      case 0:
        this.tituloSlide = (i + 1) + ". " + "Relato do Cliente no Atendimento";
        
        if (!this.laudo.relatoCliente) {
          this.slides.lockSwipeToNext(true);
        } else {
          this.slides.lockSwipeToNext(false);
        }
        
        break;
      case 1:
        this.tituloSlide = (i + 1) + ". " + "Situações";
       
        if (this.laudo.situacoes.length == 0) {
          this.slides.lockSwipeToNext(true);
        } else {
          this.slides.lockSwipeToNext(false);
        }
        break;
      case 2:
        this.tituloSlide = (i + 1) + ". " + "Infraestrutura e Rede Elétrica";
        
        if ((!this.laudo.tensaoSemCarga && !this.laudo.tensaoComCarga && !this.laudo.tensaoTerraENeutro) && !this.semInformacoesRedeEletrica) {
          this.slides.lockSwipeToNext(true);
        } else {
          this.slides.lockSwipeToNext(false); 
        }
        
        break;
      case 3:
        this.tituloSlide = (i + 1) + ". " + "Conclusão";
        
        if (!this.laudo.conclusao) {
          this.slides.lockSwipeToNext(true);
        } else {
          this.slides.lockSwipeToNext(false);
        }
        
        break;
      case 4:
        this.tituloSlide = (i + 1) + ". " + "Assinatura do Técnico";
        
        if (!this.laudo.assinaturaTecnico) {
          this.slides.lockSwipeToNext(true);
        } else {
          this.slides.lockSwipeToNext(false);
        }
        
        break;
      case 5:
        this.tituloSlide = (i + 1) + ". " + "Assinatura do Cliente";
        
        if (!this.laudo.assinaturaCliente) {
          this.slides.lockSwipeToNext(true);
        } else {
          this.slides.lockSwipeToNext(false);
        }
        
        break;
      case 6:
        this.tituloSlide = (i + 1) + ". " + "Finalizar";
        
        this.slides.lockSwipeToPrev(false);
        this.slides.lockSwipeToNext(false);
        
        break;
    }
  }

  public fecharModalConfirmacao() {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Ao sair você perderá as informações inseridas?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.fecharModal()
          }
        }
      ]
    });

    confirmacao.present();
  }

  private exibirToast(mensagem: string): Promise<any> {
    return new Promise((resolve, reject) => {
      const toast = this.toastCtrl.create({
        message: mensagem, duration: Config.TOAST.DURACAO, position: 'bottom'
      });

      resolve(toast.present());
    });
  }

  public fecharModal() {
    this.viewCtrl.dismiss();
  }
}