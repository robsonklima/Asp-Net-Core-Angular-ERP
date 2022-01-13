import { Inject, Component, LOCALE_ID, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaTipoService } from 'app/core/services/despesa-tipo.service';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { DespesaConfiguracaoCombustivel } from 'app/core/types/despesa-configuracao-combustivel.types';
import { Despesa, DespesaConfiguracao, DespesaItem, DespesaItemAlertaData, DespesaItemAlertaEnum, DespesaTipo, DespesaTipoEnum } from 'app/core/types/despesa.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';
import moment from 'moment';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import 'leaflet';
import 'leaflet-routing-machine';
import { CidadeService } from 'app/core/services/cidade.service';
import { statusConst } from 'app/core/types/status-types';
import { Geolocalizacao, GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';
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
  despesaConfiguracao: DespesaConfiguracao;
  despesaItemAlerta: DespesaItemAlertaData;
  mapsPlaceholder: any = [];
  @ViewChild('map') private map: any;
  isResidencial: boolean;
  isValidating: boolean = false;
  kmPrevisto: number;
  tentativaKm: number = 0;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _despesaTipoSvc: DespesaTipoService,
    private _despesaItemSvc: DespesaItemService,
    private _userSvc: UserService,
    private _cidadeSvc: CidadeService,
    private _geolocationService: GeolocalizacaoService,
    private dialogRef: MatDialogRef<DespesaItemDialogComponent>)
  {
    if (data)
    {
      this.codDespesa = data.codDespesa;
      this.ordemServico = data.ordemServico;
      this.rat = data.rat;
      this.despesa = data.despesa;
      this.despesaConfiguracaoCombustivel = data.despesaConfiguracaoCombustivel;
      this.despesaConfiguracao = data.despesaConfiguracao;
      this.despesaItemAlerta = data.despesaItemAlerta;
    }

    this.userSession = JSON.parse(this._userSvc.userSession);

    this.obterTiposDespesa();
    this.criarFormularioDespesaItem();
  }

  async ngOnInit() { }

  private async obterTiposDespesa()
  {
    this.tiposDespesa = (await this._despesaTipoSvc.obterPorParametros({ indAtivo: statusConst.ATIVO }).toPromise()).items;

    if (Enumerable.from(this.despesa.despesaItens)
      .where(i => i.codDespesaTipo == DespesaTipoEnum.KM).count() == 2)
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
        codDespesaItemAlerta: [DespesaItemAlertaEnum.Indefinido],
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

  public isRefeicao()
  {
    return this.despesaItemForm.value.step1.codDespesaTipo == DespesaTipoEnum.REFEICAO;
  }

  async confirmar()
  {
    var despesaItem: DespesaItem =
      this.isQuilometragem() ? await this.criaDespesaItemQuilometragem() : await this.criaDespesaItemOutros();

    this._despesaItemSvc.criar(despesaItem)
      .subscribe(
        () => this.dialogRef.close(true),
        () => this.dialogRef.close(false));
  }

  async criaDespesaItemQuilometragem()
  {
    var codCidadeOrigem =
      await this.obterCidadePeloNome(this.despesaItemForm.value.step2.cidadeOrigem);

    var despesaItem: DespesaItem =
    {
      codDespesa: this.codDespesa,
      numNF: this.despesaItemForm.value.step2.notaFiscal,
      codDespesaTipo: this.despesaItemForm.value.step1.codDespesaTipo,
      despesaValor: this.calculaConsumoCombustivel(),
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      codDespesaItemAlerta: this.despesaItemForm.value.step2.codDespesaItemAlerta != 0 ? this.despesaItemForm.value.step2.codDespesaItemAlerta : null,
      codDespesaConfiguracao: this.despesaConfiguracao.codDespesaConfiguracao,
      enderecoOrigem: this.despesaItemForm.value.step2.enderecoOrigem,
      numOrigem: this.despesaItemForm.value.step2.numeroOrigem,
      bairroOrigem: this.despesaItemForm.value.step2.bairroOrigem,
      codCidadeOrigem: codCidadeOrigem,
      indResidenciaOrigem: +this.isResidencial,
      indHotelOrigem: 0,
      enderecoDestino: this.despesaItemForm.value.step2.enderecoDestino,
      numDestino: this.despesaItemForm.value.step2.numeroDestino,
      bairroDestino: this.despesaItemForm.value.step2.bairroDestino,
      codCidadeDestino: this.ordemServico?.localAtendimento?.cidade?.codCidade,
      sequenciaDespesaKm: 0,
      indResidenciaDestino: +(codCidadeOrigem == this.rat.tecnico.codCidade),
      indHotelDestino: 0,
      kmPrevisto: this.kmPrevisto,
      kmPercorrido: this.despesaItemForm.value.step2.quilometragem,
      tentativaKM: this.tentativaKm.toString(),
      obs: this.despesaItemForm.value.step3.obs,
      latitudeHotel: !this.isResidencial ? this.despesaItemForm.value.step2.latitudeOrigem : null,
      longitudeHotel: !this.isResidencial ? this.despesaItemForm.value.step2.latitudeDestino : null,
      indAtivo: statusConst.ATIVO
    };

    return despesaItem;
  }

  async obterCidadePeloNome(nomeCidade: string)
  {
    return (await this._cidadeSvc.obterPorParametros
      ({
        filter: nomeCidade,
        indAtivo: statusConst.ATIVO,
        pageSize: 5
      }).toPromise()).items[0]?.codCidade;
  }

  criaDespesaItemOutros(): DespesaItem
  {
    var despesaItem: DespesaItem =
    {
      codDespesa: this.codDespesa,
      numNF: this.despesaItemForm.value.step2.notaFiscal,
      codDespesaTipo: this.despesaItemForm.value.step1.codDespesaTipo,
      despesaValor: this.despesaItemForm.value.step2.valor,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      codDespesaItemAlerta: this.despesaItemForm.value.step2.codDespesaItemAlerta,
      codDespesaConfiguracao: this.despesaConfiguracao.codDespesaConfiguracao,
      obs: this.despesaItemForm.value.step3.obs,
      indAtivo: statusConst.ATIVO
    };

    return despesaItem;
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
          this.isResidencial = false;
      });

    (this.despesaItemForm.get('step2') as FormGroup)
      .controls['localInicoDeslocamento'].setValue("residencial");
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

    var cep: string =
      (this.despesaItemForm.get('step2') as FormGroup)
        .controls['cepOrigem'].value?.toString();

    if (!cep) return;

    var address =
      (await this._geolocationService.obterPorParametros({ enderecoCep: cep.trim(), geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE }).toPromise());

    if (address)
      this.updateGoogleAddress(address);
  }

  private updateGoogleAddress(address: Geolocalizacao): void
  {
    var lat = address.latitude;
    var long = address.longitude;

    if (lat == (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].value &&
      long == (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].value) return;

    (this.despesaItemForm.get('step2') as FormGroup)
      .controls['longitudeOrigem']
      .setValue(address.longitude);

    (this.despesaItemForm.get('step2') as FormGroup)
      .controls['latitudeOrigem']
      .setValue(address.latitude);

    if (address.endereco)
      (this.despesaItemForm.get('step2') as FormGroup)
        .controls['enderecoOrigem']
        .setValue(address.endereco);

    if (address.numero)
      (this.despesaItemForm.get('step2') as FormGroup)
        .controls['numeroOrigem']
        .setValue(address.numero);

    if (address.bairro) (this.despesaItemForm.get('step2') as FormGroup)
      .controls['bairroOrigem']
      .setValue(address.bairro);

    if (address.cidade)
      (this.despesaItemForm.get('step2') as FormGroup)
        .controls['cidadeOrigem']
        .setValue(address.cidade);

    if (address.estado)
      (this.despesaItemForm.get('step2') as FormGroup)
        .controls['ufOrigem']
        .setValue(address.estado);

    if (address.pais)
      (this.despesaItemForm.get('step2') as FormGroup)
        .controls['paisOrigem']
        .setValue(address.pais);
  }

  calculaConsumoCombustivel(): number
  {
    return (this.despesaItemForm.value.step2.quilometragem / appConfig.autonomia_veiculo_frota) * this.despesaConfiguracaoCombustivel.precoLitro;
  }

  async revisar()
  {
    this.isValidating = true;

    await this.validaCalculoQuilometragem();
    await this.validaDespesaRefeicao();

    this.isValidating = false;
  }

  private async validaCalculoQuilometragem()
  {
    if (!this.isQuilometragem()) return;

    this.kmPrevisto = await this.calculaQuilometragemGoogle();

    // se a quilometragem informada for maior que a calculada e a diferença for maior q 1km
    if (this.despesaItemForm.value.step2.quilometragem > this.kmPrevisto &&
      Math.abs(this.kmPrevisto - this.despesaItemForm.value.step2.quilometragem) > 1)
    {
      // centro a centro
      if (this.despesaItemForm.value.step2.bairroOrigem.toString()?.toLowerCase()?.includes("centro") &&
        this.despesaItemForm.value.step2.bairroDestino.toString()?.toLowerCase()?.includes("centro"))
      {
        (this.despesaItemForm.get('step2') as FormGroup).controls['codDespesaItemAlerta']
          .setValue(DespesaItemAlertaEnum.TecnicoTeveQuilometragemPercorridaMaiorQuePrevistaCalculadaDoCentroAoCentro);
      }
      else 
      {
        (this.despesaItemForm.get('step2') as FormGroup).controls['codDespesaItemAlerta']
          .setValue(DespesaItemAlertaEnum.TecnicoTeveUmaQuilometragemPercorridaMaiorQuePrevista);
      }
      this.tentativaKm++;
      this.despesaInvalida();
    }
    else this.despesaValida();
  }

  private validaDespesaRefeicao()
  {
    if (!this.isRefeicao()) return;

    // se o valor da refeicao informada for maior que o limite da configuracao
    if (this.despesaItemForm.value.step2.valor > this.despesaConfiguracao.valorRefeicaoLimiteTecnico)
    {
      (this.despesaItemForm.get('step2') as FormGroup).controls['codDespesaItemAlerta']
        .setValue(DespesaItemAlertaEnum.TecnicoTeveAlgumaRefeicaoMaiorQueLimiteEspecificado);
      this.despesaInvalida();
    }
    else this.despesaValida();
  }

  private despesaInvalida()
  {
    (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].enable();
  }

  private despesaValida()
  {
    (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].reset();
    (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].disable();
  }

  obterMensagemAlerta()
  {
    var codDespesaItemAlerta =
      this.despesaItemForm.value.step2.codDespesaItemAlerta;

    if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveAlgumaRefeicaoMaiorQueLimiteEspecificado)
      return "Valor da refeição maior que o limite especificado. Por favor, justifique abaixo."
    else if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveQuilometragemPercorridaMaiorQuePrevistaCalculadaDoCentroAoCentro)
      return "Quilometragem maior que a prevista. Por favor, justifique abaixo."
    else if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveUmaQuilometragemPercorridaMaiorQuePrevista)
      return "Quilometragem maior que a prevista. Por favor, justifique abaixo."
  }

  //   async calculaQuilometragemLeaflet(): Promise<number>
  //   {
  //     var origem = L.latLng((this.despesaItemForm.get('step2') as any).controls['latitudeOrigem'].value,
  //       (this.despesaItemForm.get('step2') as any).controls['longitudeOrigem'].value);
  // 
  //     var destino = L.latLng((this.despesaItemForm.get('step2') as any).controls['latitudeDestino'].value,
  //       (this.despesaItemForm.get('step2') as any).controls['longitudeDestino'].value);
  // 
  //     if (this.map == null)
  //       this.map = L.map('map').setView(origem, destino, 1);
  // 
  //     var routing = L.Routing.control({
  //       waypoints: [origem, destino],
  //       routeWhileDragging: true,
  //       show: false,
  //       createMarker: function (p1, p2) { }
  //     })
  // 
  //     return new Promise(resolve => routing.on('routesfound', (e) =>
  //     {
  //       var routes = e.routes;
  //       var summary = routes[0].summary;
  //       resolve(summary.totalDistance / 1000);
  //     }).addTo(this.map));
  //   }

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
      longitudeOrigem: oLong,
      geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.NOMINATIM
    }).subscribe(result =>
    {
      resolve(result.distancia / 1000);
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
