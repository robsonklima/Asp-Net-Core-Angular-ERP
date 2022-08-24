import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CidadeService } from 'app/core/services/cidade.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { PaisService } from 'app/core/services/pais.service';
import { TransportadoraService } from 'app/core/services/transportadora.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Cidade } from 'app/core/types/cidade.types';
import { Cliente } from 'app/core/types/cliente.types';
import { Geolocalizacao, GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';
import { Pais } from 'app/core/types/pais.types';
import { Transportadora } from 'app/core/types/transportadora.types';
import { UnidadeFederativa } from 'app/core/types/unidade-federativa.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, first, map, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { FormaPagamentoService } from 'app/core/services/forma-pagamento.service';
import { FormaPagamento } from 'app/core/types/forma-pagamento.types';
import { MoedaService } from 'app/core/services/moeda.service';
import { PecaListaService } from 'app/core/services/peca-lista.service';
import { TipoFreteService } from 'app/core/services/tipo-frete.service';
import { Moeda } from 'app/core/types/moeda.types';
import { PecaLista } from 'app/core/types/peca-lista.types';
import { TipoFrete } from 'app/core/types/tipo-frete.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';

@Component({
  selector: 'app-cliente-form',
  templateUrl: './cliente-form.component.html'
})
export class ClienteFormComponent implements OnInit, OnDestroy {

  private userSession: UsuarioSessao;

  protected _onDestroy = new Subject<void>();
  public cliente: Cliente;
  public loading: boolean = true;
  public codCliente: number;
  public isAddMode: boolean;
  public form: FormGroup;

  public buscandoCEP: boolean = false;
  public paises: Pais[] = [];
  public unidadesFederativas: UnidadeFederativa[] = [];
  public cidades: Cidade[] = [];
  public formasPagamento: FormaPagamento[] = [];
  public moedas: Moeda[] = [];
  public pecasLista: PecaLista[] = [];
  public tiposFrete: TipoFrete[] = [];
  public transportadoras: Transportadora[] = [];

  cidadeFiltro: FormControl = new FormControl();
  clienteFilterCtrl: FormControl = new FormControl();
  contratosFilterCtrl: FormControl = new FormControl();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _transportadoraService: TransportadoraService,
    private _paisService: PaisService,
    private _cidadeService: CidadeService,
    private _clienteService: ClienteService,
    private _userService: UserService,
    private _unidadeFederativaService: UnidadeFederativaService,
    private _cdr: ChangeDetectorRef,
    private _googleGeolocationService: GeolocalizacaoService,
    private _moedaService: MoedaService,
    private _pecaLista: PecaListaService,
    private _tipoFrete: TipoFreteService,
    private _location: Location,
    private _formaPagamento: FormaPagamentoService
  ) { this.userSession = JSON.parse(this._userService.userSession); }

  async ngOnInit() {

    this.codCliente = +this._route.snapshot.paramMap.get('codCliente');
    this.isAddMode = !this.codCliente;
    this.inicializarForm();

    this.paises = await this._paisService.obterPaises();
    this.transportadoras = (await ((this._transportadoraService.obterPorParametros({ indAtivo: 1 })).toPromise())).items;
    this.formasPagamento = (await this._formaPagamento.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
    this.moedas = (await this._moedaService.obterPorParametros({}).toPromise()).items;
    this.pecasLista = (await this._pecaLista.obterPorParametros({}).toPromise()).items;
    this.tiposFrete = (await this._tipoFrete.obterPorParametros({}).toPromise()).items;

    this.form.controls['indAtivo'].setValue(true);

    if (!this.isAddMode) {
      this.carregarDadosCliente();
    }

    this.loading = false;
  }

  private carregarDadosCliente() {
    this._clienteService.obterPorCodigo(this.codCliente)
      .pipe(first())
      .subscribe(data => {
        this.cliente = data;
        this.form.patchValue(data);
        this.form.controls['codPais'].setValue(data.cidade?.unidadeFederativa?.codPais);
        this.form.controls['codUF'].setValue(data.cidade?.codUF);
        this.form.controls['codCidade'].setValue(data.codCidade);

        this.form.controls['inflacao'].setValue(data.inflacao.toFixed(2));
        this.form.controls['deflacao'].setValue(data.deflacao.toFixed(2));
        this.form.controls['percIcms'].setValue(data.percIcms.toFixed(2));
      });
  }

  private inicializarForm() {

    this.form = this._formBuilder.group({
      codCliente: [
        {
          value: undefined,
          disabled: true
        }
      ],
      razaoSocial: [undefined, [Validators.required, Validators.maxLength(50)]],
      endereco: [undefined, [Validators.required, Validators.maxLength(100)]],
      nomeFantasia: [undefined, [Validators.required, Validators.maxLength(50)]],
      cnpj: [undefined, Validators.required],
      inscricaoEstadual: [undefined, Validators.required],
      numBanco: [undefined, Validators.required],
      bairro: [undefined, [Validators.required]],
      cep: [undefined, [Validators.required]],
      codUF: [undefined, [Validators.required]],
      codPais: [undefined, [Validators.required]],
      codCidade: [undefined, Validators.required],
      indAtivo: [undefined],
      email: [undefined],
      fone: [undefined],
      site: [undefined],
      observacao: [undefined],
      codTransportadora: [undefined],
      inflacao: [undefined],
      deflacao: [undefined],
      codFormaPagto: [undefined],
      codMoeda: [undefined],
      codPecaLista: [undefined],
      codTipoFrete: [undefined],
      percIcms: [undefined],
      indHabilitaVendaPecas: [undefined],
      indPecaListaSomente: [undefined],
      indRevisao: [undefined]
    });

    this.form.controls['codPais'].valueChanges.subscribe(async () => {
      this.unidadesFederativas = [];
      this.unidadesFederativas = await this._unidadeFederativaService.obterUnidadesFederativas(this.form.controls['codPais'].value);
      this._cdr.detectChanges();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async () => {
      this.cidades = [];
      this.cidades = await this._cidadeService.obterCidades(this.form.controls['codUF'].value);
      this._cdr.detectChanges();
    });

    this.cidadeFiltro.valueChanges.pipe(
      filter(filtro => !!filtro),
      debounceTime(700),
      delay(500),
      takeUntil(this._onDestroy),
      map(async filtro => {
        this.cidades = await this._cidadeService.obterCidades(this.form.controls['codUF'].value, filtro);
        this._cdr.detectChanges();
      })
    ).toPromise();

  }

  async buscaCEP(cepCmp: any) {
    if (cepCmp.target.value) {
      this.form.disable();
      // Google
      // Tenta pelo cep (nem sempre os endereços são corretos)
      this._googleGeolocationService.obterPorParametros
        ({ enderecoCep: cepCmp.target.value.replace(/\D+/g, ''), geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE })
        .subscribe((mapService: Geolocalizacao) => {
          if (mapService) {
            this.form.controls['endereco'].setValue(mapService.endereco);
            this.form.controls['bairro'].setValue(mapService.bairro);

            this._cidadeService.obterCidades(null, mapService.cidade).then(c => {
              const data = c[0];
              if (data) {
                this.form.controls['codPais'].setValue(data.unidadeFederativa?.codPais);
                this.form.controls['codUF'].setValue(data.codUF);
                this.form.controls['codCidade'].setValue(data.codCidade);
              }
            });
          }
          this.form.enable();
          this._cdr.detectChanges();
        });
    }
  }

  public salvar(): void {

    const form = this.form.getRawValue();

    let obj = {
      ...this.cliente,
      ...form,
      ...{
        indAtivo: +form.indAtivo,
        indHabilitaVendaPecas: +form.indHabilitaVendaPecas,
        indPecaListaSomente: +form.indPecaListaSomente,
        indRevisao: +form.indRevisao
      }
    };

    if (this.isAddMode) {
      obj.codUsuarioCad = this.userSession.usuario.codUsuario;
      obj.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');

      this._clienteService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Cliente adicionado com sucesso!`, "success");
        this._location.back();
      });
    } else {
      obj.codUsuarioManut = this.userSession.usuario.codUsuario;
      obj.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

      this._clienteService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Cliente atualizado com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
