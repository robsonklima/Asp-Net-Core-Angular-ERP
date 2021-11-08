import { Inject, Component, LOCALE_ID, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaTipoService } from 'app/core/services/despesa-tipo.service';
import { DespesaItem, DespesaTipo, DespesaTipoEnum } from 'app/core/types/despesa.types';
import { LocalAtendimento } from 'app/core/types/local-atendimento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
  selector: 'app-despesa-item-dialog',
  templateUrl: './despesa-item-dialog.component.html',
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaItemDialogComponent implements OnInit
{
  despesaItemForm: FormGroup;
  userSession: UserSession;
  tiposDespesa: DespesaTipo[] = [];
  codDespesa: number;
  ordemServico: OrdemServico;
  rat: RelatorioAtendimento;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _despesaTipoSvc: DespesaTipoService,
    private _despesaItemSvc: DespesaItemService,
    private _userSvc: UserService,
    private dialogRef: MatDialogRef<DespesaItemDialogComponent>)
  {
    if (data)
    {
      this.codDespesa = data.codDespesa;
      this.ordemServico = data.ordemServico;
      this.rat = data.rat;
    }

    this.userSession = JSON.parse(this._userSvc.userSession);

    this.obterTiposDespesa();
    this.criarFormularioDespesaItem();
  }

  async ngOnInit() { }

  private async obterTiposDespesa()
  {
    this.tiposDespesa = (await this._despesaTipoSvc.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
  }

  private criarFormularioDespesaItem()
  {
    this.despesaItemForm = this._formBuilder.group({
      step1: this._formBuilder.group({
        codDespesaTipo: [undefined, Validators.required]
      }),
      step2: this._formBuilder.group({
        notaFiscal: [undefined],
        valor: [undefined, Validators.required],
        localInicoDeslocamento: [undefined, Validators.required],
        enderecoDestino: { value: this.ordemServico?.localAtendimento.endereco ?? "Não informado", disabled: true },
        bairroDestino: { value: this.ordemServico?.localAtendimento.bairro ?? "Não informado", disabled: true },
        complementoDestino: { value: this.ordemServico?.localAtendimento.enderecoComplemento ?? "Não informado", disabled: true },
        numeroDestino: { value: this.ordemServico?.localAtendimento.numeroEnd ?? "Não informado", disabled: true },
        cidadeDestino: { value: this.ordemServico?.localAtendimento.cidade?.nomeCidade ?? "Não informado", disabled: true },
        ufDestino: { value: this.ordemServico?.localAtendimento.cidade?.unidadeFederativa?.siglaUF ?? "Não informado", disabled: true },
        paisDestino: { value: this.ordemServico?.localAtendimento.cidade?.unidadeFederativa.pais?.siglaPais ?? "Não informado", disabled: true },
        latitudeDestino: { value: this.ordemServico?.localAtendimento.latitude ?? "Não informado", disabled: true },
        longitudeDestino: { value: this.ordemServico?.localAtendimento.longitude ?? "Não informado", disabled: true },

        enderecoOrigem: [undefined, Validators.required],
        bairroOrigem: [undefined, Validators.required],
        complementoOrigem: [undefined, Validators.required],
        numeroOrigem: [undefined, Validators.required],
        cidadeOrigem: [undefined, Validators.required],
        ufOrigem: [undefined, Validators.required],
        paisOrigem: [undefined, Validators.required],
        latitudeOrigem: [undefined, Validators.required],
        longitudeOrigem: [undefined, Validators.required],
        quilometragem: [undefined, Validators.required],
      }),
      step3: this._formBuilder.group({
        revision: [undefined]
      }),
    });

    this.registerEmitters();
  }

  public isQuilometragem()
  {
    return this.despesaItemForm.value.step1.codDespesaTipo == DespesaTipoEnum.KM;
  }

  registerEmitters()
  {
    (this.despesaItemForm.get('step2') as FormGroup)
      .controls['localInicoDeslocamento']
      .valueChanges.subscribe(() =>
      {
        this.resetFields();

        if ((this.despesaItemForm.get('step2') as FormGroup)
          .controls['localInicoDeslocamento'].value === "residencial")
        {
          (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].setValue(this.rat.tecnico.endereco);
          (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].setValue(this.rat.tecnico.bairro);
          (this.despesaItemForm.get('step2') as FormGroup).controls['complementoOrigem'].setValue(this.rat.tecnico.enderecoComplemento);
          (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(this.rat.tecnico.cidade.nomeCidade);
          (this.despesaItemForm.get('step2') as FormGroup).controls['numeroOrigem'].setValue(this.rat.tecnico.usuario.numero);
          (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].setValue(this.rat.tecnico.cidade.unidadeFederativa.siglaUF);
          (this.despesaItemForm.get('step2') as FormGroup).controls['paisOrigem'].setValue(this.rat.tecnico.cidade.unidadeFederativa.pais.siglaPais);
          (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(this.rat.tecnico.latitude);
          (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(this.rat.tecnico.longitude);
        }
        else
        {

        }
      });
  }

  resetFields()
  {
    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['complementoOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['numeroOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['paisOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['quilometragem'].reset();
  }

  confirmar(): void
  {
    var despesaItem: DespesaItem =
    {
      codDespesa: this.codDespesa,
      numNF: this.despesaItemForm.value.step2.notaFiscal,
      codDespesaTipo: this.despesaItemForm.value.step1.codDespesaTipo,
      despesaValor: this.despesaItemForm.value.step2.valor,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      codDespesaItemAlerta: 1
    };

    this._despesaItemSvc.criar(despesaItem)
      .subscribe(
        () => this.dialogRef.close(true),
        () => this.dialogRef.close(false));
  }

  configuraCamposObrigatorios(): void
  {
    if (!this.isQuilometragem())
    {
      (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['complementoOrigem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['numeroOrigem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['paisOrigem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].disable();

      (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoDestino'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['bairroDestino'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['complementoDestino'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['numeroDestino'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeDestino'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['ufDestino'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['paisDestino'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeDestino'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeDestino'].disable();
    }
    else
    {
      (this.despesaItemForm.get('step2') as FormGroup).controls['valor'].disable();
    }
  }

  obterTipoDespesa()
  {
    return Enumerable.from(this.tiposDespesa)
      .firstOrDefault(i => i.codDespesaTipo == this.despesaItemForm.value.step1.codDespesaTipo)?.nomeTipo;
  }
}