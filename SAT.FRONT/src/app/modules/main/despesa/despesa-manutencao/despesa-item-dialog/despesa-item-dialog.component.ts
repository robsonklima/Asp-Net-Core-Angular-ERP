import { Inject, Component, LOCALE_ID, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaTipoService } from 'app/core/services/despesa-tipo.service';
import { GoogleGeolocationService } from 'app/core/services/google-geolocation.service';
import { DespesaConfiguracaoCombustivel } from 'app/core/types/despesa-configuracao-combustivel.types';
import { Despesa, DespesaItem, DespesaTipo, DespesaTipoEnum } from 'app/core/types/despesa.types';
import { Result } from 'app/core/types/google-geolocation.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';
import moment from 'moment';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

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
  despesa: Despesa;
  despesaConfiguracaoCombustivel: DespesaConfiguracaoCombustivel;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _despesaTipoSvc: DespesaTipoService,
    private _despesaItemSvc: DespesaItemService,
    private _userSvc: UserService,
    private _geolocationService: GoogleGeolocationService,
    private dialogRef: MatDialogRef<DespesaItemDialogComponent>)
  {
    if (data)
    {
      this.codDespesa = data.codDespesa;
      this.ordemServico = data.ordemServico;
      this.rat = data.rat;
      this.despesa = data.despesa;
      this.despesaConfiguracaoCombustivel = data.despesaConfiguracaoCombustivel;
    }

    this.userSession = JSON.parse(this._userSvc.userSession);

    this.obterTiposDespesa();
    this.criarFormularioDespesaItem();
  }

  async ngOnInit() { }

  private async obterTiposDespesa()
  {
    this.tiposDespesa = (await this._despesaTipoSvc.obterPorParametros({ indAtivo: 1 }).toPromise()).items;

    if (Enumerable.from(this.despesa.despesaItens)
      .any(i => i.codDespesaTipo == DespesaTipoEnum.KM))
      this.tiposDespesa = Enumerable.from(this.tiposDespesa)
        .where(i => i.codDespesaTipo != DespesaTipoEnum.KM)
        .toArray();
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
        enderecoDestino: { value: this.ordemServico?.localAtendimento.endereco ?? "Não consta", disabled: true },
        cepDestino: { value: this.ordemServico?.localAtendimento.cep ?? "Não consta", disabled: true },
        bairroDestino: { value: this.ordemServico?.localAtendimento.bairro ?? "Não consta", disabled: true },
        complementoDestino: { value: this.ordemServico?.localAtendimento.enderecoComplemento ?? "Não consta", disabled: true },
        numeroDestino: { value: this.ordemServico?.localAtendimento.numeroEnd ?? "Não consta", disabled: true },
        cidadeDestino: { value: this.ordemServico?.localAtendimento.cidade?.nomeCidade ?? "Não consta", disabled: true },
        ufDestino: { value: this.ordemServico?.localAtendimento.cidade?.unidadeFederativa?.siglaUF ?? "Não consta", disabled: true },
        paisDestino: { value: this.ordemServico?.localAtendimento.cidade?.unidadeFederativa.pais?.siglaPais ?? "Não consta", disabled: true },
        latitudeDestino: { value: this.ordemServico?.localAtendimento.latitude ?? "Não consta", disabled: true },
        longitudeDestino: { value: this.ordemServico?.localAtendimento.longitude ?? "Não consta", disabled: true },

        enderecoOrigem: [undefined, Validators.required],
        cepOrigem: [undefined, Validators.required],
        bairroOrigem: [undefined, Validators.required],
        complementoOrigem: [undefined],
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

  registerEmitters()
  {
    this.onLocalInicoDeslocamentoChanged();
    this.onEnderecoChanged();
  }

  obterTipoDespesa()
  {
    return Enumerable.from(this.tiposDespesa)
      .firstOrDefault(i => i.codDespesaTipo == this.despesaItemForm.value.step1.codDespesaTipo)?.nomeTipo;
  }

  public isQuilometragem()
  {
    return this.despesaItemForm.value.step1.codDespesaTipo == DespesaTipoEnum.KM;
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

  private onLocalInicoDeslocamentoChanged(): void
  {
    (this.despesaItemForm.get('step2') as FormGroup)
      .controls['localInicoDeslocamento']
      .valueChanges.subscribe(() =>
      {
        this.resetFields();

        if ((this.despesaItemForm.get('step2') as FormGroup)
          .controls['localInicoDeslocamento'].value === "residencial")
        {
          this.setOrigemResidencial();
          this.disableOrigin();
        }
        else
        {
          this.enableOrigin();
        }
      });

    (this.despesaItemForm.get('step2') as FormGroup).controls['localInicoDeslocamento'].setValue("residencial");
  }

  private onEnderecoChanged(): void
  {
    (this.despesaItemForm.get('step2') as FormGroup)
      .controls['cepOrigem']
      .valueChanges.pipe(
        debounceTime(700),
        distinctUntilChanged()
      ).subscribe(async () =>
      {
        this.getGoogleLocation();
      });
  }

  async getGoogleLocation()
  {
    if ((this.despesaItemForm.get('step2') as FormGroup)
      .controls['localInicoDeslocamento']
      .value === "residencial")
      return;

    var cep: string = (this.despesaItemForm.get('step2') as FormGroup).controls['cepOrigem'].value?.toString();

    if (!cep) return;

    var googleAddress =
      await this._geolocationService.obterPorEndereco(cep);

    if (googleAddress)
      this.updateGoogleAddress(googleAddress);
  }

  private updateGoogleAddress(googleAddress: Result): void
  {
    var lat = googleAddress.geometry.location.lng;
    var long = googleAddress.geometry.location.lat;
    if (lat == (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].value &&
      long == (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].value) return;

    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(googleAddress.geometry.location.lng);
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(googleAddress.geometry.location.lat);
    this.disableLatLgnOrigin();

    var endereco = Enumerable.from(googleAddress.address_components).where(i => Enumerable.from(i.types).contains("route")).firstOrDefault();
    if (endereco) (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].setValue(endereco.short_name);

    var numero = Enumerable.from(googleAddress.address_components).where(i => Enumerable.from(i.types).contains("street_number")).firstOrDefault();
    if (numero) (this.despesaItemForm.get('step2') as FormGroup).controls['numeroOrigem'].setValue(numero.long_name);

    var bairro = Enumerable.from(googleAddress.address_components).where(i => Enumerable.from(i.types).contains("sublocality_level_1")).firstOrDefault();
    if (bairro) (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].setValue(bairro.long_name);

    var cidade = Enumerable.from(googleAddress.address_components).where(i => Enumerable.from(i.types).contains("administrative_area_level_2")).firstOrDefault();
    if (cidade) (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(cidade.long_name);

    var estado = Enumerable.from(googleAddress.address_components).where(i => Enumerable.from(i.types).contains("administrative_area_level_1")).firstOrDefault();
    if (estado) (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].setValue(estado.short_name);

    var pais = Enumerable.from(googleAddress.address_components).where(i => Enumerable.from(i.types).contains("country")).firstOrDefault();
    if (pais) (this.despesaItemForm.get('step2') as FormGroup).controls['paisOrigem'].setValue(pais.short_name);
  }

  calculaConsumoCombustivel(): number
  {
    return (this.despesaItemForm.value.step2.quilometragem / appConfig.autonomia_veiculo_frota) * this.despesaConfiguracaoCombustivel.precoLitro;
  }

  configuraCamposObrigatorios(): void
  {
    if (!this.isQuilometragem())
    {
      this.disableOrigin();
      this.disableDestination();
      (this.despesaItemForm.get('step2') as FormGroup).controls['quilometragem'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['valor'].enable();
    }
    else
    {
      (this.despesaItemForm.get('step2') as FormGroup).controls['quilometragem'].enable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['valor'].disable();
    }
  }

  validaQuilometragem(): void
  {

  }

  private resetFields(): void
  {
    (this.despesaItemForm.get('step2') as FormGroup).controls['cepOrigem'].reset();
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

  private setOrigemResidencial(): void
  {
    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].setValue(this.rat.tecnico.endereco);
    (this.despesaItemForm.get('step2') as FormGroup).controls['cepOrigem'].setValue(this.rat.tecnico.cep);
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].setValue(this.rat.tecnico.bairro);
    (this.despesaItemForm.get('step2') as FormGroup).controls['complementoOrigem'].setValue(this.rat.tecnico.enderecoComplemento);
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(this.rat.tecnico.cidade.nomeCidade);
    (this.despesaItemForm.get('step2') as FormGroup).controls['numeroOrigem'].setValue(this.rat.tecnico.usuario.numero);
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].setValue(this.rat.tecnico.cidade.unidadeFederativa.siglaUF);
    (this.despesaItemForm.get('step2') as FormGroup).controls['paisOrigem'].setValue(this.rat.tecnico.cidade.unidadeFederativa.pais.siglaPais);
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(this.rat.tecnico.latitude);
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(this.rat.tecnico.longitude);
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].disable();
  }

  private disableOrigin()
  {
    (this.despesaItemForm.get('step2') as FormGroup).controls['cepOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['complementoOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['numeroOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['paisOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].disable();
  }

  private enableOrigin()
  {
    (this.despesaItemForm.get('step2') as FormGroup).controls['cepOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['complementoOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['numeroOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['paisOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].enable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].enable();
  }

  private disableLatLgnOrigin()
  {
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].disable();
  }

  private disableDestination()
  {
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
}