import { Component } from '@angular/core';
import { NavParams, ViewController, AlertController, ToastController } from 'ionic-angular';

import { Chamado } from './../../models/chamado';
import { Config } from '../../models/config';
import _ from 'lodash';

import { EquipamentoPOS } from '../../models/equipamentoPOS';
import { OperadoraTelefonia } from '../../models/operadora-telefonia';
import { TipoComunicacao } from '../../models/tipo-comunicacao';
import { MotivoComunicacao } from '../../models/motivo-comunicacao';
import { MotivoCancelamento } from '../../models/motivo-cancelamento';

import { EquipamentoPOSService } from '../../services/equipamento-pos';
import { TipoComunicacaoService } from '../../services/tipo-comunicacao';
import { OperadoraTelefoniaService } from '../../services/operadora-telefonia';
import { MotivoComunicacaoService } from '../../services/motivo-comunicacao';
import { MotivoCancelamentoService } from '../../services/motivo-cancelamento';
import { DefeitoPOSService } from '../../services/defeito-pos';
import { StatusServicoService } from '../../services/status-servico';
import { DadosGlobais } from '../../models/dados-globais';
import { DadosGlobaisService } from '../../services/dados-globais';
import { StatusServico } from '../../models/status-servico';
import { DefeitoPOS } from '../../models/defeito-pos';
import { ChamadoService } from '../../services/chamado';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'rat-detalhe-pos-page',
  templateUrl: 'rat-detalhe-pos.html'
})
export class RatDetalhePosPage {
  chamado: Chamado;
  dg: DadosGlobais;
  equipamentosPOS: EquipamentoPOS[] = [];
  tiposComunicacao: TipoComunicacao[] = [];
  operadoras: OperadoraTelefonia[] = [];
  motivosComunicacao: MotivoComunicacao[] = [];
  motivosCancelamento: MotivoCancelamento[] = [];
  statusServicos: StatusServico[] = [];
  defeitosPOS: DefeitoPOS[] = [];

  constructor(
    private navParams: NavParams,
    private viewCtrl: ViewController,
    private toastCtrl: ToastController,
    private alertCtrl: AlertController,
    private equipamentoPOSService: EquipamentoPOSService,
    private tipoComunicacaoService: TipoComunicacaoService,
    private operadorasTelefoniaService: OperadoraTelefoniaService,
    private motivoComunicacaoService: MotivoComunicacaoService,
    private motivoCancelamentoService: MotivoCancelamentoService,
    private statusServicoService: StatusServicoService,
    private defeitoPOSService: DefeitoPOSService,
    private chamadoService: ChamadoService,
    private dadosGlobaisService: DadosGlobaisService
  ) {
    this.chamado = this.navParams.get('chamado');
  }

  ionViewWillEnter() {
    this.carregarDadosGlobais()
      .then(() => this.buscarEquipamentosPOS())
      .then(() => this.buscarTiposComunicacao()) 
      .then(() => this.buscarMotivosComunicacao()) 
      .then(() => this.buscarMotivosCancelamento())
      .then(() => this.buscarOperadoras())
      .then(() => this.buscarStatusServicos())
      .then(() => this.buscarDefeitosPOS())
      .catch(() => {});
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

  public verificarEquipamentoPossuiChip(): boolean {
    if (
      (
        this.chamado.codEquip === Config.EQUIPAMENTOS_POS.POS_VELOH_3 ||
        this.chamado.codEquip === Config.EQUIPAMENTOS_POS.POS_VELOH_G
      ) || (
        Number(this.chamado.equipamentoContrato.equipamento.codEEquip) === Config.EQUIPAMENTOS_POS.POS_VELOH_3 ||
        Number(this.chamado.equipamentoContrato.equipamento.codEEquip) === Config.EQUIPAMENTOS_POS.POS_VELOH_G
      )
    ) {
      return true;
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

  public salvarInformacoesPOS(form: NgForm) {
    this.chamado.rats[0].statusServico = form.value.statusServico;
    this.chamado.rats[0].equipamentoRetirado = form.value.equipamentoRetirado;
    this.chamado.rats[0].numSerieRetirada = form.value.numSerieRetirada;
    this.chamado.rats[0].equipamentoInstalado = form.value.equipamentoInstalado;
    this.chamado.rats[0].numSerieInstalada = form.value.numSerieInstalada;
    this.chamado.rats[0].rede = form.value.rede;
    this.chamado.rats[0].tipoComunicacao = form.value.tipoComunicacao;
    this.chamado.rats[0].operadoraChipRetirado = form.value.operadoraChipRetirado;
    this.chamado.rats[0].nroChipRetirado = form.value.nroChipRetirado;
    this.chamado.rats[0].operadoraChipInstalado = form.value.operadoraChipInstalado;
    this.chamado.rats[0].nroChipInstalado = form.value.nroChipInstalado;
    this.chamado.rats[0].motivoComunicacao = form.value.motivoComunicacao;
    this.chamado.rats[0].motivoCancelamento = form.value.motivoCancelamento;
    this.chamado.rats[0].obsMotivoComunicacao = form.value.obsMotivoComunicacao;
    this.chamado.rats[0].obsMotivoCancelamento = form.value.obsMotivoCancelamento;
    this.chamado.rats[0].defeitoPOS = form.value.defeitoPOS;
    this.chamado.rats[0].descricaoAtendimento = form.value.descricaoAtendimento;

    this.chamadoService.buscarStatusExecucao().then(executando => {
      if (executando) {
        this.exibirToast(Config.MSG.AGUARDE_ALGUNS_INSTANTES, Config.TOAST.WARNING);
        return;
      }

      if (!this.validarCamposObrigatorios()) return;

      this.chamadoService.atualizarChamado(this.chamado).then(() => {
        this.exibirToast('Informações do POS salvas com sucesso', Config.TOAST.SUCCESS);
        this.fecharModal() 
      });
    });
  }

  private validarCamposObrigatorios(): boolean {
    if (!_.has(this.chamado.rats[0], 'statusServico') || !_.has(this.chamado.rats[0].statusServico, 'codStatusServico')) {
      this.exibirToast("Informe o status de serviço do POS", Config.TOAST.ERROR);

      return false;
    }

    if (!_.has(this.chamado.rats[0], 'defeitoPOS') || !_.has(this.chamado.rats[0].defeitoPOS, 'codDefeitoPOS')) {
      this.exibirToast("Informe o defeito apresentado pelo POS", Config.TOAST.ERROR);

      return false;
    }

    if (
      this.chamado.rats[0].statusServico.codStatusServico == Config.STATUS_SERVICO.CANCELADO || 
      this.chamado.rats[0].statusServico.codStatusServico == Config.STATUS_SERVICO.CANCELADO_COM_ATENDIMENTO
    ) {
      if (!_.has(this.chamado.rats[0], 'motivoCancelamento') || !_.has(this.chamado.rats[0].motivoCancelamento, 'codMotivoCancelamento') || !this.chamado.rats[0].obsMotivoCancelamento) {
        this.exibirToast('Favor inserir o motivo do cancelamento do chamado POS e a observação', Config.TOAST.ERROR);

        return false;
      }
    }

    if (this.chamado.rats[0].statusServico.codStatusServico == Config.STATUS_SERVICO.FECHADO) {
      if (this.chamado.tipoIntervencao.codTipoIntervencao == Config.TIPO_INTERVENCAO.INSTALAÇÃO) {
        if (!_.has(this.chamado.rats[0], 'equipamentoInstalado') || !_.has(this.chamado.rats[0].equipamentoInstalado, 'codEquip') || !this.chamado.rats[0].numSerieInstalada) {
          this.exibirToast('Favor inserir o equipamento POS instalado e a série', Config.TOAST.ERROR);

          return false;
        }

        if (!_.has(this.chamado.rats[0], 'tipoComunicacao') || !_.has(this.chamado.rats[0].tipoComunicacao, 'codTipoComunicacao')) {
          this.exibirToast('Favor inserir o tipo de comunicação do POS', Config.TOAST.ERROR);

          return false;
        }

        if (!this.chamado.rats[0].rede && this.chamado.cliente.codCliente == Config.CLIENTE.BANRISUL) {
          this.exibirToast('Favor inserir a rede do equipamento POS', Config.TOAST.ERROR);

          return false;
        }
      }

      if (this.chamado.tipoIntervencao.codTipoIntervencao == Config.TIPO_INTERVENCAO.CORRETIVA) {
        if (!_.has(this.chamado.rats[0], 'equipamentoInstalado') || !_.has(this.chamado.rats[0].equipamentoInstalado, 'codEquip') || !this.chamado.rats[0].numSerieInstalada) {
          this.exibirToast('Favor inserir o equipamento POS instalado', Config.TOAST.ERROR);

          return false;
        }

        if (this.verificarSeDefeitoExigeTroca()) {
          if (!_.has(this.chamado.rats[0], 'equipamentoInstalado') || !_.has(this.chamado.rats[0].equipamentoInstalado, 'codEquip') || !this.chamado.rats[0].numSerieInstalada) {
            this.exibirToast('Favor inserir o equipamento POS instalado', Config.TOAST.ERROR);
  
            return false;
          }
  
          if (!_.has(this.chamado.rats[0], 'equipamentoRetirado') || !_.has(this.chamado.rats[0].equipamentoRetirado, 'codEquip') || !this.chamado.rats[0].numSerieRetirada) {
            this.exibirToast('Favor inserir o equipamento POS retirado', Config.TOAST.ERROR);
  
            return false;
          }
        }

        if (!_.has(this.chamado.rats[0], 'tipoComunicacao') || !_.has(this.chamado.rats[0].tipoComunicacao, 'codTipoComunicacao')) {
          this.exibirToast('Favor inserir o tipo de comunicação do POS', Config.TOAST.ERROR);

          return false;
        }

        if (!this.chamado.rats[0].rede && this.chamado.cliente.codCliente == Config.CLIENTE.BANRISUL) {
          this.exibirToast('Favor inserir a rede do equipamento POS', Config.TOAST.ERROR);

          return false;
        }
      }

      if (this.chamado.tipoIntervencao.codTipoIntervencao == Config.TIPO_INTERVENCAO.DESINSTALAÇÃO) {
        if (!_.has(this.chamado.rats[0], 'equipamentoRetirado') || !_.has(this.chamado.rats[0].equipamentoRetirado, 'codEquip') || !this.chamado.rats[0].numSerieRetirada) {
          this.exibirToast('Favor inserir o equipamento POS retirado e a série', Config.TOAST.ERROR);

          return false;
        }

        if (!this.chamado.rats[0].rede && this.chamado.cliente.codCliente == Config.CLIENTE.BANRISUL) {
          this.exibirToast('Favor inserir a rede do equipamento POS', Config.TOAST.ERROR);

          return false;
        }
      }

      if (
        this.chamado.tipoIntervencao.codTipoIntervencao == Config.TIPO_INTERVENCAO.REMANEJAMENTO ||
        this.chamado.tipoIntervencao.codTipoIntervencao == Config.TIPO_INTERVENCAO.TROCA_VELOHC
      ) {
        if (!_.has(this.chamado.rats[0], 'equipamentoInstalado') || !_.has(this.chamado.rats[0].equipamentoInstalado, 'codEquip') || !this.chamado.rats[0].numSerieInstalada) {
          this.exibirToast('Favor inserir o equipamento POS instalado e a série', Config.TOAST.ERROR);
  
          return false;
        }

        if (this.verificarSeDefeitoExigeTroca()) {
          if (!_.has(this.chamado.rats[0], 'equipamentoRetirado') || !_.has(this.chamado.rats[0].equipamentoRetirado, 'codEquip') || !this.chamado.rats[0].numSerieRetirada) {
            this.exibirToast('Favor inserir o equipamento POS retirado', Config.TOAST.ERROR);
    
            return false;
          }
        }
  
        if (!_.has(this.chamado.rats[0], 'tipoComunicacao') || !_.has(this.chamado.rats[0].tipoComunicacao, 'codTipoComunicacao')) {
          this.exibirToast('Favor inserir o tipo de comunicação do POS', Config.TOAST.ERROR);
  
          return false;
        }
  
        if (!this.chamado.rats[0].rede && this.chamado.cliente.codCliente == Config.CLIENTE.BANRISUL) {
          this.exibirToast('Favor inserir a rede do equipamento POS', Config.TOAST.ERROR);
  
          return false;
        }
      }
  
      if (
        this.chamado.tipoIntervencao.codTipoIntervencao == Config.TIPO_INTERVENCAO.ATUALIZACAO ||
        this.chamado.tipoIntervencao.codTipoIntervencao == Config.TIPO_INTERVENCAO.ALTERAÇÃO_ENGENHARIA
      ) {
        if (!_.has(this.chamado.rats[0], 'equipamentoInstalado') || !_.has(this.chamado.rats[0].equipamentoInstalado, 'codEquip') || !this.chamado.rats[0].numSerieInstalada) {
          this.exibirToast('Favor inserir o equipamento POS instalado e a série', Config.TOAST.ERROR);
  
          return false;
        }
  
        if (!_.has(this.chamado.rats[0], 'tipoComunicacao') || !_.has(this.chamado.rats[0].tipoComunicacao, 'codTipoComunicacao')) {
          this.exibirToast('Favor inserir o tipo de comunicação do POS', Config.TOAST.ERROR);
  
          return false;
        }
  
        if (!this.chamado.rats[0].rede && this.chamado.cliente.codCliente == Config.CLIENTE.BANRISUL) {
          this.exibirToast('Favor inserir a rede do equipamento POS', Config.TOAST.ERROR);
  
          return false;
        }
      }
    }

    return true;
  }

  public salvarStatusServico(statusServico: StatusServico) {
    this.chamado.rats[0].statusServico = statusServico;

    this.chamadoService.atualizarChamado(this.chamado);    
  }

  public salvarDefeitoPOS(defeitoPOS: DefeitoPOS) {
    if (this.chamado.rats.length) {
      this.chamado.rats[0].defeitoPOS = defeitoPOS;

      this.chamadoService.atualizarChamado(this.chamado);
    }
  }

  public compararEquipamentosPOS(e1: EquipamentoPOS, e2: EquipamentoPOS): boolean {
    return e1 && e2 ? e1.codEquip == e2.codEquip : e1 == e2;
  }

  public buscarTiposComunicacao(): Promise<TipoComunicacao[]> {
    return new Promise((resolve, reject) => {
      this.tipoComunicacaoService.buscarTiposComunicacaoStorage().then((tiposCom: TipoComunicacao[]) => { 
        this.tiposComunicacao = tiposCom;

        resolve(tiposCom);
      }).catch(err => {
        reject(err);
      });
    });
  }

  public compararTiposComunicacao(t1: TipoComunicacao, t2: TipoComunicacao): boolean {
    return t1 && t2 ? t1.codTipoComunicacao == t2.codTipoComunicacao : t1 == t2;
  }

  public buscarOperadoras(): Promise<OperadoraTelefonia[]> {
    return new Promise((resolve, reject) => {
      this.operadorasTelefoniaService.buscarOperadorasStorage().then((operadoras: OperadoraTelefonia[]) => { 
        this.operadoras = operadoras;

        resolve(operadoras);
      }).catch(err => {
        reject(err);
      });
    });
  }

  public compararOperadoras(o1: OperadoraTelefonia, o2: OperadoraTelefonia): boolean {
    return o1 && o2 ? o1.codOperadoraTelefonica == o2.codOperadoraTelefonica : o1 == o2;
  }

  public buscarMotivosComunicacao(): Promise<MotivoComunicacao[]> {
    return new Promise((resolve, reject) => {
      this.motivoComunicacaoService.buscarMotivosComunicacaoStorage().then((motivos: MotivoComunicacao[]) => { 
        this.motivosComunicacao = motivos;

        resolve(motivos);
      }).catch(err => {
        reject(err);
      });
    });
  }

  public compararMotivosComunicacao(m1: MotivoComunicacao, m2: MotivoComunicacao): boolean {
    return m1 && m2 ? m1.codMotivoComunicacao == m2.codMotivoComunicacao : m1 == m2;
  }

  public buscarMotivosCancelamento(): Promise<MotivoCancelamento[]> {
    return new Promise((resolve, reject) => {
      this.motivoCancelamentoService.buscarMotivosCancelamentoStorage().then((motivos: MotivoCancelamento[]) => { 
        this.motivosCancelamento = motivos;
        
        resolve(motivos);
      }).catch(err => {
        reject(err);
      });
    });
  }

  public compararMotivosCancelamento(m1: MotivoCancelamento, m2: MotivoCancelamento): boolean {
    return m1 && m2 ? m1.codMotivoCancelamento == m2.codMotivoCancelamento : m1 == m2;
  }

  public buscarStatusServicos(): Promise<StatusServico[]> {
    return new Promise((resolve, reject) => {
      this.statusServicoService.buscarStatusServicosStorage().then((statusServicos: StatusServico[]) => { 
        this.statusServicos = statusServicos.filter((s) => { 
          return (s.codStatusServico == 10 || s.codStatusServico == 5 || s.codStatusServico == 3 || 
                  s.codStatusServico == 2 || s.codStatusServico == 16 || s.codStatusServico == 7);
        });
        
        resolve(statusServicos);
      }).catch(err => {
        reject(err);
      });
    });
  }

  public compararStatusServicos(ss1: StatusServico, ss2: StatusServico): boolean {
    return ss1 && ss2 ? ss1.codStatusServico == ss2.codStatusServico : ss1 == ss2;
  }

  public buscarDefeitosPOS(): Promise<DefeitoPOS[]> {
    return new Promise((resolve, reject) => {
      this.defeitoPOSService.buscarDefeitosPOSStorage().then((defeitos: DefeitoPOS[]) => { 
        this.defeitosPOS = defeitos;

        resolve(defeitos);
      }).catch(err => {
        reject(err);
      });
    });
  }

  public compararDefeitosPOS(def1: DefeitoPOS, def2: DefeitoPOS): boolean {
    return def1 && def2 ? def1.codDefeitoPOS == def2.codDefeitoPOS : def1 == def2;
  }

  public verificarDefeitoPOSExiteTrocaEquipamento(codDefeitoPOS: number): boolean {
    this.defeitosPOS.forEach(defeito => {
      if (defeito.codDefeitoPOS == codDefeitoPOS && defeito.exigeTrocaEquipamento) 
        return true;
    });

    return false;
  }

  public verificarSeDefeitoExigeTroca(): boolean {
    var i;
    
    for (i = 0; i < this.defeitosPOS.length; i++) {
      if (this.defeitosPOS[i].codDefeitoPOS === this.chamado.rats[0].defeitoPOS.codDefeitoPOS) {
        if(this.defeitosPOS[i].exigeTrocaEquipamento) {
          return true;
        }
      }
    }
    
    return false;
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
            this.fecharModal()
          }
        }
      ]
    });

    confirmacao.present();
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

  private fecharModal() {
    this.viewCtrl.dismiss();
  }
}