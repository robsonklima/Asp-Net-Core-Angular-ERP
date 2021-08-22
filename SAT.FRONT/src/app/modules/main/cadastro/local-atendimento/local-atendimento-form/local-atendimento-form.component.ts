import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TipoRotaEnum } from 'app/core/enums/tipo-rota.enum';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { CidadeService } from 'app/core/services/cidade.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FilialService } from 'app/core/services/filial.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { NominatimService } from 'app/core/services/nominatim.service';
import { PaisService } from 'app/core/services/pais.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { LocalAtendimento } from 'app/core/types/local-atendimento.types';
import { Pais, PaisParameters } from 'app/core/types/pais.types';
import { Regiao } from 'app/core/types/regiao.types';
import { TipoRota } from 'app/core/types/tipo-rota.types';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-local-atendimento-form',
  templateUrl: './local-atendimento-form.component.html'
})
export class LocalAtendimentoFormComponent implements OnInit, OnDestroy {
  codPosto: number;
  local: LocalAtendimento;
  form: FormGroup;
  isAddMode: boolean;
  paises: Pais[] = [];
  ufs: UnidadeFederativa[] = [];
  ufsFiltro: FormControl = new FormControl();
  cidades: Cidade[] = [];
  cidadesFiltro: FormControl = new FormControl();
  clientes: Cliente[] = [];
  regioes: Regiao[] = [];
  regioesFiltro: FormControl = new FormControl();
  autorizadas: Autorizada[] = [];
  autorizadasFiltro: FormControl = new FormControl();
  tiposRota: TipoRota[] = [];
  filiais: Filial[] = [];
  userSession: UsuarioSessionData;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _router: Router,
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _paisService: PaisService,
    private _ufService: UnidadeFederativaService,
    private _cidadeService: CidadeService,
    private _localService: LocalAtendimentoService,
    private _autorizadaService: AutorizadaService,
    private _clienteService: ClienteService,
    private _filialService: FilialService,
    private _nominatimService: NominatimService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codPosto = +this._route.snapshot.paramMap.get('codPosto');
    this.isAddMode = !this.codPosto;
    this.inicializarForm();

    this.obterPaises();
    this.obterClientes();
    this.obterFiliais();
    this.obterTiposRota();
    
    this.form.controls['codPais'].valueChanges.subscribe(async () => {
      this.obterUFs();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async () => {
      this.obterCidades();
    });

    this.form.controls['codFilial'].valueChanges.subscribe(async () => {
      this.obterAutorizadas();
    });

    this.form.controls['codAutorizada'].valueChanges.subscribe(async () => {
      this.obterRegioes();
    });

    this.form.controls['codAutorizada'].valueChanges.subscribe(async () => {
      this.obterRegioes();
    });

    this.form.controls['cep'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text => { 
        if (text.length === 9) {
          this.obterLatLngPorEndereco();
        }
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.cidadesFiltro.valueChanges.pipe(
      filter(filtro => !!filtro),
      tap(() => { }),
      debounceTime(700),
      map(async filtro => { this.obterCidades(filtro) }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    if (!this.isAddMode) {
      this.obterLocal();
    }
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codPosto: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      nomeLocal: [undefined, [Validators.required]],
      codPais: [undefined, Validators.required],
      codUF: [undefined, Validators.required],
      codCidade: [undefined, Validators.required],
      codCliente: [undefined, Validators.required],
      dcPosto: [undefined, [Validators.required, Validators.maxLength(2)]],
      numAgencia: [undefined, [Validators.required, Validators.maxLength(5)]],
      cnpj: [undefined, Validators.required],
      inscricaoEstadual: [undefined],
      cep: [undefined, Validators.required],
      endereco: [undefined, Validators.required],
      enderecoComplemento: [undefined],
      bairro: [undefined, Validators.required],
      email: [undefined],
      site: [undefined],
      fone: [undefined],
      descTurno: [undefined],
      distanciaKmPatRes: [undefined],
      observacao: [undefined],
      numeroEnd: [undefined, Validators.required],
      cnpjFaturamento: [undefined],
      codRegiao: [undefined, Validators.required],
      codAutorizada: [
        {
          value: undefined,
          disabled: true,
        }, Validators.required
      ],
      codFilial: [undefined, Validators.required],
      codTipoRota: [undefined],
      latitude: [
        {
          value: undefined,
          disabled: true,
        }, Validators.required
      ],
      longitude: [
        {
          value: undefined,
          disabled: true,
        }, Validators.required
      ],
      indAtivo: [undefined]
    });
  }

  private async obterLocal() {
    this.local = await this._localService.obterPorCodigo(this.codPosto).toPromise();
    this.form.patchValue(this.local);
  }

  private async obterPaises() {
    const params: PaisParameters = {
      sortActive: 'nomePais',
      sortDirection: 'asc',
      pageSize: 200
    }

    const data = await this._paisService.obterPorParametros(params).toPromise();
    this.paises = data.paises;
  }

  private async obterUFs() {
    const params: UnidadeFederativaParameters = {
      sortActive: 'nomeUF',
      sortDirection: 'asc',
      pageSize: 50
    }

    const data = await this._ufService.obterPorParametros(params).toPromise();
    this.ufs = data.unidadesFederativas;
  }

  private async obterCidades(filtro: string = '') {
    const params: CidadeParameters = {
      sortActive: 'nomeCidade',
      sortDirection: 'asc',
      pageSize: 50,
      indAtivo: 1,
      filter: filtro
    }

    const data = await this._cidadeService.obterPorParametros(params).toPromise();
    this.cidades = data.cidades;
  }

  private async obterClientes() {
    const params: ClienteParameters = {
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      indAtivo: 1,
      pageSize: 50
    }

    const data = await this._clienteService.obterPorParametros(params).toPromise();
    this.clientes = data.clientes;
  }

  private async obterFiliais() {
    const params: FilialParameters = {
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      indAtivo: 1,
      pageSize: 50
    }

    const data = await this._filialService.obterPorParametros(params).toPromise();
    this.filiais = data.filiais;
  }

  private async obterAutorizadas() {
    const params: AutorizadaParameters = {
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      indAtivo: 1,
      codFilial: this.form.controls['codFilial'].value,
      pageSize: 50
    }

    const data = await this._autorizadaService.obterPorParametros(params).toPromise();
    this.autorizadas = data.autorizadas;
  }

  private async obterRegioes() {
    const codAutorizada = this.form.controls['codAutorizada'].value;

    const data = await this._regiaoAutorizadaService.obterPorParametros({
      codAutorizada: codAutorizada,
      pageSize: 100
    }).toPromise();

    this.regioes = data.regioesAutorizadas
      .filter(ra => ra.codAutorizada === codAutorizada)
      .map(ra => ra.regiao);
  }

  private async obterLatLngPorEndereco() {
    const cep = this.form.controls['cep']?.value || '';
    const endereco = this.form.controls['endereco']?.value || '';
    const numero = this.form.controls['numero']?.value || '';
    const bairro = this.form.controls['bairro']?.value || '';
    
    const codCidade = this.form.controls['codCidade']?.value;
    let cidade: string = '';

    if (codCidade) {
      cidade = (await this._cidadeService.obterPorCodigo(codCidade).toPromise()).nomeCidade;
    }

    const query = `${cep} ${endereco} ${numero} ${bairro} ${cidade}`.replace('  ', '');
    
    this._nominatimService.buscarEndereco(query).subscribe(data => {
      if (data) {
        const infos = data.filter(m => {
          m.address.country === 'Brazil'
        })
      }

      console.log(query, data);      

      const info = data.shift();
      
      if (info) {
        this.form.controls['endereco'].setValue(info.display_name);
        this.form.controls['bairro'].setValue(info.suburb);
        this.form.controls['latitude'].setValue(info.lat);
        this.form.controls['longitude'].setValue(info.lon);
      }
    });
  }

  private obterTiposRota(): void {
    const tiposRota = Object.keys(TipoRotaEnum).filter((element) => {
      return isNaN(Number(element));
    });

    tiposRota.forEach((tr, i) => {
      this.tiposRota.push({
        codTipoRota: i + 1,
        nomeTipoRota: tr
      })
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  deletar() {
    this._localService.deletar(this.codPosto);
  }

  private atualizar(): void {
    this.form.disable();

    const form: any = this.form.getRawValue();
    let obj = {
      ...this.local,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._localService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Chamado atualizado com sucesso!", "success");
      this._router.navigate(['local-atendimento']);
    });
  }

  private criar(): void {
    const form: any = this.form.getRawValue();
    let obj = {
      ...this.local,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._localService.criar(obj).subscribe((os) => {
      this._snack.exibirToast("Registro adicionado com sucesso!", "success");
      this._router.navigate(['local-atendimento']);
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
