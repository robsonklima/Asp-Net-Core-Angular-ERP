import { Component, Inject, LOCALE_ID, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { CidadeService } from 'app/core/services/cidade.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaTipoService } from 'app/core/services/despesa-tipo.service';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { DespesaConfiguracaoCombustivel } from 'app/core/types/despesa-configuracao-combustivel.types';
import {
  Despesa, DespesaConfiguracao, DespesaItem, DespesaItemAlertaData, DespesaItemAlertaEnum,
  DespesaTipo, DespesaTipoEnum, DespesaTipoParameters
} from 'app/core/types/despesa.types';
import { Geolocalizacao, GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';
import _ from 'lodash';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, filter, map, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-despesa-item-dialog',
  templateUrl: './despesa-item-dialog.component.html',
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaItemDialogComponent implements OnInit {
  form: FormGroup;
  userSession: UserSession;
  tiposDespesa: DespesaTipo[] = [];
  codDespesa: number;
  ordemServico: OrdemServico;
  rat: RelatorioAtendimento;
  rats: RelatorioAtendimento[] = [];
  despesa: Despesa;
  despesaConfiguracaoCombustivel: DespesaConfiguracaoCombustivel;
  despesaConfiguracao: DespesaConfiguracao;
  despesaItemAlerta: DespesaItemAlertaData;
  mapsPlaceholder: any = [];
  isResidenciaOrigem: boolean = false;
  isHotelOrigem: boolean = false;
  isResidenciaDestino: boolean = false;
  isHotelDestino: boolean = false;
  isValidating: boolean = false;
  kmPrevisto: number;
  tentativaKm: number = 0;
  loading: boolean;
  flagOrigens: boolean;
  flagDestinos: boolean;
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _despesaTipoSvc: DespesaTipoService,
    private _despesaItemSvc: DespesaItemService,
    private _userSvc: UserService,
    private _cidadeSvc: CidadeService,
    private _geolocationService: GeolocalizacaoService,
    private _ordemServicoSvc: OrdemServicoService,
    private _snack: CustomSnackbarService,
    private dialogRef: MatDialogRef<DespesaItemDialogComponent>,
  ) {
    if (data) {
      this.codDespesa = data.codDespesa;
      this.ordemServico = data.ordemServico;
      this.rat = data.rat;
      this.despesa = data.despesa;
      this.despesaConfiguracaoCombustivel = data.despesaConfiguracaoCombustivel;
      this.despesaConfiguracao = data.despesaConfiguracao;
      this.despesaItemAlerta = data.despesaItemAlerta;
      this.rats = data.rats;
    }

    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  async ngOnInit() {
    this.obterTiposDespesa();
    this.criarForm();
    this.registrarEmitters();
    this.flagOrigens = this.mostrarOpcaoResidenciaHotelOrigem();
    this.flagDestinos = this.mostrarOpcaoResidenciaHotelDestino();
  }

  private async obterTiposDespesa() {
    const params: DespesaTipoParameters = { indAtivo: statusConst.ATIVO, sortActive: "NomeTipo", sortDirection: "asc" };
    this.tiposDespesa = (await this._despesaTipoSvc.obterPorParametros(params).toPromise()).items;
    const despesasQuilometragem = this.obterDespesaItensKM();

    if (despesasQuilometragem?.length == 2 || (despesasQuilometragem?.length == 1 && !this.isUltimaRATDoDia())) {
      this.tiposDespesa = Enumerable.from(this.tiposDespesa)
        .where(i => i.codDespesaTipo != DespesaTipoEnum.KM)
        .toArray();
    }
  }

  private criarForm() {
    this.form = this._formBuilder.group({
      step1: this._formBuilder.group({
        codDespesaTipo: [undefined, Validators.required]
      }),
      step2: this._formBuilder.group({
        notaFiscal: [undefined],
        valor: [undefined, Validators.required],
        localInicioDeslocamento: [undefined],
        localDestinoDeslocamento: [undefined],
        codDespesaItemAlerta: [DespesaItemAlertaEnum.Indefinido],
        enderecoDestino: [undefined, Validators.required],
        bairroDestino: [undefined, Validators.required],
        cidadeDestino: [undefined, Validators.required],
        ufDestino: [undefined, Validators.required],
        latitudeDestino: [undefined, Validators.required],
        longitudeDestino: [undefined, Validators.required],
        enderecoOrigem: [undefined, Validators.required],
        bairroOrigem: [undefined, Validators.required],
        cidadeOrigem: [undefined, Validators.required],
        ufOrigem: [undefined, Validators.required],
        latitudeOrigem: [undefined, Validators.required],
        longitudeOrigem: [undefined, Validators.required],
        quilometragem: [undefined, Validators.required],
      }),
      step3: this._formBuilder.group({
        revision: [undefined],
        obs: [undefined, Validators.required],
      }),
    });
  }

  private registrarEmitters() {
    this.localInicioDeslocamentoEmitter();
    this.localDestinoDeslocamentoEmitter();
    this.enderecoOrigemEmitter();
    this.enderecoDestinoEmitter();
  }

  public isQuilometragem() {
    return this.form.value.step1.codDespesaTipo == DespesaTipoEnum.KM;
  }

  public isRefeicao() {
    return this.form.value.step1.codDespesaTipo == DespesaTipoEnum.REFEICAO;
  }

  async obterCidadePeloNome(nomeCidade: string) {
    return (await this._cidadeSvc.obterPorParametros({
      filter: nomeCidade,
      indAtivo: statusConst.ATIVO,
      pageSize: 1
    }).toPromise()).items.shift().codCidade;
  }

  private async validaCalculoQuilometragem() {
    if (!this.isQuilometragem()) return;
    const form = this.form.getRawValue();

    this.kmPrevisto = await this.calculaQuilometragem();
    const trajeto = Math.abs(this.kmPrevisto - form.step2.quilometragem);

    if (form.step2.quilometragem > this.kmPrevisto && trajeto > 2) {
      const centroOrigem = form.step2.bairroOrigem.toString()?.toLowerCase()?.includes("centro");
      const centroDestino = form.step2.bairroDestino.toString()?.toLowerCase()?.includes("centro");

      if (centroOrigem && centroDestino) {
        (this.form.get('step2') as FormGroup).controls['codDespesaItemAlerta']
          .setValue(DespesaItemAlertaEnum.TecnicoTeveQuilometragemPercorridaMaiorQuePrevistaCalculadaDoCentroAoCentro);
      }
      else {
        (this.form.get('step2') as FormGroup).controls['codDespesaItemAlerta']
          .setValue(DespesaItemAlertaEnum.TecnicoTeveUmaQuilometragemPercorridaMaiorQuePrevista);
      }
      this.tentativaKm++;
      this.despesaInvalida();
    }
    else this.despesaValida();
  }

  private validaDespesaRefeicao() {
    if (!this.isRefeicao()) return;

    if (this.form.value.step2.valor > this.despesaConfiguracao.valorRefeicaoLimiteTecnico) {
      (this.form.get('step2') as FormGroup).controls['codDespesaItemAlerta']
        .setValue(DespesaItemAlertaEnum.TecnicoTeveAlgumaRefeicaoMaiorQueLimiteEspecificado);
      this.despesaInvalida();
    }
    else this.despesaValida();
  }

  private despesaInvalida() {
    (this.form.get('step3') as FormGroup).controls['obs'].enable();
  }

  private despesaValida() {
    (this.form.get('step3') as FormGroup).controls['obs'].reset();
    (this.form.get('step3') as FormGroup).controls['obs'].disable();
  }

  async calculaQuilometragem(): Promise<number> {
    var oLat = (this.form.get('step2') as any).controls['latitudeOrigem'].value;
    var oLong = (this.form.get('step2') as any).controls['longitudeOrigem'].value;

    var dLat = (this.form.get('step2') as any).controls['latitudeDestino'].value;
    var dLong = (this.form.get('step2') as any).controls['longitudeDestino'].value;

    return new Promise(resolve => this._geolocationService.obterDistancia({
      latitudeDestino: dLat,
      longitudeDestino: dLong,
      latitudeOrigem: oLat,
      longitudeOrigem: oLong,
      geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE
    }).subscribe(result => {
      resolve(result.distancia / 1000);
    }));
  }

  private limpaFormOrigem(): void {
    (this.form.get('step2') as FormGroup).controls['enderecoOrigem'].reset();
    (this.form.get('step2') as FormGroup).controls['bairroOrigem'].reset();
    (this.form.get('step2') as FormGroup).controls['cidadeOrigem'].reset();
    (this.form.get('step2') as FormGroup).controls['ufOrigem'].reset();
    (this.form.get('step2') as FormGroup).controls['latitudeOrigem'].reset();
    (this.form.get('step2') as FormGroup).controls['longitudeOrigem'].reset();
    (this.form.get('step2') as FormGroup).controls['quilometragem'].reset();
  }

  private setOrigemResidencial(): void {
    (this.form.get('step2') as FormGroup).controls['enderecoOrigem'].setValue(this.rat.tecnico.endereco);
    (this.form.get('step2') as FormGroup).controls['bairroOrigem'].setValue(this.rat.tecnico.bairro);
    (this.form.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(this.rat.tecnico.cidade.nomeCidade);
    (this.form.get('step2') as FormGroup).controls['ufOrigem'].setValue(this.rat.tecnico.cidade.unidadeFederativa.siglaUF);
    (this.form.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(this.rat.tecnico.latitude);
    (this.form.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(this.rat.tecnico.longitude);
  }

  private setDestinoResidencial(): void {
    (this.form.get('step2') as FormGroup).controls['enderecoDestino'].setValue(this.rat.tecnico.endereco);
    (this.form.get('step2') as FormGroup).controls['bairroDestino'].setValue(this.rat.tecnico.bairro);
    (this.form.get('step2') as FormGroup).controls['cidadeDestino'].setValue(this.rat.tecnico.cidade.nomeCidade);
    (this.form.get('step2') as FormGroup).controls['ufDestino'].setValue(this.rat.tecnico.cidade.unidadeFederativa.siglaUF);
    (this.form.get('step2') as FormGroup).controls['latitudeDestino'].setValue(this.rat.tecnico.latitude);
    (this.form.get('step2') as FormGroup).controls['longitudeDestino'].setValue(this.rat.tecnico.longitude);
  }

  private async setOrigemRATAnterior() {
    const index = _.findIndex(this.rats, (r) => { return r.codRAT == this.rat.codRAT });
    const tipoDespesaSelecionado = this.form.value.step1.codDespesaTipo;

    if (!index || tipoDespesaSelecionado !== DespesaTipoEnum.KM) {
      return;
    }

    const ratAnterior = this.rats[index - 1];
    const os: OrdemServico = await this._ordemServicoSvc.obterPorCodigo(ratAnterior.codOS).toPromise();

    (this.form.get('step2') as FormGroup).controls['enderecoOrigem'].setValue(os.localAtendimento.endereco);
    (this.form.get('step2') as FormGroup).controls['bairroOrigem'].setValue(os.localAtendimento.bairro);
    (this.form.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(os.localAtendimento.cidade.nomeCidade);
    (this.form.get('step2') as FormGroup).controls['ufOrigem'].setValue(os.localAtendimento.cidade.unidadeFederativa.siglaUF);
    (this.form.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(os.localAtendimento.latitude);
    (this.form.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(os.localAtendimento.longitude);

    this.desabilitaFormOrigem();
  }

  private async setDestino() {
    this.desabilitaFormDestino();

    const tipoDespesaSelecionado = this.form.value.step1.codDespesaTipo;
    if (tipoDespesaSelecionado !== DespesaTipoEnum.KM)
      return;

    const qtdDespesasKM = this.obterDespesaItensKM()?.length || 0;
    if (this.isUltimaRATDoDia() && qtdDespesasKM)
      return;

    this.setDestinoLocalChamado();
  }

  private desabilitaFormOrigem() {
    (this.form.get('step2') as FormGroup).controls['enderecoOrigem'].disable();
    (this.form.get('step2') as FormGroup).controls['bairroOrigem'].disable();
    (this.form.get('step2') as FormGroup).controls['cidadeOrigem'].disable();
    (this.form.get('step2') as FormGroup).controls['ufOrigem'].disable();
    (this.form.get('step2') as FormGroup).controls['latitudeOrigem'].disable();
    (this.form.get('step2') as FormGroup).controls['longitudeOrigem'].disable();
  }

  private desabilitaFormDestino() {
    (this.form.get('step2') as FormGroup).controls['enderecoDestino'].disable();
    (this.form.get('step2') as FormGroup).controls['bairroDestino'].disable();
    (this.form.get('step2') as FormGroup).controls['cidadeDestino'].disable();
    (this.form.get('step2') as FormGroup).controls['ufDestino'].disable();
    (this.form.get('step2') as FormGroup).controls['latitudeDestino'].disable();
    (this.form.get('step2') as FormGroup).controls['longitudeDestino'].disable();
  }

  private limpaFormDestino() {
    (this.form.get('step2') as FormGroup).controls['enderecoDestino'].setValue('');
    (this.form.get('step2') as FormGroup).controls['bairroDestino'].setValue('');
    (this.form.get('step2') as FormGroup).controls['cidadeDestino'].setValue('');
    (this.form.get('step2') as FormGroup).controls['ufDestino'].setValue('');
    (this.form.get('step2') as FormGroup).controls['latitudeDestino'].setValue('');
    (this.form.get('step2') as FormGroup).controls['longitudeDestino'].setValue('');
  }

  private habilitaFormOrigem() {
    (this.form.get('step2') as FormGroup).controls['enderecoOrigem'].enable();
    (this.form.get('step2') as FormGroup).controls['bairroOrigem'].enable();
    (this.form.get('step2') as FormGroup).controls['cidadeOrigem'].enable();
    (this.form.get('step2') as FormGroup).controls['ufOrigem'].enable();
    (this.form.get('step2') as FormGroup).controls['latitudeOrigem'].enable();
    (this.form.get('step2') as FormGroup).controls['longitudeOrigem'].enable();
  }

  private habilitaFormDestino() {
    (this.form.get('step2') as FormGroup).controls['enderecoDestino'].enable();
    (this.form.get('step2') as FormGroup).controls['bairroDestino'].enable();
    (this.form.get('step2') as FormGroup).controls['cidadeDestino'].enable();
    (this.form.get('step2') as FormGroup).controls['ufDestino'].enable();
    (this.form.get('step2') as FormGroup).controls['latitudeDestino'].enable();
    (this.form.get('step2') as FormGroup).controls['longitudeDestino'].enable();
  }

  private setOrigemLocalChamado() {
    (this.form.get('step2') as FormGroup).controls['enderecoOrigem'].setValue(this.ordemServico.localAtendimento.endereco);
    (this.form.get('step2') as FormGroup).controls['bairroOrigem'].setValue(this.ordemServico.localAtendimento.bairro);
    (this.form.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(this.ordemServico.localAtendimento.cidade.nomeCidade);
    (this.form.get('step2') as FormGroup).controls['ufOrigem'].setValue(this.ordemServico.localAtendimento.cidade.unidadeFederativa.siglaUF);
    (this.form.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(this.ordemServico.localAtendimento.latitude);
    (this.form.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(this.ordemServico.localAtendimento.longitude);
  }

  private setDestinoLocalChamado() {
    (this.form.get('step2') as FormGroup).controls['enderecoDestino'].setValue(this.ordemServico.localAtendimento.endereco);
    (this.form.get('step2') as FormGroup).controls['bairroDestino'].setValue(this.ordemServico.localAtendimento.bairro);
    (this.form.get('step2') as FormGroup).controls['cidadeDestino'].setValue(this.ordemServico.localAtendimento.cidade.nomeCidade);
    (this.form.get('step2') as FormGroup).controls['ufDestino'].setValue(this.ordemServico.localAtendimento.cidade.unidadeFederativa.siglaUF);
    (this.form.get('step2') as FormGroup).controls['latitudeDestino'].setValue(this.ordemServico.localAtendimento.latitude);
    (this.form.get('step2') as FormGroup).controls['longitudeDestino'].setValue(this.ordemServico.localAtendimento.longitude);
  }

  private localInicioDeslocamentoEmitter() {
    if (!this.isPrimeiraRATDoDia())
      return;

    (this.form.get('step2') as FormGroup).controls['localInicioDeslocamento'].valueChanges.subscribe(() => {
      this.limpaFormOrigem();
      const local = (this.form.get('step2') as FormGroup).controls['localInicioDeslocamento'].value;

      if (local === "residencial") {
        this.desabilitaFormOrigem();
        this.setOrigemResidencial();
        this.isResidenciaOrigem = true;
        this.isHotelOrigem = false;
      } else if (local == "hotel") {
        this.habilitaFormOrigem();
        this.isResidenciaOrigem = false;
        this.isHotelOrigem = true;
      }
    });
  }

  private async enderecoOrigemEmitter() {
    (this.form.get('step2') as FormGroup).controls['enderecoOrigem'].valueChanges.pipe(
      filter(endereco => !!endereco),
      debounceTime(700),
      takeUntil(this._onDestroy),
      map(async endereco => {
        if (endereco && this.obterLocalInicioDeslocamento() == 'hotel') {
          const localizacao: Geolocalizacao = await this.obterCoordenadasEndereco(endereco);

          (this.form.get('step2') as FormGroup).controls['bairroOrigem'].setValue(localizacao.bairro);
          (this.form.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(localizacao.cidade);
          (this.form.get('step2') as FormGroup).controls['ufOrigem'].setValue(localizacao.estado);
          (this.form.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(localizacao.latitude);
          (this.form.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(localizacao.longitude);
        }
      })
    ).subscribe(() => { });
  }

  private async enderecoDestinoEmitter() {
    (this.form.get('step2') as FormGroup).controls['enderecoDestino'].valueChanges.pipe(
      filter(endereco => !!endereco),
      debounceTime(700),
      takeUntil(this._onDestroy),
      map(async endereco => {
        if (endereco && this.obterLocalDestinoDeslocamento() == 'hotel') {
          const localizacao: Geolocalizacao = await this.obterCoordenadasEndereco(endereco);

          (this.form.get('step2') as FormGroup).controls['bairroDestino'].setValue(localizacao.bairro);
          (this.form.get('step2') as FormGroup).controls['cidadeDestino'].setValue(localizacao.cidade);
          (this.form.get('step2') as FormGroup).controls['ufDestino'].setValue(localizacao.estado);
          (this.form.get('step2') as FormGroup).controls['latitudeDestino'].setValue(localizacao.latitude);
          (this.form.get('step2') as FormGroup).controls['longitudeDestino'].setValue(localizacao.longitude);
        }
      })
    ).subscribe(() => { });
  }

  localDestinoDeslocamentoEmitter() {
    if (!this.isUltimaRATDoDia())
      return;

    (this.form.get('step2') as FormGroup).controls['localDestinoDeslocamento'].valueChanges.subscribe(() => {
      this.limpaFormDestino();
      const local = (this.form.get('step2') as FormGroup).controls['localDestinoDeslocamento'].value;

      if (local === "residencial") {
        this.desabilitaFormDestino();
        this.setDestinoResidencial();
        this.isResidenciaDestino = true;
        this.isHotelDestino = false;
      }
      else if (local === "hotel") {
        this.habilitaFormDestino();
        this.isResidenciaDestino = false;
        this.isHotelDestino = true;
      }
    });
  }

  private obterDespesaItensKM() {
    return this.despesa.despesaItens?.filter(i => i.codDespesaTipo === DespesaTipoEnum.KM && i.indAtivo == statusConst.ATIVO);
  }

  private configuraCamposHabilitados() {
    if (!this.isQuilometragem()) {
      (this.form.get('step2') as FormGroup).controls['quilometragem'].disable();
      (this.form.get('step3') as FormGroup).controls['obs'].disable();
      (this.form.get('step2') as FormGroup).controls['valor'].enable();
    }
    else {
      (this.form.get('step2') as FormGroup).controls['quilometragem'].enable();
      (this.form.get('step3') as FormGroup).controls['obs'].enable();
      (this.form.get('step2') as FormGroup).controls['valor'].disable();
    }
  }

  private async obterCoordenadasEndereco(endereco: string): Promise<Geolocalizacao> {
    return new Promise((resolve, reject) => {
      const params = { enderecoCep: endereco, geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE };

      this._geolocationService.obterPorParametros(params).subscribe((geo: Geolocalizacao) => {
        resolve(geo);
      }, e => {
        reject(e);
      });
    });
  }

  private obterLocalInicioDeslocamento(): string {
    return (this.form.get('step2') as FormGroup).controls['localInicioDeslocamento'].value;
  }

  private obterLocalDestinoDeslocamento(): string {
    return (this.form.get('step2') as FormGroup).controls['localDestinoDeslocamento'].value;
  }

  async revisar() {
    this.isValidating = true;

    await this.validaCalculoQuilometragem();
    await this.validaDespesaRefeicao();

    this.isValidating = false;
  }

  isPrimeiraRATDoDia(): boolean {
    return _.findIndex(this.rats, (r) => { return r.codRAT == this.rat.codRAT }) == 0;
  }

  isUltimaRATDoDia(): boolean {
    return _.findIndex(this.rats, (r) => { return r.codRAT == this.rat.codRAT }) == this.rats.length - 1;
  }

  verificarObservacaoVazia(str: string): boolean {
    if(str)
    {
      if(str.trim().length !== 0)
        return true;
      
      return false;
    }
    
    return false;
  }

  async salvar() {
    this.loading = true;

    const despesaItem: DespesaItem = this.isQuilometragem() ? await this.criaDespesaItemQuilometragem() : await this.criaDespesaItemOutros();

      this._despesaItemSvc.criar(despesaItem)
        .subscribe(() => {
          this.dialogRef.close(true);
          this.loading = false;
        },
          () => {
            this._snack.exibirToast('Erro ao adicionar item da despesa!', 'error');
            this.dialogRef.close(false);
            this.loading = false;
          }
        );
  }

  async criaDespesaItemQuilometragem() {
    const form = this.form.getRawValue();
    const codCidadeOrigem = await this.obterCidadePeloNome(form.step2.cidadeOrigem);
    const codCidadeDestino = await this.obterCidadePeloNome(form.step2.cidadeDestino);

    let latitudeHotel: string;
    let longitudeHotel: string;

    if (this.isHotelOrigem) {
      latitudeHotel = form.step2.latitudeOrigem;
      longitudeHotel = form.step2.longitudeOrigem;
    }

    if (this.isHotelDestino) {
      latitudeHotel = form.step2.latitudeDestino;
      longitudeHotel = form.step2.longitudeDestino;
    }

    var despesaItem: DespesaItem = {
      codDespesa: this.codDespesa,
      numNF: form.step2.notaFiscal,
      codDespesaTipo: form.step1.codDespesaTipo,
      despesaValor: this.calculaConsumoCombustivel(),
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      codDespesaItemAlerta: form.step2.codDespesaItemAlerta != 0 ? form.step2.codDespesaItemAlerta : null,
      codDespesaConfiguracao: this.despesaConfiguracao.codDespesaConfiguracao,
      enderecoOrigem: form.step2.enderecoOrigem,
      numOrigem: form.step2.numeroOrigem,
      bairroOrigem: form.step2.bairroOrigem,
      codCidadeOrigem: codCidadeOrigem,
      indResidenciaOrigem: +this.isResidenciaOrigem,
      indHotelOrigem: +this.isHotelOrigem,
      enderecoDestino: form.step2.enderecoDestino,
      numDestino: form.step2.numeroDestino,
      bairroDestino: form.step2.bairroDestino,
      codCidadeDestino: codCidadeDestino,
      sequenciaDespesaKm: 0,
      indResidenciaDestino: +this.isResidenciaDestino,
      indHotelDestino: +this.isHotelDestino,
      kmPrevisto: this.kmPrevisto,
      kmPercorrido: form.step2.quilometragem,
      tentativaKM: this.tentativaKm.toString(),
      obs: form.step3.obs,
      latitudeHotel: latitudeHotel,
      longitudeHotel: longitudeHotel,
      indAtivo: statusConst.ATIVO
    };

    return despesaItem;
  }

  criaDespesaItemOutros(): DespesaItem {
    const form = this.form.getRawValue();

    var despesaItem: DespesaItem = {
      codDespesa: this.codDespesa,
      numNF: form.step2.notaFiscal,
      codDespesaTipo: form.step1.codDespesaTipo,
      despesaValor: form.step2.valor,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      codDespesaItemAlerta: form.step2.codDespesaItemAlerta != 0 ? form.step2.codDespesaItemAlerta : null,
      codDespesaConfiguracao: this.despesaConfiguracao.codDespesaConfiguracao,
      obs: form.step3.obs,
      indAtivo: statusConst.ATIVO
    };

    return despesaItem;
  }

  private mostrarOpcaoResidenciaHotelOrigem(): boolean {
    const despesaItensKM = this.obterDespesaItensKM();
    const isPrimeiroDia = this.isPrimeiraRATDoDia();
    
    if (isPrimeiroDia && !despesaItensKM)
        return true;

    if (isPrimeiroDia && despesaItensKM.length == 0)
        return true;

    return false;
  }

  private mostrarOpcaoResidenciaHotelDestino() {
    const despesaItensKM = this.obterDespesaItensKM();
    const isUltimaRATDoDia = this.isUltimaRATDoDia()

    if (isUltimaRATDoDia && despesaItensKM?.length > 0)
      return true;

    return false;
  }

  obterMensagemAlerta() {
    var codDespesaItemAlerta =
      this.form.value.step2.codDespesaItemAlerta;

    if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveAlgumaRefeicaoMaiorQueLimiteEspecificado)
      return "Valor da refeição maior que o limite especificado. Por favor, justifique abaixo."
    else if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveQuilometragemPercorridaMaiorQuePrevistaCalculadaDoCentroAoCentro)
      return "Quilometragem maior que a prevista. Por favor, justifique abaixo."
    else if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveUmaQuilometragemPercorridaMaiorQuePrevista)
      return "Quilometragem maior que a prevista. Por favor, justifique abaixo."
  }

  calculaConsumoCombustivel(): number {
    return (this.form.value.step2.quilometragem / appConfig.autonomia_veiculo_frota) * this.despesaConfiguracaoCombustivel?.precoLitro;
  }

  obterTipoDespesa() {
    return Enumerable
      .from(this.tiposDespesa)
      .firstOrDefault(i => i.codDespesaTipo == this.form.value.step1.codDespesaTipo)?.nomeTipo;
  }

  onProximo(): void {
    this.configuraCamposHabilitados();
    this.desabilitaFormOrigem();
    const despesasKM = this.obterDespesaItensKM();

    if (despesasKM) {
      if (this.isUltimaRATDoDia() && despesasKM.length)
        this.setOrigemLocalChamado();
      else
        this.setOrigemRATAnterior();
    }
    else
        this.setOrigemRATAnterior();

    this.setDestino();
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
