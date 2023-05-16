import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DefeitoPOSService } from 'app/core/services/defeito-pos.service';
import { EquipamentoPOSService } from 'app/core/services/equipamento-pos.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { MotivoCancelamentoService } from 'app/core/services/motivo-cancelamento.service';
import { MotivoComunicacaoService } from 'app/core/services/motivo-comunicacao.service';
import { OperadoraTelefoniaService } from 'app/core/services/operadora-telefonia.service';
import { RedeBanrisulService } from 'app/core/services/rede-banrisul.service';
import { RelatorioAtendimentoPOSService } from 'app/core/services/relatorio-atendimento-pos.service';
import { TipoComunicacaoService } from 'app/core/services/tipo-comunicacao.service';
import { DefeitoPOS, OperadoraTelefonia } from 'app/core/types/chamado.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { MotivoCancelamento } from 'app/core/types/motivo-cancelamento.types';
import { MotivoComunicacao } from 'app/core/types/motivo-comunicacao.types';
import { RedeBanrisul } from 'app/core/types/rede-banrisul.types';
import { RelatorioAtendimentoPOS } from 'app/core/types/relatorio-atendimento-pos.types';
import { TipoComunicacao } from 'app/core/types/tipo-comunicacao.types';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-relatorio-atendimento-pos-form',
  templateUrl: './relatorio-atendimento-pos-form.component.html'
})
export class RelatorioAtendimentoPosFormComponent implements OnInit {
  @Input() codRAT: number;
  rat: RelatorioAtendimentoPOS;
  redes: RedeBanrisul[] = [];
  motivosCancelamento: MotivoCancelamento[] = [];
  tiposComunicacao: TipoComunicacao[] = [];
  motivosComunicacao: MotivoComunicacao[] = [];
  operadoras: OperadoraTelefonia[] = [];
  defeitos: DefeitoPOS[] = [];
  loading: boolean = true;
  isAddMode: boolean;
  form: FormGroup;
  equipamentos: Equipamento[] = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    private _equipamentoService: EquipamentoService,
    private _redeBanrisulService: RedeBanrisulService,
    private _tipoComunicacaoService: TipoComunicacaoService,
    private _operadoraTelefoniaService: OperadoraTelefoniaService,
    private _motivoComunicacaoService: MotivoComunicacaoService,
    private _motivoCancelamentoService: MotivoCancelamentoService,
    private _relatorioAtendimentoPOSService: RelatorioAtendimentoPOSService,
    private _equipamentoPOSService: EquipamentoPOSService,
    private _defeitoPOSService: DefeitoPOSService,
    private _snack: CustomSnackbarService,
    private _formBuilder: FormBuilder,
  ) { }

  async ngOnInit() {
    this.inicializarForm();
    this.registrarEmitters();

    if (this.codRAT) {
      await this.obterRATPOS();
      this.form.patchValue(this.rat);
    }

    this.isAddMode = !this.rat?.codRATbanrisul;
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

  private async obterEquipamentos() {
    this.equipamentos = (await this._equipamentoService
        .obterPorParametros({ sortActive: 'NomeEquip',sortDirection: 'asc' })
        .toPromise()).items.filter(e => e.nomeEquip.includes('POS') || e.nomeEquip.includes('PIN'));
  }

  private async obterRedes() {
    this.redes = (await this._redeBanrisulService
        .obterPorParametros({ sortActive: 'Rede',sortDirection: 'asc' })
        .toPromise()).items;
  }

  private async obterTiposDeComunicacao() {
    this.tiposComunicacao = (await this._tipoComunicacaoService
        .obterPorParametros({ sortActive: 'Tipo',sortDirection: 'asc' })
        .toPromise()).items;
  }

  private async obterOperadoras() {
    this.operadoras = (await this._operadoraTelefoniaService
        .obterPorParametros({ sortActive: 'NomeOperadoraTelefonia',sortDirection: 'asc' })
        .toPromise()).items;
  }

  private async obterMotivosComunicacao() {
    this.motivosComunicacao = (await this._motivoComunicacaoService
      .obterPorParametros({ sortActive: 'Motivo',sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async obterMotivosCancelamento() {
    this.motivosCancelamento = (await this._motivoCancelamentoService
      .obterPorParametros({ sortActive: 'Motivo',sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async obterDefeitos() {
    this.defeitos = (await this._defeitoPOSService
      .obterPorParametros({ sortActive: 'NomeDefeitoPOS',sortDirection: 'asc' })
      .toPromise()).items;
  }

  private validarDados() {
    
  }

  salvar() {
    const form: any = this.form.getRawValue();
    let obj = { ...this.rat, ...form, ...{ codRat: this.codRAT } };

    if (this.isAddMode) {
      this._relatorioAtendimentoPOSService.criar(obj).subscribe(() => {
        this._snack.exibirToast("Registro criado com sucesso!", "success");
      }, () => this._snack.exibirToast("Erro ao criar o registro", "error"))
    } else {
      this._relatorioAtendimentoPOSService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      }, () => this._snack.exibirToast("Erro ao atualizar o registro", "error"))
    }
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
