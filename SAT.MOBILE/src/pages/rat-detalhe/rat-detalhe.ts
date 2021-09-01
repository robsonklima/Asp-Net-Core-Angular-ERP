import { RatDetalhe } from '../../models/rat-detalhe';
import { Component, ViewChild } from '@angular/core';
import { AlertController, NavParams, Slides, ViewController, ToastController, ModalController } from 'ionic-angular';
import { NgForm } from '@angular/forms';

import { Config } from '../../models/config';

import { Chamado } from './../../models/chamado';
import { TipoServico } from "../../models/tipo-servico";
import { Peca } from "../../models/peca";
import { Acao } from "../../models/acao";
import { TipoCausa } from "../../models/tipo-causa";
import { Causa } from "../../models/causa";
import { Defeito } from "../../models/defeito";

import { TipoServicoService } from "../../services/tipo-servico";
import { AcaoService } from "../../services/acao";
import { CausaService } from "../../services/causa";
import { DefeitoService } from "../../services/defeito";
import { PecaService } from '../../services/peca';
import { ChamadoService } from "../../services/chamado";
import { EquipamentoCausaService } from '../../services/equipamento-causa';
import { EquipamentoCausa } from '../../models/equipamento-causa';
import { EquipamentoPOS } from '../../models/equipamentoPOS';
import { EquipamentoPOSService } from '../../services/equipamento-pos';
import { RatDetalhePosPage } from './rat-detalhe-pos';
import { DefeitoCausaService } from '../../services/defeito-causa';
import { AcaoCausaService } from '../../services/acao-causa';


@Component({
  selector: 'rat-detalhe-page',
  templateUrl: 'rat-detalhe.html'
})
export class RatDetalhePage {
    @ViewChild(Slides) slides: Slides;
    tituloSlide: string;
    chamado: Chamado;
    ratDetalhe: RatDetalhe;
    tipoCausas: TipoCausa[] = [];
    tipoServico: TipoServico;
    tipoServicos: TipoServico[];
    tipoCausa: TipoCausa;
    defeito: Defeito;
    defeitos: Defeito[] = [];
    causa: Causa;
    modulos: Causa[] = [];
    subModulos: Causa[] = [];
    componentes: Causa[] = [];
    acao: Acao;
    acoes: Acao[] = [];
    pecas: Peca[] = [];
    inputPesquisar: string;
    equipamentosPOS: EquipamentoPOS[] = [];
    spinner: boolean;
    qtd = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 ];

  constructor(
    private navParams: NavParams,
    private viewCtrl: ViewController,
    private toastCtrl: ToastController,
    private alertCtrl: AlertController,
    private modalCtrl: ModalController,
    private acaoService: AcaoService,
    private causaService: CausaService,
    private defeitoService: DefeitoService,
    private pecaService: PecaService,
    private chamadoService: ChamadoService,
    private tipoServicoService: TipoServicoService,
    private equipamentoCausaService: EquipamentoCausaService,
    private defeitoCausaService: DefeitoCausaService,
    private acaoCausaService: AcaoCausaService,
    private equipamentoPOSService: EquipamentoPOSService
  ) {
    this.chamado = this.navParams.get('chamado');
  }

  ionViewWillEnter() {
    this.configurarSlide(this.slides.getActiveIndex());
    
    this.buscarEquipamentosPOS().then(() => {
      this.buscarTipoCausas();
      this.buscarDefeitosTodos();
      this.buscarAcoesTodas();
    });   
  }

  public telaRatDetalhePOS() {
    const modal = this.modalCtrl.create(RatDetalhePosPage, { chamado: this.chamado });
    modal.present();
    modal.onDidDismiss(() => {
      this.configurarSlide(this.slides.getActiveIndex());
    });
  }

  public buscarTipoCausas() {
    this.tipoCausas = [{
      codTipoCausa: 1,
      nomeTipoCausa: 'Máquina'
    }, {
      codTipoCausa: 2,
      nomeTipoCausa: 'Extra-Máquina'
    }]
  }

  public buscarTipoServicos(codTipoCausa: string) {
    this.tipoServicoService.buscarTipoServicosStorage()
      .then((tipoServicos: TipoServico[]) => {
        this.tipoServicos = tipoServicos
          .filter((ts) => { return (ts.codETipoServico.substring(0, 1) == codTipoCausa) })
          .sort((a, b) => ( Number(a.codETipoServico) - Number(b.codETipoServico) ));
      })
      .catch(err => {});
  }

  private buscarModulosTodos() {
    this.causaService.buscarCausasStorage().then((causas: Causa[]) => {
      this.modulos = causas.filter((causa) => { return (causa.codECausa.substring(2, 5) == '000') });

      if (this.verificarSeEquipamentoEPOS()) {
        this.modulos = this.modulos.filter((m) => { return (Number(m.codECausa) >= 50000) });
      }
    }).catch(err => {});
  }

  public buscarModulosPorEquipamento(codTipoCausa: any=null) {
    if (codTipoCausa == Config.TIPO_CAUSA.EXTRA_MAQUINA) {
      this.buscarModulosTodos();   
    } else {
      this.equipamentoCausaService.buscarCausasPorEquipamento(
        this.chamado.equipamentoContrato.equipamento.codEquip || this.chamado.codEquip
      ).then((ec) => {
        this.modulos = ec[0].causas;
        if (this.modulos.length === 0) this.buscarModulosTodos();
      })
      .catch(() => {
        this.buscarModulosTodos();
      });
    }   
  }

  public buscarSubModulosDefeitosEAcoes(modulo: string) {
    this.buscarDefeitosPorCausa(modulo);
    this.buscarAcoesPorCausa(modulo);

    this.causaService.buscarCausasStorage()
      .then((causas: Causa[]) => {
        this.subModulos = causas.filter((causa) => { 
          return (causa.codECausa.substring(0, 2) == modulo.substring(0, 2) 
            && causa.codECausa.substring(3, 5) == '00');
        });
      })
      .catch(err => {});

    this.componentes = [];
  }

  public buscarComponentes(subModulo: string) {
    this.causaService.buscarCausasStorage()
      .then((causas: Causa[]) => {
        this.componentes = causas.filter((causa) => { 
          return (causa.codECausa.substring(0, 3) == subModulo.substring(0, 3));
        });
      })
      .catch(err => {});
  }

  public buscarDefeitosTodos() {
    this.defeitoService.buscarDefeitosStorage()
      .then((defeitos: Defeito[]) => {
        if (!defeitos.length) 
          this.exibirAlerta(Config.MSG.DEFEITOS_NAO_ENCONTRADOS_COMPONENTE);
        
        this.defeitos = defeitos.sort((a, b) => Number(a.codEDefeito) - Number(b.codEDefeito));

        if (this.verificarSeEquipamentoEPOS()) {
          this.defeitos = this.defeitos.filter((d) => { 
            return (Number(d.codEDefeito) >= 500);
          });
        }
      })
      .catch(err => {});
  }

  public buscarDefeitosPorCausa(causa: string) {
    if (this.verificarSeEquipamentoEPOS()) {
      this.buscarDefeitosTodos();
      return;
    }

    this.defeitoCausaService.buscarDefeitosPorCausa(causa)
      .then((res) => {
        if (res.length) {
          this.defeitos = res[0].defeitos;
        }
          
        if (this.defeitos.length === 0) {
          this.buscarDefeitosTodos();
        }
      })
      .catch();
  }

  public buscarAcoesTodas() {
    this.acaoService.buscarAcoesStorage()
      .then((acoes: Acao[]) => {
        if (!acoes.length)
          this.exibirAlerta(Config.MSG.ACOES_NAO_ENCONTRADAS_COMPONENTE);
        
        this.acoes = acoes.sort((a, b) => 
          Number(a.codEAcao) - Number(b.codEAcao));
      })
      .catch(err => {});
  }

  public buscarAcoesPorCausa(causa: string) {
    if (this.verificarSeEquipamentoEPOS()) {
      this.buscarAcoesTodas();
      return;
    }

    this.acaoCausaService.buscarAcoesPorCausa(causa)
      .then((res) => {
        if (res.length) {
          this.acoes = res[0].acoes;
        }
          
        if (this.acoes.length === 0) {
          this.buscarAcoesTodas();
        }
      })
      .catch();
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

  public salvarRatDetalhe(form: NgForm) {
    this.chamadoService.buscarStatusExecucao().then(executando => {
      if (executando) {
        this.exibirToast(Config.MSG.AGUARDE_ALGUNS_INSTANTES, Config.TOAST.WARNING);
        return;
      }

      let tipoCausa = new TipoCausa();
    tipoCausa = form.value.tipoCausa;
    let tipoServico = new TipoServico()
    tipoServico = form.value.tipoServico;
    let defeito = new Defeito();
    defeito = form.value.defeito;
    let causa = new Causa();
    causa = form.value.causa;
    let acao = new Acao();
    acao = form.value.acao;
    let pecas: Peca[] = [];

    if (!tipoCausa.codTipoCausa  || !tipoServico.codTipoServico || !defeito.codDefeito || !causa.codCausa || !acao.codAcao ) {
      this.exibirToast('Erro ao salvar detalhe! Tente novamente', Config.TOAST.ERROR);
      this.viewCtrl.dismiss();
      return
    } else {
      this.ratDetalhe = {
        tipoCausa: tipoCausa,
        tipoServico: tipoServico,
        defeito: defeito,
        causa: causa,
        acao: acao,
        pecas: pecas
      };
      
      this.configurarSlide(this.slides.getActiveIndex());
      this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
  
      if (
          this.ratDetalhe.acao.codAcao == Config.ACAO.PENDENCIA_PECA.CODACAO &&
          (
            this.chamado.cliente.codCliente == Config.CLIENTE.BB ||
            this.chamado.cliente.codCliente == Config.CLIENTE.BANRISUL ||
            this.chamado.cliente.codCliente == Config.CLIENTE.BRB ||
            this.chamado.cliente.codCliente == Config.CLIENTE.PROTEGE ||
            this.chamado.cliente.codCliente == Config.CLIENTE.SICREDI ||
            this.chamado.cliente.codCliente == Config.CLIENTE.SAQUE_PAGUE ||
            (
              this.chamado.cliente.codCliente == Config.CLIENTE.PERTO_PARKING &&
              (
                this.chamado.equipamentoContrato.equipamento.codEquip == Config.EQUIPAMENTO.TPC_4110 ||
                this.chamado.equipamentoContrato.equipamento.codEquip == Config.EQUIPAMENTO.TPC_4110_290_01_969
              )
            )
          )
        ) 
        {
          this.apresentarCampoProtocoloStn();
        }

        if (causa.codECausa.substring(0, 2) == "08") {
          this.exibirAlerta(Config.MSG.CHAMADO_EXIGE_LAUDO);
        }
      }
    });
  }

  public salvarRatDetalheNoChamadoESair() {
    this.chamado.rats[0].ratDetalhes.push(this.ratDetalhe);
    this.chamadoService.atualizarChamado(this.chamado);
    this.exibirToast(Config.MSG.DETALHE_ADICIONADO, Config.TOAST.SUCCESS);

    if (this.verificarSeEquipamentoEPOS()) {
      this.viewCtrl.dismiss();
      this.telaRatDetalhePOS();
    } else {
      this.viewCtrl.dismiss();
    }
  }

  public verificarSeEquipamentoEPOS(): boolean {
    var i;
    let retorno: boolean = false

    for (i = 0; i < this.equipamentosPOS.length; i++) {
      if (this.equipamentosPOS[i].codEquip === this.chamado.equipamentoContrato.equipamento.codEquip) {
        retorno = true;
      }

      if (this.equipamentosPOS[i].codEquip === this.chamado.codEquip) {
        retorno = true;
      }
    }

    return retorno;
  }

  public filtrarPecas(ev: any) {
    let val = ev.target.value;

    if (val && val.trim() != '') {
      if (val.length < 4) {
        return
      }
    }

    this.spinner = true;

    this.carregarPecasStorage().then(() => {
      this.spinner = false;

      this.pecas = this.pecas.filter((peca) => {
        if (val && val.trim() != '') {
          return (peca.nomePeca.toString().toLowerCase().indexOf(val.toLowerCase()) > -1
            || peca.codMagnus.toString().toLowerCase().indexOf(val.toLowerCase()) > -1);
        }
      })
    });
  }

  public limparPesquisa() {
    this.pecas.splice(0, this.pecas.length);
    this.inputPesquisar = "";
  }

  public adicionarPeca(peca: Peca) {
    if (!this.verificaPecaJaAdicionada(peca)) {
      this.ratDetalhe.pecas.push({
        codPeca: peca.codPeca,
        codMagnus: peca.codMagnus,
        nomePeca: peca.nomePeca,
        qtd: 1
      });
      this.limparPesquisa();
    } else {
      this.exibirToast("Esta peça já foi adicionada", Config.TOAST.ERROR);
    }
  }

  public alterarQtdPeca(qtd: number, i: number) {
    this.ratDetalhe.pecas[i].qtd = qtd;
  }

  public removerPeca(peca: Peca) {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja excluir esta peça?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Confirmar',
          handler: () => {
            let i = this.ratDetalhe.pecas
              .map(function (p) { return p.codPeca; }).indexOf(peca.codPeca);

            if (i > -1) {
              this.ratDetalhe.pecas.splice(i, 1);
              this.exibirToast("Peça removida com sucesso", Config.TOAST.SUCCESS);
              this.chamadoService.atualizarChamado(this.chamado);
            }
          }
        }
      ]
    });

    confirmacao.present();
  }

  public fecharModalConfirmacao() {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja sair e perder as informações inseridas?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.viewCtrl.dismiss();
          }
        }
      ]
    });

    confirmacao.present();
  }

  private verificaPecaJaAdicionada(peca: Peca) {
    let res: boolean;

    this.ratDetalhe.pecas.forEach((p, i) => {
      if (p.codMagnus
        .toString()
        .toLowerCase()
        .indexOf(peca.codMagnus.toString().toLowerCase()) > -1)
        res = true;
    });

    return res;
  }

  private apresentarCampoProtocoloStn() {
    let prompt = this.alertCtrl.create({
      title: 'Protocolo STN',
      message: `Digite o protocolo STN para esta 
                  pendência de peças`,
      enableBackdropDismiss: false,
      inputs: [
        {
          name: 'protocoloStn',
          type: 'number',
          placeholder: 'Protocolo STN'
        },
      ],
      buttons: [
        {
          text: 'Salvar',
          handler: dados => {
            if (!dados.protocoloStn || dados.protocoloStn.trim() == '') {
              return false
            }

            this.ratDetalhe.protocoloStn = dados.protocoloStn.trim();
          }
        }
      ]
    });
    prompt.present();
  }

  private carregarPecasStorage(): Promise<any> {
    return new Promise((resolve, reject) => {
      resolve(
        this.pecaService.buscarPecasStorage()
          .then(
          (pecas: Peca[]) => {
            this.pecas = pecas;
          })
          .catch(err => { })
      );
    });
  }

  private configurarSlide(i: number) {
    switch (i) {
      case 0:
        this.tituloSlide = "5." + (i + 1) + ". " + "Inserindo Detalhe";
        this.slides.lockSwipeToPrev(true);
        if (!this.ratDetalhe)
          this.slides.lockSwipeToNext(true);
          else
            this.slides.lockSwipeToNext(false);
        break;
      case 1:
        this.tituloSlide = "5." + (i + 1) + ". " + "Inserindo Peças";
        this.slides.lockSwipeToPrev(false);
        this.slides.lockSwipeToNext(true);
        break;
    }
  }

  public alterarSlide() {
    this.configurarSlide(this.slides.getActiveIndex());
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
}