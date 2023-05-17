import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DefeitoPOSService } from 'app/core/services/defeito-pos.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { EquipamentoPOSService } from 'app/core/services/equipamento-pos.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { MotivoCancelamentoService } from 'app/core/services/motivo-cancelamento.service';
import { MotivoComunicacaoService } from 'app/core/services/motivo-comunicacao.service';
import { OperadoraTelefoniaService } from 'app/core/services/operadora-telefonia.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RedeBanrisulService } from 'app/core/services/rede-banrisul.service';
import { RelatorioAtendimentoPOSService } from 'app/core/services/relatorio-atendimento-pos.service';
import { TipoComunicacaoService } from 'app/core/services/tipo-comunicacao.service';
import { DefeitoPOS, OperadoraTelefonia } from 'app/core/types/chamado.types';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { EquipamentoPOS } from 'app/core/types/equipamento-pos.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { mensagensConst, toastTypesConst } from 'app/core/types/generic.types';
import { MotivoCancelamento } from 'app/core/types/motivo-cancelamento.types';
import { MotivoComunicacao } from 'app/core/types/motivo-comunicacao.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RedeBanrisul } from 'app/core/types/rede-banrisul.types';
import { RelatorioAtendimentoPOS } from 'app/core/types/relatorio-atendimento-pos.types';
import { statusConst } from 'app/core/types/status-types';
import { TipoComunicacao } from 'app/core/types/tipo-comunicacao.types';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-relatorio-atendimento-pos-form',
  templateUrl: './relatorio-atendimento-pos-form.component.html'
})
export class RelatorioAtendimentoPosFormComponent implements OnInit {
  @Input() codRAT: number;
  @Input() codOS: number;
  rat: RelatorioAtendimentoPOS;
  os: OrdemServico;
  form: FormGroup;
  redes: RedeBanrisul[] = [];
  motivosCancelamento: MotivoCancelamento[] = [];
  tiposComunicacao: TipoComunicacao[] = [];
  motivosComunicacao: MotivoComunicacao[] = [];
  operadoras: OperadoraTelefonia[] = [];
  defeitos: DefeitoPOS[] = [];
  loading: boolean = true;
  isAddMode: boolean;
  equipamentos: Equipamento[] = [];
  equipInst: EquipamentoContrato;
  equipPOSInst: EquipamentoPOS;
  equipRet: EquipamentoContrato;
  equipPOSRet: EquipamentoPOS;
  userSession: UserSession;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _equipamentoService: EquipamentoService,
    private _redeBanrisulService: RedeBanrisulService,
    private _tipoComunicacaoService: TipoComunicacaoService,
    private _operadoraTelefoniaService: OperadoraTelefoniaService,
    private _equipamentoContratoService: EquipamentoContratoService,
    private _motivoComunicacaoService: MotivoComunicacaoService,
    private _motivoCancelamentoService: MotivoCancelamentoService,
    private _equipamentoPOSService: EquipamentoPOSService,
    private _relatorioAtendimentoPOSService: RelatorioAtendimentoPOSService,
    private _ordemServicoService: OrdemServicoService,
    private _defeitoPOSService: DefeitoPOSService,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
    private _formBuilder: FormBuilder,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();
    this.registrarEmitters();

    if (this.codRAT) {
      await this.obterRATPOS();

      this.form.patchValue(this.rat);
    }

    this.isAddMode = !this.rat?.codRATbanrisul;
    await this.obterOS();


    this.form.controls['codEquipInst'].setValue(this.os.codEquip);
    this.form.controls['codEquipRet'].setValue(this.os.codEquip);
    this.form.controls['numSerieRet'].setValue(this.os?.equipamentoContrato?.numSerie);

    if (this.os.codTipoIntervencao == TipoIntervencaoEnum.DESINSTALACAO)
      this.form.controls['numSerieRet'].setValidators([Validators.required]);
    else if (this.os.codTipoIntervencao == TipoIntervencaoEnum.CORRETIVA)
      this.form.controls['numSerieRet'].clearValidators();
    else if (this.os.codTipoIntervencao == TipoIntervencaoEnum.INSTALACAO) {
      this.form.controls['numSerieRet'].disable();
      this.form.controls['numSerieInst'].setValidators([Validators.required]);
    }

    await this.obterEquipamentos();
    await this.obterRedes();
    await this.obterTiposDeComunicacao();
    await this.obterOperadoras();
    await this.obterMotivosComunicacao();
    await this.obterDefeitos();
    await this.obterMotivosCancelamento();

    this.loading = false;
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codStatusServico: [undefined],
      codDefeitoPOS: [undefined],
      numSerieRet: [undefined],
      numSerieInst: [undefined],
      codEquipRet: [undefined],
      codEquipInst: [undefined],
      rede: [undefined],
      codTipoComunicacao: [undefined],
      codMotivoComunicacao: [undefined],
      codMotivoCancelamento: [undefined],
      numeroChipInstalado: [undefined],
      numeroChipRetirado: [undefined],
      codOperadoraTelefoniaChipRetirado: [undefined],
      codOperadoraTelefoniaChipInstalado: [undefined],
      indSmartphone: [0],
      obsMotivoCancelamento: [undefined],
      obsMotivoComunicacao: [undefined]
    })
  }

  private registrarEmitters() {
    this.form.controls['numSerieRet'].valueChanges.pipe(
      filter(t => !!t || t != ''),
      tap(() => { }),
      debounceTime(700),
      map(async serie => {
        this.equipRet = (await this._equipamentoContratoService
          .obterPorParametros({ numSerie: serie, codClientes: this.os.codCliente.toString() })
          .toPromise()).items?.shift();

        this.equipPOSRet = (await this._equipamentoPOSService
          .obterPorParametros({ numSerie: serie })
          .toPromise()).items?.shift();
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['numSerieInst'].valueChanges.pipe(
      filter(t => !!t || t != ''),
      tap(() => { }),
      debounceTime(700),
      map(async serie => {
        this.equipInst = (await this._equipamentoContratoService
          .obterPorParametros({ numSerie: serie, codClientes: this.os.codCliente.toString() })
          .toPromise()).items?.shift();

        this.equipPOSInst = (await this._equipamentoPOSService
          .obterPorParametros({ numSerie: serie })
          .toPromise()).items?.shift();
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });
  }

  private async obterRATPOS() {
    this.rat = (await this._relatorioAtendimentoPOSService
      .obterPorParametros({
        sortActive: 'CodRAT',
        sortDirection: 'desc',
        codRAT: this.codRAT
      })
      .toPromise()).items
      .filter(r => r.codRat == this.codRAT)
      .shift();
  }

  private async obterOS() {
    this.os = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
  }

  private async obterEquipamentos() {
    this.equipamentos = (await this._equipamentoService
      .obterPorParametros({ sortActive: 'NomeEquip', sortDirection: 'asc' })
      .toPromise()).items.filter(e => e.nomeEquip.includes('POS') || e.nomeEquip.includes('PIN'));
  }

  private async obterRedes() {
    this.redes = (await this._redeBanrisulService
      .obterPorParametros({ sortActive: 'Rede', sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async obterTiposDeComunicacao() {
    this.tiposComunicacao = (await this._tipoComunicacaoService
      .obterPorParametros({ sortActive: 'Tipo', sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async obterOperadoras() {
    this.operadoras = (await this._operadoraTelefoniaService
      .obterPorParametros({ sortActive: 'NomeOperadoraTelefonia', sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async obterMotivosComunicacao() {
    this.motivosComunicacao = (await this._motivoComunicacaoService
      .obterPorParametros({ sortActive: 'Motivo', sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async obterMotivosCancelamento() {
    this.motivosCancelamento = (await this._motivoCancelamentoService
      .obterPorParametros({ sortActive: 'Motivo', sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async obterDefeitos() {
    this.defeitos = (await this._defeitoPOSService
      .obterPorParametros({ sortActive: 'NomeDefeitoPOS', sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async validarDados(): Promise<string> {
    const form: any = this.form.getRawValue();

    if (!form.numSerieRet)
      this.form.controls['codEquipRet'].setValue(undefined);

    if (!form.numSerieInst)
      this.form.controls['codEquipInst'].setValue(undefined);

    if (this.equipInst.codEquip != this.os.codEquip)
      return mensagensConst.MODELO_DIFERENTE;

    if ((form.numSerieRet && !form.numSerieInst) || (!form.numSerieRet && form.numSerieInst))
      return mensagensConst.NUM_SERIE_RET_INST_OBRIGATORIO;

    if (!_.isEqual(this.equipRet, this.os.equipamentoContrato))
      return mensagensConst.EQUIP_RET_DIFERENTE_OS;

    if (!this.equipRet || !this.equipPOSRet)
      this._snack.exibirToast(mensagensConst.NUMERO_SERIE_RETIRADO_N_ENCONTRADO, toastTypesConst.ERROR);

    if (!this.equipInst || !this.equipPOSInst)
      this._snack.exibirToast(mensagensConst.NUMERO_SERIE_INSTALADO_N_ENCONTRADO, toastTypesConst.ERROR);

    return undefined;
  }

  async salvar() {
    const msgErro = await this.validarDados();

    if (msgErro)
      return this._snack.exibirToast(msgErro, toastTypesConst.ERROR);

    const form: any = this.form.getRawValue();

    let rat: RelatorioAtendimentoPOS = {
      ...this.rat,
      ...form,
      ...{ codRat: this.codRAT }
    };

    if (form.numSerieRet && this.equipRet) {
      this.equipRet.indAtivo = statusConst.INATIVO;
      await this._equipamentoContratoService.atualizar(this.equipRet).toPromise();
    }

    if (form.numSerieInst) {
      if (this.equipInst) {
        this.equipInst.indAtivo = statusConst.ATIVO;

        await this._equipamentoContratoService.atualizar(this.equipInst).toPromise();
      } else {
        const equipInstAux: EquipamentoContrato = {
          indInstalacao: 1,
          indReceita: 0,
          indAtivo: statusConst.ATIVO,
          codFilial: this.os.codFilial || this.os.localAtendimento.codFilial || this.os.equipamentoContrato.codFilial,
          codPosto: this.os.codPosto || this.os.equipamentoContrato.codPosto,
          codRegiao: this.os.codRegiao || this.os.equipamentoContrato.codRegiao,
          codAutorizada: this.os.codAutorizada || this.os.equipamentoContrato.codEquipContrato,
          codCliente: this.os.codCliente,
          numSerie: form.numSerieInst,
          codGrupoEquip: this.os.equipamento.codGrupoEquip || this.os.equipamentoContrato.codGrupoEquip,
          codTipoEquip: this.os.equipamento.codTipoEquip || this.os.equipamentoContrato.codTipoEquip,
          codEquip: this.os.equipamento.codEquip || this.os.equipamentoContrato.codEquip,
          codSLA: this.os.equipamentoContrato.codSLA,
          codContrato: this.os.equipamentoContrato.codContrato,
        }

        const equipInstCriado = await this._equipamentoContratoService.criar(equipInstAux).toPromise();

        this._ordemServicoService.atualizar({
          ...this.os,
          ...{
            codEquipContrato: equipInstCriado.codEquipContrato,
            codAutorizada: equipInstCriado.codAutorizada,
            codTipoEquip: equipInstCriado.codTipoEquip,
            codGrupoEquip: equipInstCriado.codGrupoEquip,
            codEquip: equipInstCriado.codEquip,
            codContrato: equipInstCriado.codContrato,
            codRegiao: equipInstCriado.codRegiao
          }
        }).toPromise();
      }
    }

    if (this.isAddMode)
      this._relatorioAtendimentoPOSService
        .criar(rat)
        .subscribe(() => {
          this._snack.exibirToast(mensagensConst.SUCESSO_AO_CRIAR, toastTypesConst.SUCCESS);
        },
          () => this._snack.exibirToast(mensagensConst.ERRO_AO_CRIAR, toastTypesConst.ERROR)
        );
    else
      this._relatorioAtendimentoPOSService
        .atualizar(rat)
        .subscribe(() => {
          this._snack.exibirToast(mensagensConst.SUCESSO_AO_ATUALIZAR, toastTypesConst.SUCCESS);
        },
          () => this._snack.exibirToast(mensagensConst.ERRO_AO_ATUALIZAR, toastTypesConst.ERROR)
        );
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
