import { Inject, Component, LOCALE_ID, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaTipoService } from 'app/core/services/despesa-tipo.service';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { DespesaConfiguracaoCombustivel } from 'app/core/types/despesa-configuracao-combustivel.types';
import { Despesa, DespesaConfiguracao, DespesaItem, DespesaItemAlertaData, DespesaItemAlertaEnum, DespesaItemParameters, DespesaTipo, DespesaTipoEnum, DespesaTipoParameters } from 'app/core/types/despesa.types';
import { OrdemServico, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';
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
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import _ from 'lodash';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
declare var L: any;

@Component({
  selector: 'app-despesa-item-dialog',
  templateUrl: './despesa-item-dialog.component.html',
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaItemDialogComponent implements OnInit {
  despesaItemForm: FormGroup;
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
  isResidencial: boolean;
  isValidating: boolean = false;
  kmPrevisto: number;
  tentativaKm: number = 0;

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
    private dialogRef: MatDialogRef<DespesaItemDialogComponent>) {
    if (data)
    {
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
  }

  private async obterTiposDespesa() {
    const params: DespesaTipoParameters = { indAtivo: statusConst.ATIVO };
    const tipos = await this._despesaTipoSvc.obterPorParametros(params).toPromise();
    this.tiposDespesa = tipos.items;

    if (this.obterDespesaItensKM().length == 2 || (this.obterDespesaItensKM().length == 1 && !this.isUltimaRATDoDia())) { // Adicionar regra de plantao
      this.tiposDespesa = Enumerable.from(this.tiposDespesa)
        .where(i => i.codDespesaTipo != DespesaTipoEnum.KM)
        .toArray();
    }
  }

  private criarForm() {
    this.despesaItemForm = this._formBuilder.group({
      step1: this._formBuilder.group({
        codDespesaTipo: [undefined, Validators.required]
      }),
      step2: this._formBuilder.group({
        notaFiscal: [undefined],
        valor: [undefined, Validators.required],
        localInicioDeslocamento: [undefined],
        localFimDeslocamento: [undefined],
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

  public isQuilometragem() {
    return this.despesaItemForm.value.step1.codDespesaTipo == DespesaTipoEnum.KM;
  }

  public isRefeicao() {
    return this.despesaItemForm.value.step1.codDespesaTipo == DespesaTipoEnum.REFEICAO;
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

    this.kmPrevisto = await this.calculaQuilometragem();
    const trajeto = Math.abs(this.kmPrevisto - this.despesaItemForm.value.step2.quilometragem);

    if (this.despesaItemForm.value.step2.quilometragem > this.kmPrevisto && trajeto > 2)
    {
      const centroOrigem = this.despesaItemForm.value.step2.bairroOrigem.toString()?.toLowerCase()?.includes("centro");
      const centroDestino = this.despesaItemForm.value.step2.bairroDestino.toString()?.toLowerCase()?.includes("centro");

      if (centroOrigem && centroDestino)
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

  private validaDespesaRefeicao() {
    if (!this.isRefeicao()) return;

    if (this.despesaItemForm.value.step2.valor > this.despesaConfiguracao.valorRefeicaoLimiteTecnico)
    {
      (this.despesaItemForm.get('step2') as FormGroup).controls['codDespesaItemAlerta']
        .setValue(DespesaItemAlertaEnum.TecnicoTeveAlgumaRefeicaoMaiorQueLimiteEspecificado);
      this.despesaInvalida();
    }
    else this.despesaValida();
  }

  private despesaInvalida() {
    (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].enable();
  }

  private despesaValida() {
    (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].reset();
    (this.despesaItemForm.get('step3') as FormGroup).controls['obs'].disable();
  }

  async calculaQuilometragem(): Promise<number> {
    var oLat = (this.despesaItemForm.get('step2') as any).controls['latitudeOrigem'].value;
    var oLong = (this.despesaItemForm.get('step2') as any).controls['longitudeOrigem'].value;

    var dLat = (this.despesaItemForm.get('step2') as any).controls['latitudeDestino'].value;
    var dLong = (this.despesaItemForm.get('step2') as any).controls['longitudeDestino'].value;

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

  private resetFields(): void {
    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].reset();
    (this.despesaItemForm.get('step2') as FormGroup).controls['quilometragem'].reset();
  }

  private setOrigemResidencial(): void {
    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].setValue(this.rat.tecnico.endereco);
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].setValue(this.rat.tecnico.bairro);
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(this.rat.tecnico.cidade.nomeCidade);
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].setValue(this.rat.tecnico.cidade.unidadeFederativa.siglaUF);
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(this.rat.tecnico.latitude);
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(this.rat.tecnico.longitude);
  }

  private async setOrigemRATAnterior() {
    const index = _.findIndex(this.rats, (r) => { return r.codRAT == this.rat.codRAT });
    const tipoDespesaSelecionado = this.despesaItemForm.value.step1.codDespesaTipo;
    if (!index || tipoDespesaSelecionado !== DespesaTipoEnum.KM)
      return;

    const ratAnterior = this.rats[index-1];
    const os: OrdemServico = await this._ordemServicoSvc.obterPorCodigo(ratAnterior.codOS).toPromise();

    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].setValue(os.localAtendimento.endereco);
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].setValue(os.localAtendimento.bairro);
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].setValue(os.localAtendimento.cidade.nomeCidade);
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].setValue(os.localAtendimento.cidade.unidadeFederativa.siglaUF);
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].setValue(os.localAtendimento.latitude);
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].setValue(os.localAtendimento.longitude);
    
    this.desabilitaFormOrigem();
  }

  private async setDestino() {
    const tipoDespesaSelecionado = this.despesaItemForm.value.step1.codDespesaTipo;
    if (tipoDespesaSelecionado !== DespesaTipoEnum.KM)
      return;

    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoDestino'].setValue(this.ordemServico.localAtendimento.endereco);
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroDestino'].setValue(this.ordemServico.localAtendimento.bairro);
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeDestino'].setValue(this.ordemServico.localAtendimento.cidade.nomeCidade);
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufDestino'].setValue(this.ordemServico.localAtendimento.cidade.unidadeFederativa.siglaUF);
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeDestino'].setValue(this.ordemServico.localAtendimento.latitude);
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeDestino'].setValue(this.ordemServico.localAtendimento.longitude);
    
    this.desabilitaFormDestino();
  }
  
  private desabilitaFormOrigem() {
    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeOrigem'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeOrigem'].disable();
  }
  
  private desabilitaFormDestino() {
    (this.despesaItemForm.get('step2') as FormGroup).controls['enderecoDestino'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['bairroDestino'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['cidadeDestino'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['ufDestino'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['latitudeDestino'].disable();
    (this.despesaItemForm.get('step2') as FormGroup).controls['longitudeDestino'].disable();
  }

  private registrarEmitters() {
    this.localInicioDeslocamentoEmitter();
  }
  
  private localInicioDeslocamentoEmitter() {
    if (!this.isPrimeiraRATDoDia())
      return;

    (this.despesaItemForm.get('step2') as FormGroup).controls['localInicioDeslocamento'].valueChanges.subscribe(() => {
      this.resetFields();

      if ((this.despesaItemForm.get('step2') as FormGroup)
        .controls['localInicioDeslocamento'].value === "residencial")
      {
        this.setOrigemResidencial();
        this.isResidencial = true;
      }
      else
        this.isResidencial = false;
    });
  }

  private obterDespesaItensKM() {
    return this.despesa.despesaItens.filter(i => i.codDespesaTipo === DespesaTipoEnum.KM);
  }

  private configuraCamposHabilitados() {
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

  async salvar() {
    const despesaItem: DespesaItem = this.isQuilometragem() ? await this.criaDespesaItemQuilometragem() : await this.criaDespesaItemOutros();
    
    this._despesaItemSvc.criar(despesaItem)
      .subscribe(() => {
        this.despesa.despesaItens.push(despesaItem);
        this.dialogRef.close(true);
      },
        () => {
          this._snack.exibirToast('Erro ao adicionar item da despesa!', 'error');
          this.dialogRef.close(false);
        }
      );
  }

  async criaDespesaItemQuilometragem() {
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
      indResidenciaOrigem: +this.isResidencial || 0,
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

  criaDespesaItemOutros(): DespesaItem {
    var despesaItem: DespesaItem =
    {
      codDespesa: this.codDespesa,
      numNF: this.despesaItemForm.value.step2.notaFiscal,
      codDespesaTipo: this.despesaItemForm.value.step1.codDespesaTipo,
      despesaValor: this.despesaItemForm.value.step2.valor,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      codDespesaItemAlerta: this.despesaItemForm.value.step2.codDespesaItemAlerta != 0 ? this.despesaItemForm.value.step2.codDespesaItemAlerta : null,
      codDespesaConfiguracao: this.despesaConfiguracao.codDespesaConfiguracao,
      obs: this.despesaItemForm.value.step3.obs,
      indAtivo: statusConst.ATIVO
    };

    return despesaItem;
  }

  mostrarOpcaoResidenciaHotelOrigem() {
    const despesaItensKM = this.obterDespesaItensKM();

    if (this.isPrimeiraRATDoDia() && despesaItensKM.length == 0)
      return true;

    return false;
  }

  mostrarOpcaoResidenciaHotelDestino() {
    const despesaItensKM = this.obterDespesaItensKM();

    if (this.isUltimaRATDoDia() && despesaItensKM.length > 0)
      return true;

    return false;
  }

  obterMensagemAlerta() {
    var codDespesaItemAlerta =
      this.despesaItemForm.value.step2.codDespesaItemAlerta;

    if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveAlgumaRefeicaoMaiorQueLimiteEspecificado)
      return "Valor da refeição maior que o limite especificado. Por favor, justifique abaixo."
    else if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveQuilometragemPercorridaMaiorQuePrevistaCalculadaDoCentroAoCentro)
      return "Quilometragem maior que a prevista. Por favor, justifique abaixo."
    else if (codDespesaItemAlerta == DespesaItemAlertaEnum.TecnicoTeveUmaQuilometragemPercorridaMaiorQuePrevista)
      return "Quilometragem maior que a prevista. Por favor, justifique abaixo."
  }

  calculaConsumoCombustivel(): number {
    return (this.despesaItemForm.value.step2.quilometragem / appConfig.autonomia_veiculo_frota) * this.despesaConfiguracaoCombustivel?.precoLitro;
  }

  obterTipoDespesa() {
    return Enumerable
      .from(this.tiposDespesa)
      .firstOrDefault(i => i.codDespesaTipo == this.despesaItemForm.value.step1.codDespesaTipo)?.nomeTipo;
  }

  onProximo(): void {
    this.configuraCamposHabilitados();
    this.setOrigemRATAnterior();
    this.setDestino();
  }
}
