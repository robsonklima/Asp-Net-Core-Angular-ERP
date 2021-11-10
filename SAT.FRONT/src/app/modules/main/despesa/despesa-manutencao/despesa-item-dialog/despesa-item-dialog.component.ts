import { Inject, Component, LOCALE_ID, OnInit, ViewChild } from '@angular/core';
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
import 'leaflet';
import 'leaflet-routing-machine';
declare var L: any;


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
  mapsPlaceholder: any = [];
  @ViewChild('map') private map: any;
  isResidencial: boolean;
  kmInvalid: boolean = false;
  isValidating: boolean = false;

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

        enderecoDestino: [this.ordemServico?.localAtendimento?.endereco, Validators.required],
        cepDestino: [this.ordemServico?.localAtendimento?.cep, Validators.required],
        bairroDestino: [this.ordemServico?.localAtendimento?.bairro, Validators.required],
        complementoDestino: [this.ordemServico?.localAtendimento?.enderecoComplemento],
        numeroDestino: [this.ordemServico?.localAtendimento?.numeroEnd],
        cidadeDestino: [this.ordemServico?.localAtendimento?.cidade?.nomeCidade, Validators.required],
        ufDestino: [this.ordemServico?.localAtendimento?.cidade?.unidadeFederativa?.siglaUF, Validators.required],
        paisDestino: [this.ordemServico?.localAtendimento?.cidade?.unidadeFederativa?.pais?.siglaPais, Validators.required],
        latitudeDestino: [this.ordemServico?.localAtendimento?.latitude, Validators.required],
        longitudeDestino: [this.ordemServico?.localAtendimento?.longitude, Validators.required],

        enderecoOrigem: [undefined, Validators.required],
        cepOrigem: [undefined, Validators.required],
        bairroOrigem: [undefined, Validators.required],
        complementoOrigem: [undefined],
        numeroOrigem: [undefined],
        cidadeOrigem: [undefined, Validators.required],
        ufOrigem: [undefined, Validators.required],
        paisOrigem: [undefined, Validators.required],
        latitudeOrigem: [undefined, Validators.required],
        longitudeOrigem: [undefined, Validators.required],

        quilometragem: [undefined, Validators.required],
      }),
      step3: this._formBuilder.group({
        revision: [undefined],
        obs: [undefined, Validators.required],
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
          this.isResidencial = true;
        }
        else
        {
          this.isResidencial = false;
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

  async revisar()
  {
    this.isValidating = true;

    await this.validaQuilometragem();

    this.isValidating = false;
  }

  async validaQuilometragem()
  {
    var quilometragemLeaflet = await this.calculaQuilometragemLeaflet();
    // var quilometragemGoogle = await this.calculaQuilometragemGoogle();

    // se a quilometragem informada for maior que a calculada e a diferença for maior q 1km
    if (this.despesaItemForm.value.step2.quilometragem > quilometragemLeaflet &&
      Math.abs(quilometragemLeaflet - this.despesaItemForm.value.step2.quilometragem) > 1)
    {
      this.kmInvalid = true;
      (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].enable();
    }
    else
    {
      this.kmInvalid = false;
      (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].reset();
      (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].disable();
    }
  }

  async calculaQuilometragemLeaflet(): Promise<number>
  {
    var origem = L.latLng((this.despesaItemForm.get('step2') as any).controls['latitudeOrigem'].value,
      (this.despesaItemForm.get('step2') as any).controls['longitudeOrigem'].value);

    var destino = L.latLng((this.despesaItemForm.get('step2') as any).controls['latitudeDestino'].value,
      (this.despesaItemForm.get('step2') as any).controls['longitudeDestino'].value);

    if (this.map == null)
      this.map = L.map('map').setView(origem, destino, 1);

    var routing = L.Routing.control({
      waypoints: [origem, destino],
      routeWhileDragging: true,
      show: false,
      createMarker: function (p1, p2) { }
    })

    return new Promise(resolve => routing.on('routesfound', function (e)
    {
      var routes = e.routes;
      var summary = routes[0].summary;
      // alert('A distância é de ' + summary.totalDistance / 1000 + 'km e o tempo é ' + 
      // Math.round(summary.totalTime % 3600 / 60) + ' minutos');
      resolve(summary.totalDistance / 1000);
    }).addTo(this.map));
  }


  async calculaQuilometragemGoogle(): Promise<number>
  {
    var oLat = (this.despesaItemForm.get('step2') as any).controls['latitudeOrigem'].value;
    var oLong = (this.despesaItemForm.get('step2') as any).controls['longitudeOrigem'].value;

    var dLat = (this.despesaItemForm.get('step2') as any).controls['latitudeDestino'].value;
    var dLong = (this.despesaItemForm.get('step2') as any).controls['longitudeDestino'].value;

    return new Promise(resolve => this._geolocationService.obterDistancia({
      latitudeDestino: dLat,
      longitudeDestino: dLong,
      latitudeOrigem: oLat,
      longitudeOrigem: oLong
    }).subscribe(result =>
    {
      resolve(result.rows[0]?.elements[0]?.distance?.value / 1000);
    }));
  }

  configuraCamposObrigatorios(): void
  {
    if (!this.isQuilometragem())
    {
      (this.despesaItemForm.get('step2') as FormGroup).controls['quilometragem'].disable();
      (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].disable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['valor'].enable();
    }
    else
    {
      (this.despesaItemForm.get('step2') as FormGroup).controls['quilometragem'].enable();
      (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].enable();
      (this.despesaItemForm.get('step2') as FormGroup).controls['valor'].disable();
    }
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
  }
}